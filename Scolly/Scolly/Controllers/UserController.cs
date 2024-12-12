using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Scolly.Services.Data.DTOs;
using Scolly.Services.Services.Contracts;
using Scolly.ViewModels.User;
using System.ComponentModel.DataAnnotations;

namespace Scolly.Controllers
{
    [AllowAnonymous]
    public class UserController : BaseController
    {
        private readonly IUserService _userService;
        private readonly IParentService _parentService;

        public UserController(ICityService cityService, ITeacherService teacherService, ISpecialtyService specialtyService, IAgeGroupService ageGroupService, ICourseTypeService courseTypeService, IUserService userService, IParentService parentService) : base(cityService, teacherService, specialtyService, ageGroupService, courseTypeService)
        {
            _userService = userService;
            _parentService = parentService;
        }

        [AllowAnonymous]
        public IActionResult Login()
        {
            if (!_userService.IsSignedIn(User))
            {
                return View(new LoginViewModel());
            }
            return RedirectToAction("Index", "Home");
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _userService.SignIn(model.Email, model.Password);

                if (result)
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Невалидни имейл или парола!");
                    return View(model);
                }
            }
            return View(model);
        }

        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await _userService.Logout();
            return RedirectToAction("Index", "Home");
        }

        [AllowAnonymous]
        public async Task<IActionResult> Register()
        {
            if (!_userService.IsSignedIn(User))
            {
                RegisterFormViewModel model = new RegisterFormViewModel();
                await SortedCitiesInViewBag();
                return View(model);
            }
            return RedirectToAction("Index", "Home");
        }


        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Register(RegisterFormViewModel model)
        {
            if (!_userService.IsSignedIn(User))
            {
                if (ModelState.IsValid)
                {
                    var userDto = new UserDto();
                    userDto.FirstName = model.FirstName;
                    userDto.MiddleName = model.MiddleName;
                    userDto.LastName = model.LastName;
                    userDto.CityDtoId = model.CityId;
                    userDto.Address = model.Address;
                    userDto.Email = model.Email;
                    userDto.Password = model.Password;
                    userDto.PhoneNumber = model.PhoneNumber;

                    var parentDto = new ParentDto();
                    parentDto.UserDto = userDto;

                    await _parentService.Add(parentDto);

                    var result = await _userService.SignIn(model.Email, model.Password);

                    if (result)
                    {
                        return RedirectToAction("Index", "Home");
                    }
                    else
                    {
                        ModelState.AddModelError(string.Empty, "Акаунтът беше регистриран, но се получи грешка при влизането в акаунта. Моля, опитайте отново.");
                        return View(model);
                    }
                }
            }
            await SortedCitiesInViewBag();
            return View(model);
        }
        /*Id
        FirstName
        MiddleName
        LastName
        CityId
        Address
        PhoneNumber
        Email
        Password
        ConfirmPassword
        ScpecialyIds 
        isTeacher*/

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> RegisterTeacher()
        {
            var userDto = new UserDto();
            if (!_userService.IsSignedIn(User))
            {
                var model = new RegisterTeacherViewModel();
                await SortedCitiesInViewBag();
                await SortedSpecialtiesInViewBag();
                return View(model);
            }
            return RedirectToAction("Index", "Home");
            /*Id
            FirstName
            MiddleName
            LastName
            CityId
            Address
            PhoneNumber
            Email
            Password
            ConfirmPassword
            ScpecialyIds 
            isTeacher*/
        }

    }
}
