﻿using Humanizer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Scolly.Infrastructure.Data.Models;
using Scolly.Services.Data.DTOs;
using Scolly.Services.Services;
using Scolly.Services.Services.Contracts;
using Scolly.ViewModels.Child;


namespace Scolly.Controllers
{
    [Authorize]
    public class ChildController : BaseController
    {
        private readonly IChildService _childService;
        private readonly ICourseRequestService _courseRequestService;
        private readonly ICourseService _courseService;
        private readonly IParentService _parentService;

        public ChildController(ICityService cityService, ITeacherService teacherService, ISpecialtyService specialtyService, IAgeGroupService ageGroupService, ICourseTypeService courseTypeService, IChildService childService, ICourseRequestService courseRequestService, IParentService parentService, ICourseService courseService) : base(cityService, teacherService, specialtyService, ageGroupService, courseTypeService)
        {
            _childService = childService;
            _courseRequestService = courseRequestService;
            _parentService = parentService;
            _courseService = courseService;
        }

        public async Task<IActionResult> Index(int? page,
            string? message = null,
            bool? messageIsError = null,
            bool? sortByName = null,
            int? cityId = null,
            string? searchInput = null)
        {
            var dtos = await _childService.GetAll();

            if (searchInput != null)
            {
                dtos = await _childService.GetAllByName(searchInput);
                ViewBag.SearchInput = searchInput.TrimEnd();
            }

            var userId = GetUserId();
            if (userId == null)
            {
                return RedirectToAction("Index", "Home");
            }
            if (User.IsInRole("Parent"))
            {
                dtos = dtos.Where(x => x.ParentDto.UserDtoId == userId).ToList();
            }

            if (sortByName != null)
            {
                if (sortByName == false)
                {
                    dtos = dtos.OrderByDescending(x => x.FirstName).ToList();
                    ViewBag.SortByName = false;
                }
                else
                {
                    dtos = dtos.OrderBy(x => x.LastName).ToList();
                    ViewBag.SortByName = true;
                }
            }

            ViewBag.CityId = 0;
            if (cityId != null)
            {
                dtos = dtos.Where(x => x.ParentDto.UserDto.CityDtoId == cityId).ToList();
                ViewBag.CityId = cityId;
            }

            if (message != null)
            {
                if (messageIsError != null)
                {
                    if (messageIsError == false)
                    {
                        TempData["SuccessMessage"] = message;
                    }
                    else
                    {
                        TempData["ErrorMessage"] = message;
                    }
                }
            }

            foreach (var dto in dtos)
            {
                dto.CourseRequestsDtos = await _courseRequestService.GetAllRequestsOfChild(dto.Id, true, true);
            }

            await SortedCitiesInViewBag();

            dtos = Pagination(page, dtos, 12);
            return View(dtos);
        }

        public async Task<IActionResult> Info(int childId)
        {
            var dto = await _childService.GetById(childId);
            if (dto == null) return RedirectToAction(nameof(Index));

            if (User.IsInRole("Parent"))
            {
                var parent = await _parentService.GetParentByUserId(GetUserId());
                if (parent != null && dto.ParentDtoId == parent.Id)
                {
                    dto.CourseRequestsDtos = await _courseRequestService.GetAllRequestsOfChild(childId, false);
                    return View(dto);
                }
                return RedirectToAction("Index");
            }

            dto.CourseRequestsDtos = await _courseRequestService.GetAllRequestsOfChild(childId, false);
            return View(dto);
        }

        [Authorize(Roles = "Admin, Parent")]
        public async Task<IActionResult> Delete(int childId)
        {
            var dto = await _childService.GetById(childId);
            if (dto == null) return RedirectToAction(nameof(Index));
            if (User.IsInRole("Parent"))
            {
                var userId = GetUserId();
                if (userId != null)
                {
                    if (dto.ParentDto.UserDtoId == userId)
                    {
                        await _childService.DeleteById(childId);
                        return RedirectToAction(nameof(Index), new
                        {
                            message = $"Регистрациата на детето беше изтрита успешно, заедно със всички заявки и регистрации за курсове.",
                            messageIsError = false
                        });
                    }
                }
                else
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            else
            {
                await _childService.DeleteById(childId);
                return RedirectToAction(nameof(Index), new
                {
                    message = $"Регистрациата на детето беше изтрита успешно, заедно със всички заявки и регистрации за курсове.",
                    messageIsError = false
                });
            }
            return RedirectToAction(nameof(Index));

        }

        [Authorize(Roles = "Admin, Parent")]
        public async Task<IActionResult> Add()
        {
            var model = new ChildFormViewModel();
            if (User.IsInRole("Parent"))
            {
                var parentDto = await _parentService.GetParentByUserId(GetUserId());
                if (parentDto != null)
                {
                    model.ParentId = parentDto.Id;
                }
                else
                {
                    return RedirectToAction(nameof(Index));
                }
            }
            else
            {
                await ParentsInViewBag();
            }
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Add(ChildFormViewModel model)
        {
            if (ModelState.IsValid)
            {
                ChildDto dto = new ChildDto()
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    PhoneNumber = model.PhoneNumber,
                    ParentDtoId = model.ParentId,
                };

                try
                {
                    await _childService.Add(dto);
                    return RedirectToAction(nameof(Index), new
                    {
                        message = $"Регистрациата на детето {model.FirstName} {model.LastName} беше създадена успешно.",
                        messageIsError = false
                    });
                }
                catch (ArgumentException ex)
                {
                    return RedirectToAction(nameof(Index), new
                    {
                        message = ex,
                        messageIsError = true
                    });
                }
            }
            else
            {
                await ParentsInViewBag();

                return View(model);
            }
        }

        [Authorize(Roles = "Admin, Parent")]
        public async Task<IActionResult> Edit(int childId)
        {
            var model = new ChildFormViewModel();
            var dto = await _childService.GetById(childId);

            if (dto != null)
            {
                model.Id = dto.Id;
                model.FirstName = dto.FirstName;
                model.LastName = dto.LastName;
                model.PhoneNumber = dto.PhoneNumber;
                model.ParentId = dto.ParentDtoId;

                if (User.IsInRole("Parent"))
                {
                    var parentDto = await _parentService.GetParentByUserId(GetUserId());
                    if (parentDto != null && dto.ParentDtoId == parentDto.Id)
                    {
                        model.ParentId = parentDto.Id;
                    }
                    else
                    {
                        return RedirectToAction(nameof(Index));
                    }
                }
                else
                {
                    await ParentsInViewBag();
                }
            }


            return View(model);
        }

        [HttpPost]
        [Authorize(Roles = "Admin, Parent")]
        public async Task<IActionResult> Edit(ChildFormViewModel model)
        {
            if (ModelState.IsValid)
            {
                ChildDto dto = new ChildDto()
                {
                    Id = model.Id,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    PhoneNumber = model.PhoneNumber,
                    ParentDtoId = model.ParentId,
                };

                await _childService.EditById(model.Id, dto);

                return RedirectToAction(nameof(Index));
            }
            else
            {
                await ParentsInViewBag();

                return View(model);
            }
        }

        [Authorize(Roles = "Admin, Parent")]
        public async Task<IActionResult> SignUpToCourse(int childId,
            int? page = null,
            int? courseTypeId = null,
            int? ageGroupId = null)
        {
            var userId = GetUserId();

            ViewBag.ChildId = childId;
            ViewBag.CourseTypeId = courseTypeId;
            ViewBag.AgeGroupId = ageGroupId;

            if (userId == null)
            {
                return RedirectToAction(nameof(Index));
            }

            var child = await _childService.GetById(childId);
            if (child != null)
            {
                if (User.IsInRole("Admin") ||
                    (User.IsInRole("Parent") & child.ParentDto.UserDtoId == userId))
                {
                    var model = await _courseService.GetAllWithNoRequestsFromChild(child.Id);

                    ViewBag.ChildName = child.FirstName + " " + child.LastName;

                    if (courseTypeId != null)
                    {
                        model = model.Where(x => x.CourseTypeDto.Id == courseTypeId).ToList();
                    }
                    if (ageGroupId != null)
                    {
                        model = model.Where(x => x.AgeGroupDto.Id == ageGroupId).ToList();
                    }

                    foreach (var dto in model)
                    {
                        dto.CourseRequestDtos = await _courseRequestService.GetAllRequestsOfChild(dto.Id, true, true);
                    }
                    model = model.Where(x => x.EndDate > DateTime.Now).OrderBy(x => x.CourseTypeDto.Name).ThenBy(x => x.AgeGroupDto.Name).ThenBy(x => x.StartDate).ToList();

                    await SortedCourseTypeInViewBag();
                    await SortedAgeGroupInViewBag();

                    model = Pagination(page, model, 16);

                    return View(model);
                }
            }
            return RedirectToAction(nameof(Index));
        }

        [Authorize(Roles = "Admin, Parent")]
        public async Task<IActionResult> SigningUpToCourse(int childId, int courseId)
        {
            var userId = GetUserId();

            if (userId == null)
            {
                return RedirectToAction(nameof(Index));
            }

            var child = await _childService.GetById(childId);
            var course = await _courseService.GetById(courseId);

            if (child != null && course != null)
            {
                if (User.IsInRole("Admin") ||
                    (User.IsInRole("Parent") & child.ParentDto.UserDtoId == userId))
                {
                    var requests = await _courseRequestService.GetAllRequestsOfChild(child.Id, false, true);

                    if (requests.FirstOrDefault(x => x.CourseDtoId == course.Id && x.ChildDtoId == child.Id) != null)
                    {
                        return RedirectToAction("Info", new { childId = child.Id });
                    }

                    var courseRequest = new CourseRequestDto()
                    {
                        ChildDtoId = child.Id,
                        CourseDtoId = course.Id,
                    };

                    await _courseRequestService.Add(courseRequest);
                    return RedirectToAction("Info", new { childId = child.Id });
                }
                TempData["ErrorMessage"] = $"Детето {child.FirstName} {child.LastName} не се регистрира в системата поради неуспешно валидиране на потребителя. ";
            }

            return RedirectToAction("Info", new { childId });
        }

        public async Task ParentsInViewBag()
        {
            var parentDtos = await _parentService.GetAll();
            ViewBag.Parents = parentDtos.Select(x => new SelectListItem()
            {
                Value = x.Id.ToString(),
                Text = x.UserDto.FirstName + " " + x.UserDto.LastName,
            }).ToList();
        }
    }
}
