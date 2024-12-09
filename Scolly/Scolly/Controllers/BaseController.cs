using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.EntityFrameworkCore;
using Scolly.Services.Data.DTOs;
using Scolly.Services.Services.Contracts;
using System.Security.Claims;

namespace Scolly.Controllers
{
    public class BaseController : Controller
    {
        protected ICityService _cityService;
        protected ITeacherService _teacherService;
        protected ISpecialtyService _specialtyService;
        protected IAgeGroupService _ageGroupService;
        protected ICourseTypeService _courseTypeService;

        public BaseController(ICityService cityService, ITeacherService teacherService, ISpecialtyService specialtyService, IAgeGroupService ageGroupService, ICourseTypeService courseTypeService)
        {
            _cityService = cityService;
            _teacherService = teacherService;
            _specialtyService = specialtyService;
            _ageGroupService = ageGroupService;
            _courseTypeService = courseTypeService;
        }

        public string? GetUserId()
        {
            string? id = string.Empty;

            if (User != null)
            {
                id = User.FindFirstValue(ClaimTypes.NameIdentifier);
            }
            return id;
        }

        public List<T> Pagination<T>(int? page, List<T> list, int elementsOnPage)
        {
            if (page == null || elementsOnPage < page * elementsOnPage - list.Count())
            {
                page = 1;
            }
            int pagecount = list.Count() / elementsOnPage;
            if (list.Count() % elementsOnPage > 0)
            {
                pagecount++;
            }

            var tempList = new List<T>();
            int itemsOnPage;
            if (page < pagecount)
            {
                itemsOnPage = elementsOnPage;
            }
            else
            {
                itemsOnPage = list.Count() - (elementsOnPage * ((int)page - 1));
            }

            for (int i = ((int)page - 1) * elementsOnPage; i < ((int)page - 1) * elementsOnPage + itemsOnPage; i++)
            {
                tempList.Add(list[i]);
            }

            ViewBag.Page = (int)page;
            ViewBag.PageCount = pagecount;

            return tempList;
        }

        public async Task SortedCitiesInViewBag()
        {
            var cityDtos = await _cityService.GetAll();

            ViewBag.Cities = cityDtos.OrderBy(x => x.Name).Select(x => new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = x.Name
            }).ToList();
        }

        public async Task SortedTeachersInViewBag()
        {
            var teacherDtos = await _teacherService.GetAll();

            ViewBag.Teachers = teacherDtos
                .OrderBy(x => x.UserDto.CityDto.Name)
                .ThenBy(x => x.UserDto.LastName)
                .ThenBy(x => x.UserDto.FirstName)
                .Select(x => new SelectListItem
                {
                    Value = x.Id.ToString(),
                    Text = $"{x.UserDto.FirstName[0]}. {x.UserDto.LastName} ({x.UserDto.CityDto.Name.ToUpper()}) - {string.Join(", ", x.SpecialtyDtos.Select(x => x.Name))}",
                }).ToList();
        }

        public async Task SortedSpecialtiesInViewBag()
        {
            var specialtyDtos = await _specialtyService.GetAll();

            ViewBag.Specialties = specialtyDtos.OrderBy(x => x.Name).Select(x => new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = x.Name
            }).ToList();
        }

        public async Task SortedCourseTypeInViewBag()
        {
            var courseTypeDtos = await _courseTypeService.GetAll();

            ViewBag.CourseTypes = courseTypeDtos
                .OrderBy(x => x.Name)
                .Select(x => new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = x.Name
            }).ToList();
        }

        public async Task SortedAgeGroupInViewBag()
        {
            var ageGroupDtos = await _ageGroupService.GetAll();

            ViewBag.AgeGroups = ageGroupDtos
                .OrderBy(x => x.Name.Length)
                .ThenBy(x => x.Name)
                .Select(x => new SelectListItem
            {
                Value = x.Id.ToString(),
                Text = x.Name
            }).ToList();
        }
    }
}
