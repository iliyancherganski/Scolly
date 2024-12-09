using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Scolly.Services.Data.DTOs;
using Scolly.Services.Services.Contracts;

namespace Scolly.Controllers
{
    [Authorize(Roles = "Admin")]
    public class CityController : BaseController
    {
        public CityController(ICityService cityService, ITeacherService teacherService, ISpecialtyService specialtyService, IAgeGroupService ageGroupService, ICourseTypeService courseTypeService) : base(cityService, teacherService, specialtyService, ageGroupService, courseTypeService)
        {
        }

        public async Task<IActionResult> Index(int? page,
            bool? sortByName = null,
            string? searchInput = null)
        {

            var dtos = await _cityService.GetAll();
            if (sortByName != null)
            {
                if (sortByName == false)
                {
                    dtos = dtos.OrderByDescending(x => x.Name).ToList();
                    ViewBag.SortByName = false;
                }
                else
                {
                    dtos = dtos.OrderBy(x => x.Name).ToList();
                    ViewBag.SortByName = true;
                }
            }

            if (searchInput != null)
            {
                dtos = await _cityService.GetAllByName(searchInput);
            }

            dtos = Pagination(page, dtos, 16);
            return View(dtos);
        }

        public IActionResult Add()
        {
            var dto = new CityDto();
            return View(dto);
        }

        [HttpPost]
        public async Task<IActionResult> Add(CityDto model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _cityService.Add(model);
                    return RedirectToAction("Index");
                }
                catch (ArgumentException ex)
                {
                    TempData["ErrorMessage"] = ex.Message;
                    return View(model);
                }
            }
            return View(model);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var dto = await _cityService.GetById(id);
            return View(dto);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(CityDto model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _cityService.EditById(model.Id, model);
                    return RedirectToAction("Index");
                }
                catch (ArgumentException ex)
                {
                    TempData["ErrorMessage"] = ex.Message;
                    return View(model);
                }
            }
            return View(model);
        }

        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                await _cityService.DeleteById(id);
                return RedirectToAction("Index");
            }
            catch (ArgumentException ex)
            {
                TempData["ErrorMessage"] = ex.Message;
                return RedirectToAction(nameof(Index));
            }
        }
    }
}
