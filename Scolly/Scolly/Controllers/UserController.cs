using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Scolly.Infrastructure.Data.Models;
using Scolly.Services.Data.DTOs;
using Scolly.Services.Services.Contracts;
using Scolly.ViewModels.User;

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
        public async Task<IActionResult> Register(bool isTeacher = false)
        {
            if (!_userService.IsSignedIn(User) && !isTeacher)
            {
                RegisterFormViewModel model = new RegisterFormViewModel();
                await SortedCitiesInViewBag();
                return View(model);
            }
            else if (_userService.IsSignedIn(User) &&
                isTeacher &&
                User.IsInRole("Admin"))
            {
                RegisterFormViewModel model = new RegisterFormViewModel(true);
                await SortedCitiesInViewBag();
                await SortedSpecialtiesInViewBag();
                return View(model);
            }
            return RedirectToAction("Index", "Home");
        }


        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Register(RegisterFormViewModel model)
        {
            if ((model.SpecialtyIds == null && _userService.IsSignedIn(User)) ||
                (model.SpecialtyIds != null && _userService.IsSignedIn(User) && !User.IsInRole("Admin")))
            {
                return RedirectToAction("Index", "Home");
            }

            else if (ModelState.IsValid)
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


                if (await _userService.UserWithEmailExists(model.Email))
                {
                    ModelState.AddModelError(string.Empty, "Вече има акаунт, който е регистриран с този имейл.");
                }
                else
                {
                    // IF PARENT
                    if (model.SpecialtyIds == null)
                    {
                        var parentDto = new ParentDto();
                        parentDto.UserDto = userDto;
                        await _parentService.Add(parentDto);
                        ModelState.AddModelError(string.Empty, "Акаунтът беше регистриран, но се получи грешка при влизането в акаунта. Моля, опитайте отново.");

                        var result = await _userService.SignIn(model.Email, model.Password);
                        if (result)
                        {
                            return RedirectToAction("Index", "Home");
                        }
                        else
                        {
                            ModelState.AddModelError(string.Empty, "Акаунтът беше регистриран, но се получи грешка при влизането в акаунта. Моля, опитайте отново.");
                        }

                    }
                    // IF TEACHER
                    else
                    {
                        var teacherDto = new TeacherDto();
                        teacherDto.UserDto = userDto;
                        foreach (var id in model.SpecialtyIds)
                        {
                            teacherDto.SpecialtyDtos.Add(new SpecialtyDto() { Id = id });
                        }
                        await _teacherService.Add(teacherDto);
                        return RedirectToAction("Index", "Teacher");

                    }
                }
            }

            if (model.SpecialtyIds != null)
            {
                await SortedSpecialtiesInViewBag();
            }
            await SortedCitiesInViewBag();
            return View(model);
        }

        [Authorize]
        public async Task<IActionResult> Edit(int id, bool isTeacher = false)
        {
            var model = new EditAccountViewModel();
            var user = new UserDto();
            var specialtyIds = new List<int>();
            string? userId = GetUserId();

            if (userId == null)
            {
                TempData["ErrorMessage"] = "Получи се грешка при потвърждаването самоличността на потребителя. ";
                return RedirectToAction("Index", "Home");
            }

            if (isTeacher)
            {
                var teacher = await _teacherService.GetById(id);

                if (teacher != null &&
                    (User.IsInRole("Admin") ||
                        (User.IsInRole("Teacher") && await _teacherService.GetTeacherByUserId(userId) != null)))
                {
                    user = teacher.UserDto;
                    specialtyIds = teacher.SpecialtyDtos.Select(x => x.Id).ToList();
                    model.SpecialtyIds = specialtyIds;
                    model.IsTeacher = true;
                    model.Id = teacher.Id;

                    await SortedSpecialtiesInViewBag();
                }
                else
                {
                    TempData["ErrorMessage"] = "Получи се грешка при потвърждаването самоличността на потребителя. ";
                    return RedirectToAction("Index", "Home");
                }

            }
            else
            {
                var parent = await _parentService.GetById(id);
                if (parent != null &&
                    (User.IsInRole("Admin") ||
                        (User.IsInRole("Parent") && await _parentService.GetParentByUserId(userId) != null)))
                {
                    user = parent.UserDto;
                    model.IsTeacher = false;
                    model.Id = parent.Id;
                }
                else
                {
                    TempData["ErrorMessage"] = "Получи се грешка при потвърждаването самоличността на потребителя. ";
                    return RedirectToAction("Index", "Home");
                }
            }

            if (user != null)
            {
                model.FirstName = user.FirstName;
                model.MiddleName = user.MiddleName;
                model.LastName = user.LastName;
                model.Address = user.Address;
                model.PhoneNumber = user.PhoneNumber;
                model.CityId = user.CityDtoId;
            }
            else
            {
                return RedirectToAction("Index", "Home");
            }
            await SortedCitiesInViewBag();
            return View(model);
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Edit(EditAccountViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = new UserDto();
                string? userId = GetUserId();

                if (userId == null)
                {
                    TempData["ErrorMessage"] = "Получи се грешка при потвърждаването самоличността на потребителя. ";
                    return RedirectToAction("Index", "Home");
                }

                user.FirstName = model.FirstName;
                user.MiddleName = model.MiddleName;
                user.LastName = model.LastName;
                user.Address = model.Address;
                user.PhoneNumber = model.PhoneNumber;
                user.CityDtoId = model.CityId;

                if (model.IsTeacher)
                {
                    var teacher = new TeacherDto();

                    if (User.IsInRole("Admin") ||
                        (User.IsInRole("Teacher") && await _teacherService.GetTeacherByUserId(userId) != null))
                    {
                        teacher.Id = model.Id;
                        teacher.SpecialtyDtos = model.SpecialtyIds.Select(x => new SpecialtyDto() { Id = x }).ToList();
                        teacher.UserDto = user;

                        await _teacherService.EditById(teacher.Id, teacher);
                        return RedirectToAction("Index", "Teacher");
                    }
                    else
                    {
                        TempData["ErrorMessage"] = "Получи се грешка при потвърждаването самоличността на потребителя. ";
                        return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    var parent = new ParentDto();

                    if (User.IsInRole("Admin") ||
                        (User.IsInRole("Parent") && await _parentService.GetParentByUserId(userId) != null))
                    {
                        parent.Id = model.Id;
                        parent.UserDto = user;

                        await _parentService.EditById(parent.Id, parent);
                        return RedirectToAction("Index", "Parent");
                    }
                    else
                    {
                        TempData["ErrorMessage"] = "Получи се грешка при потвърждаването самоличността на потребителя. ";
                        return RedirectToAction("Index", "Home");
                    }
                }
            }
            await SortedCitiesInViewBag();
            if (model.IsTeacher)
            {
                await SortedSpecialtiesInViewBag();
            }
            return View(model);
        }

        [Authorize(Roles = "Parent, Teacher")]
        public async Task<IActionResult> DirectEdit()
        {
            string? userId = GetUserId();

            if (userId == null)
            {
                TempData["ErrorMessage"] = "Получи се грешка при потвърждаването самоличността на потребителя. ";
                return RedirectToAction("Index", "Home");
            }

            if (User.IsInRole("Parent"))
            {
                var parent = await _parentService.GetParentByUserId(userId);
                if (parent != null)
                {
                    return RedirectToAction("Edit", new { id = parent.Id, isTeacher = false });
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }
            else if (User.IsInRole("Teacher"))
            {
                var teacher = await _teacherService.GetTeacherByUserId(userId);
                if (teacher != null)
                {
                    return RedirectToAction("Edit", new { id = teacher.Id, isTeacher = true });
                }
                else
                {
                    return RedirectToAction("Index", "Home");
                }
            }

            return RedirectToAction("Index", "Home");
        }


        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id, bool isTeacher = false)
        {
            if (User.IsInRole("Admin"))
            {
                if (!isTeacher)
                {
                    await _parentService.DeleteById(id);
                    return RedirectToAction("Index", "Parent");

                }
                else
                {
                    await _teacherService.DeleteById(id);
                    return RedirectToAction("Index", "Teacher");
                }
            }

            return RedirectToAction("Index", "Home");
        }
    }
}
