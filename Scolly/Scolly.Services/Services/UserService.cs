using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

using Scolly.Infrastructure.Data;
using Scolly.Infrastructure.Data.Models;
using Scolly.Services.Data.DTOs;
using Scolly.Services.Data.Enums;
using Scolly.Services.Services.Contracts;
using System.Security.Claims;

namespace Scolly.Services.Services
{
    public class UserService : IUserService
    {
        private readonly ApplicationDbContext _context;
        private readonly ICityService _cityService;

        private readonly SignInManager<User> _signInManager;
        private readonly UserManager<User> _userManager;
        private readonly IUserStore<User> _userStore;

        public UserService(ApplicationDbContext context, ICityService cityService, SignInManager<User> signInManager, UserManager<User> userManager, IUserStore<User> userStore)
        {
            _context = context;
            _cityService = cityService;
            _signInManager = signInManager;
            _userManager = userManager;
            _userStore = userStore;
        }

        //private readonly ILogger<LoginViewModel> _logger;


        public async Task DeleteUserById(string id)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == id);
            if (user == null) return;

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
        }

        public async Task EditUserById(string id, UserDto model)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == id);
            if (user == null) return;

            user.FirstName = model.FirstName;
            user.MiddleName = model.MiddleName;
            user.LastName = model.LastName;
            user.Address = model.Address;
            user.PhoneNumber = model.PhoneNumber;
            var city = await _context.Cities.FirstOrDefaultAsync(x => x.Id == model.CityDtoId);
            if (city != null)
            {
                user.CityId = city.Id;
                user.City = city;
            }
            user.CityId = model.CityDtoId;

            await _context.SaveChangesAsync();
        }

        public async Task<List<UserDto>> GetAllByName(string name)
        {
            if (!string.IsNullOrEmpty(name) && !string.IsNullOrWhiteSpace(name))
            {
                string tempName = string.Empty;
                for (int i = 0; i < name.Length; i++)
                {
                    if (name[i] != ' ')
                    {
                        tempName += name[i];
                    }
                }
                tempName = tempName.ToLower();

                var dtos = await GetAllUsers();

                dtos = dtos
                    .Where(x => $"{x.FirstName}{x.LastName}".ToLower().Contains(tempName)
                        || (x.PhoneNumber != null && x.PhoneNumber.Contains(tempName))).ToList();

                return dtos;
            }
            return new List<UserDto>();
        }

        public async Task<List<UserDto>> GetAllUsers()
        {
            var users = await _context.Users
                .ToListAsync();

            var userDtos = new List<UserDto>();

            var userDto = new UserDto();
            foreach (var userId in users.Select(x => x.Id))
            {
                userDto = await MapData(userId);
                if (userDto != null)
                {
                    userDtos.Add(userDto);
                }
            }

            return userDtos;
        }

        public async Task<UserDto?> GetUserById(string id)
        {
            var dtos = await GetAllUsers();
            var dto = dtos.FirstOrDefault(x => x.Id == id);
            return dto;
        }

        public bool IsSignedIn(ClaimsPrincipal user)
        {
            return _signInManager.IsSignedIn(user);
        }

        public async Task Logout()
        {
            await _signInManager.SignOutAsync();
        }

        public async Task<UserDto?> MapData(string modelId)
        {
            var model = await _context.Users
                .Include(x => x.City)
                .FirstOrDefaultAsync(x => x.Id == modelId);
            if (model == null)
            {
                return null;
            }

            var dto = new UserDto()
            {
                Id = model.Id,
                FirstName = model.FirstName,
                MiddleName = model.MiddleName,
                LastName = model.LastName,
                Address = model.Address,
                PhoneNumber = model.PhoneNumber,
            };
            var city = _context.Cities.FirstOrDefault(x => x.Id == model.CityId);
            if (city != null)
            {
                var cityDto = await _cityService.MapData(city.Id);
                if (cityDto != null)
                {
                    dto.CityDtoId = cityDto.Id;
                    dto.CityDto = cityDto;
                }
            }

            dto.Role = UserRoles.Teacher;

            return dto;
        }

        public async Task<string?> RegisterNewUser(UserDto model)
        {
            if (await _context.Users.FirstOrDefaultAsync(x => x.Email == model.Email) == null)
            {
                User user = CreateUser();

                user.FirstName = model.FirstName;
                user.MiddleName = model.MiddleName;
                user.LastName = model.LastName;
                user.Address = model.Address;
                user.PhoneNumber = model.PhoneNumber;
                user.LockoutEnabled = false;
                user.EmailConfirmed = true;
                var city = await _context.Cities.FirstOrDefaultAsync(x => x.Id == model.CityDtoId);
                if (city != null)
                {
                    user.CityId = city.Id;
                    user.City = city;
                }
                user.CityId = model.CityDtoId;

                await _userStore.SetUserNameAsync(user, model.Email, CancellationToken.None);
                _userManager.Options.SignIn.RequireConfirmedAccount = false;
                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    return user.Id;
                }
            }
            return null;
        }

        public async Task<SignInResult?> SignIn(string username, string password)
        {
            var user = _context.Users.FirstOrDefault(x => (x.UserName != null && x.UserName.ToUpper() == username.ToUpper()) || (x.Email != null && x.Email.ToUpper() == username.ToUpper()));
            if (user == null || user.UserName == null)
            {
                throw new ArgumentException("Невалиден имейл или парола!");
            }
            var result = await _signInManager.PasswordSignInAsync(user.UserName, password, true, lockoutOnFailure: false);
            return result;
        }

        private User CreateUser()
        {
            try
            {
                return Activator.CreateInstance<User>();
            }
            catch
            {
                throw new InvalidOperationException($"Can't create an instance of '{nameof(User)}'. ");
            }
        }
    }
}
