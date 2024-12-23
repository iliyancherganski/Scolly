﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Scolly.Infrastructure.Data.Models;
using Scolly.Services.Data.DTOs;
using Scolly.Services.DTOs.Enums;
using Scolly.Services.Services;
using Scolly.Services.Services.Contracts;
using Scolly.ViewModels.Course;

namespace Scolly.Controllers
{
    [AllowAnonymous]
    public class CourseController : BaseController
    {
        private readonly ICourseService _courseService;
        private readonly IChildService _childService;
        private readonly IParentService _parentService;
        private readonly ICourseRequestService _courseRequestService;

        public CourseController(ICityService cityService, ITeacherService teacherService, ISpecialtyService specialtyService, IAgeGroupService ageGroupService, ICourseTypeService courseTypeService, ICourseService courseService, IChildService childService, ICourseRequestService courseRequestService, IParentService parentService) : base(cityService, teacherService, specialtyService, ageGroupService, courseTypeService)
        {
            _courseService = courseService;
            _childService = childService;
            _courseRequestService = courseRequestService;
            _parentService = parentService;
        }

        public async Task<IActionResult> Index(int? page,
            bool? sortByStartDate = null,
            int? courseTypeId = null,
            int? teacherId = null,
            int? ageGroupId = null)
        {
            ViewBag.SortByStartDate = null;
            ViewBag.CourseTypeId = null;
            ViewBag.AgeGroupId = null;
            ViewBag.TeacherId = null;

            var dtos = new List<CourseDto>();
            if (teacherId != null)
            {
                dtos = await _courseService.GetCoursesOfTeacher((int)teacherId);
                ViewBag.TeacherId = teacherId;
            }
            else
            {
                dtos = await _courseService.GetAll();
                if (courseTypeId != null)
                {
                    dtos = dtos.Where(x => x.CourseTypeDtoId == courseTypeId).ToList();
                    ViewBag.CourseTypeId = courseTypeId;
                }
                else if (ageGroupId != null)
                {
                    dtos = dtos.Where(x => x.AgeGroupDtoId == ageGroupId).ToList();
                    ViewBag.AgeGroupId = ageGroupId;
                }


                if (sortByStartDate != null)
                {
                    if (sortByStartDate == true)
                    {
                        ViewBag.SortByStartDate = true;
                    }
                    else
                    {
                        dtos = dtos.OrderBy(x => x.EndDate).ToList();
                        ViewBag.SortByStartDate = false;
                    }
                }
                else
                {
                    ViewBag.SortByStartDate = null;
                }
            }

            foreach (var dto in dtos)
            {
                dto.CourseRequestDtos = await _courseRequestService.GetAllRequestsOfCourse(dto.Id, false, true);
            }
            if (sortByStartDate == null)
            {
                dtos = dtos.OrderByDescending(x => x.CourseRequestDtos.Count(x => x.Status == RequestStatusDto.Pending)).ToList();
            }

            dtos = Pagination(page, dtos, 12);

            await SortedAgeGroupInViewBag();
            await SortedCourseTypeInViewBag();
            await SortedTeachersInViewBag();

            return View(dtos);
        }

        [Authorize(Roles = "Teacher")]
        public async Task<IActionResult> MyCourses()
        {
            var teacherDto = await _teacherService.GetTeacherByUserId(GetUserId());
            if (teacherDto != null)
            {
                return RedirectToAction(nameof(Index), new { teacherId = teacherDto.Id });
            }
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Info(int courseId)
        {
            var dto = await _courseService.GetById(courseId);
            if (dto == null)
            {
                return RedirectToAction(nameof(Index));
            }
            var crDtos = await _courseRequestService.GetAllRequestsOfCourse(dto.Id, false);
            dto.CourseRequestDtos = crDtos.OrderBy(x => x.ChildDto.FirstName).ThenBy(x => x.ChildDto.LastName).ToList();

            if (User.IsInRole("Parent"))
            {
                var parent = await _parentService.GetParentByUserId(GetUserId());
                if (parent != null && crDtos.Where(x=>x.Status == RequestStatusDto.Accepted).Select(x => x.ChildDto.ParentDtoId).Contains(parent.Id))
                {
                    return View(dto);
                }
                else
                {
                    ViewBag.DoNotShowChildren = true;
                }
            }

            return View(dto);
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Add()
        {
            var model = new CourseFormViewModel();

            await SortedTeachersInViewBag();
            await SortedAgeGroupInViewBag();
            await SortedCourseTypeInViewBag();

            return View(model);
        }


        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Add(CourseFormViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.StartDate > model.EndDate)
                {
                    TempData["ErrorMessage"] = "Невалидно въведени дати! Моля, въведете датите така, че датата на начало на курса да бъде преди датата на края му.";
                }
                else
                {
                    CourseDto dto = new CourseDto()
                    {
                        AgeGroupDtoId = model.AgeGroupId,
                        CourseTypeDtoId = model.CourseTypeId,
                        Description = model.Description,
                        Price = model.Price,
                        TeacherDtos = model.TeacherIds.Select(x => new TeacherDto() { Id = x, }).ToList(),
                    };
                    dto.StartDate = model.StartDate;
                    dto.EndDate = model.EndDate;

                    await _courseService.Add(dto);
                    return RedirectToAction(nameof(Index));
                }
            }

            await SortedTeachersInViewBag();
            await SortedAgeGroupInViewBag();
            await SortedCourseTypeInViewBag();

            return View(model);
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Edit(int courseId)
        {
            var dto = await _courseService.GetById(courseId);
            if (dto != null)
            {
                var model = new CourseFormViewModel()
                {
                    Id = dto.Id,
                    AgeGroupId = dto.AgeGroupDtoId,
                    CourseTypeId = dto.CourseTypeDtoId,
                    Description = dto.Description,
                    Price = dto.Price,
                    TeacherIds = dto.TeacherDtos.Select(x => x.Id).ToList(),
                    StartDate = dto.StartDate,
                    EndDate = dto.EndDate,
                };
                await SortedTeachersInViewBag();
                await SortedAgeGroupInViewBag();
                await SortedCourseTypeInViewBag();

                return View(model);
            }
            return RedirectToAction(nameof(Index));
        }


        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> Edit(CourseFormViewModel model)
        {
            if (ModelState.IsValid)
            {
                if (model.StartDate > model.EndDate)
                {
                    TempData["ErrorMessage"] = "Невалидно въведени дати! Моля, въведете датите така, че датата на започване на курса да бъде преди датата на завършването му.";
                }
                else
                {
                    CourseDto dto = new CourseDto()
                    {
                        AgeGroupDtoId = model.AgeGroupId,
                        CourseTypeDtoId = model.CourseTypeId,
                        Description = model.Description,
                        Price = model.Price,
                        TeacherDtos = model.TeacherIds.Select(x => new TeacherDto() { Id = x, }).ToList(),
                    };
                    dto.StartDate = model.StartDate;
                    dto.EndDate = model.EndDate;

                    await _courseService.EditById(model.Id, dto);
                    return RedirectToAction(nameof(Index));
                }
            }

            await SortedTeachersInViewBag();
            await SortedAgeGroupInViewBag();
            await SortedCourseTypeInViewBag();

            return View(model);
        }


        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int courseId)
        {
            await _courseService.DeleteById(courseId);
            return RedirectToAction(nameof(Index));
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> DeleteRequest(int requestId)
        {
            var dto = await _courseRequestService.GetById(requestId);
            if (dto != null)
            {
                await _courseRequestService.DeleteById(requestId);
                return RedirectToAction("Info", new { courseId = dto.CourseDtoId });
            }
            return RedirectToAction("Index");
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> ConfirmRequest(int requestId)
        {
            var courseRequest = await _courseRequestService.GetById(requestId);
            await _courseRequestService.SetStatus(requestId, RequestStatusDto.Accepted);
            if (courseRequest == null)
            {
                return RedirectToAction("Index");
            }
            return RedirectToAction("Info", new { courseId = courseRequest.CourseDtoId });
        }

        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> RejectRequest(int requestId)
        {
            var courseRequest = await _courseRequestService.GetById(requestId);
            await _courseRequestService.SetStatus(requestId, RequestStatusDto.Rejected);
            if (courseRequest == null)
            {
                return RedirectToAction("Index");
            }
            return RedirectToAction("Info", new { courseId = courseRequest.CourseDtoId });
        }
    }
}
