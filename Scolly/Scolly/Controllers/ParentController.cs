using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.RenderTree;
using Microsoft.AspNetCore.Mvc;
using Scolly.Services.Data.DTOs;
using Scolly.Services.Services.Contracts;

namespace Scolly.Controllers
{
    [Authorize(Roles = "Admin, Teacher")]
    public class ParentController : BaseController
    {
        private readonly IParentService _parentService;
        private readonly IChildService _childService;

        public ParentController(ICityService cityService, ITeacherService teacherService, ISpecialtyService specialtyService, IAgeGroupService ageGroupService, ICourseTypeService courseTypeService, IParentService parentService, IChildService childService) : base(cityService, teacherService, specialtyService, ageGroupService, courseTypeService)
        {
            _parentService = parentService;
            _childService = childService;
        }

        public async Task<IActionResult> Index(int? page,
            bool? sortByName = null,
            int? cityId = null,
            string? searchInput = null)
        {
            var dtos = new List<ParentDto>();
            if (searchInput != null)
            {
                dtos = await _parentService.GetAllByName(searchInput);
                ViewBag.SearchInput = searchInput.TrimEnd();
            }
            else
            {
                dtos = await _parentService.GetAll();

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

            }
            await SortedCitiesInViewBag();

            dtos = Pagination(page, dtos, 12);

            return View(dtos);
        }

        public IActionResult Delete(int parentId)
        {
            return RedirectToAction("Delete", "User", new { id = parentId, isTeacher = false });
        }
    }
}
