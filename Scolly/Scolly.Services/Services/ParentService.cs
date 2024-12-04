using Microsoft.EntityFrameworkCore;

using Scolly.Infrastructure.Data;
using Scolly.Infrastructure.Data.Models;
using Scolly.Services.Data.DTOs;
using Scolly.Services.Data.Enums;
using Scolly.Services.Services.Contracts;

namespace Scolly.Services.Services
{
    public class ParentService : IParentService
    {
        // ctros when done
        private readonly ApplicationDbContext _context;

        private readonly IChildService _childService;
        private readonly IUserService _userService;

        public ParentService(ApplicationDbContext context, IChildService childService, IUserService userService)
        {
            _context = context;
            _childService = childService;
            _userService = userService;
        }

        public async Task<List<ParentDto>> GetAll()
        {
            var parents = await _context.Parents.ToListAsync();
            var parentDtos = new List<ParentDto>();
            foreach (var parentDto in parents.Select(x => MapData(x.Id)))
            {
                if (parentDto.Result != null)
                {
                    parentDtos.Add(parentDto.Result);
                }
            }
            return parentDtos;
        }

        public async Task<List<ParentDto>> GetAllByName(string name)
        {
            var parentDtos = new List<ParentDto>();
            var userDtos = await _userService.GetAllByName(name);

            var parent = new Parent();
            var parentDto = new ParentDto();
            foreach (var userDto in userDtos)
            {
                parent = await _context.Parents
                    .Include(x => x.User)
                    .FirstOrDefaultAsync(x => x.UserId == userDto.Id);
                if (parent != null)
                {
                    parentDto = await MapData(parent.Id);
                    if (parentDto != null)
                    {
                        parentDtos.Add(parentDto);
                    }
                }
            }
            return parentDtos;
        }

        public async Task<List<ChildDto>> GetAllChildren(int parentId)
        {
            var parentDto = await GetById(parentId);
            if (parentDto != null)
                return parentDto.ChildDtos;
            return new List<ChildDto> { };
        }

        public async Task<ParentDto?> GetById(int id)
        {
            var parentDtos = await GetAll();
            return parentDtos.FirstOrDefault(x => x.Id == id);
        }

        public async Task Add(ParentDto model)
        {
            var parent = new Parent();
            await _context.Parents.AddAsync(parent);

        }

        public Task DeleteById(int id)
        {
            throw new NotImplementedException();
        }

        public Task EditById(int id, ParentDto model)
        {
            throw new NotImplementedException();
        }

        public async Task<ParentDto?> MapData(int modelId)
        {
            var model = await _context.Parents
                .Include(x => x.User)
                .ThenInclude(x => x.City)
                .Include(x => x.Children)
                .FirstOrDefaultAsync(x => x.Id == modelId);

            if (model == null)
            {
                return null;
            }

            var dto = new ParentDto { Id = model.Id, };

            var childDtos = new List<ChildDto>();
            foreach (var childDto in model.Children.Select(x => _childService.MapData(x.Id)))
            {
                if (childDto != null && childDto.Result != null)
                {
                    childDtos.Add(childDto.Result);
                }
            }
            dto.ChildDtos = childDtos;

            var userDto = _userService.MapData(model.User.Id).Result;
            if (userDto != null)
            {
                dto.UserDtoId = userDto.Id;
                dto.UserDto = userDto;
            }

            dto.UserDto.Role = UserRoles.Parent;

            return dto;
        }
    }
}
