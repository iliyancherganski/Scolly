using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata;
using Scolly.Services.Services.Contracts;

namespace Scolly.Controllers
{
    [AllowAnonymous]
    public class TeacherController : BaseController
    {
        private readonly ICourseService _courseService;
        private readonly ICourseRequestService _courseRequestService;

        public TeacherController(ICityService cityService, ITeacherService teacherService, ISpecialtyService specialtyService, IAgeGroupService ageGroupService, ICourseTypeService courseTypeService, ICourseService courseService, ICourseRequestService courseRequestService) : base(cityService, teacherService, specialtyService, ageGroupService, courseTypeService)
        {
            _courseService = courseService;
            _courseRequestService = courseRequestService;
        }

        public async Task<IActionResult> Index(
            int? page,
            bool? sortByName = null,
            string? searchInput = null,
            int? cityId = null,
            int? specialtyId = null)
        {
            var dtos = await _teacherService.GetAll();

            if (sortByName != null)
            {
                if (sortByName == false)
                {
                    dtos = dtos.OrderByDescending(x => x.UserDto.FirstName).ToList();
                    ViewBag.SortByName = false;
                }
                else
                {
                    dtos = dtos.OrderBy(x => x.UserDto.LastName).ToList();
                    ViewBag.SortByName = true;
                }

            }

            ViewBag.CityId = null;
            if (cityId != null)
            {
                dtos = dtos.Where(x => x.UserDto.CityDtoId == cityId).ToList();
                ViewBag.CityId = cityId;
            }

            ViewBag.SpecialtyId = null;
            if (specialtyId != null)
            {
                dtos = dtos.Where(x => x.SpecialtyDtos.Any(x => x.Id == specialtyId)).ToList();
                ViewBag.SpecialtyId = specialtyId;
            }

            if (searchInput != null)
            {
                dtos = await _teacherService.GetAllByName(searchInput);
                ViewBag.SearchInput = searchInput.TrimEnd();
            }

            await SortedCitiesInViewBag();
            await SortedSpecialtiesInViewBag();

            dtos = Pagination(page, dtos, 12);
            return View(dtos);
        }

        public async Task<IActionResult> Info(int teacherId)
        {
            var dto = await _teacherService.GetById(teacherId);
            if (dto == null)
            {
                return RedirectToAction(nameof(Index));
            }
            var courseDtos = await _courseService.GetCoursesOfTeacher(dto.Id);
            foreach (var course in courseDtos)
            {
                var courseRequestDtos = await _courseRequestService.GetAllRequestsOfCourse(course.Id, true, true);
                course.CourseRequestDtos = courseRequestDtos;
            }

            dto.CourseDtos = courseDtos;

            return View(dto);
        }

    }
}
