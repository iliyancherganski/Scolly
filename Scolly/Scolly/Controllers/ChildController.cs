using Microsoft.AspNetCore.Mvc;
using Scolly.Infrastructure.Data.Models;
using Scolly.Services.Data.DTOs;
using Scolly.Services.Services;
using Scolly.Services.Services.Contracts;

namespace Scolly.Controllers
{
    public class ChildController : BaseController
    {
        private readonly IChildService _childService;
        private readonly ICourseRequestService _courseRequestService;

        public ChildController(ICityService cityService, ITeacherService teacherService, ISpecialtyService specialtyService, IAgeGroupService ageGroupService, ICourseTypeService courseTypeService, IChildService childService, ICourseRequestService courseRequestService) : base(cityService, teacherService, specialtyService, ageGroupService, courseTypeService)
        {
            _childService = childService;
            _courseRequestService = courseRequestService;
        }

        public async Task<IActionResult> Index(int? page,
            string? message = null,
            bool? messageIsError = null,
            bool? sortByName = null,
            int? cityId = 0,
            string? searchInput = null)
        {
            var dtos = await _childService.GetAll();

            if (searchInput != null)
            {
                dtos = await _childService.GetAllByName(searchInput);
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
                    TempData["SuccessMessage"] = message;
                }
            }

            await SortedCitiesInViewBag();

            return View(dtos);
        }

        public async Task<IActionResult> Info(int childId)
        {
            var dto = await _childService.GetById(childId);
            if (dto == null) return RedirectToAction(nameof(Index));

            dto.CourseRequestsDtos = await _courseRequestService.GetAllRequestOfChild(childId);
            return View(dto);
        }

        public async Task<IActionResult> Delete(int childId)
        {
            try
            {
                await _childService.DeleteById(childId);
                return RedirectToAction(nameof(Index));
            }
            catch (ArgumentException ex)
            {
                return RedirectToAction(nameof(Index), new { message = ex.Message, messageIsError = false});
            }
        }
    }
}
