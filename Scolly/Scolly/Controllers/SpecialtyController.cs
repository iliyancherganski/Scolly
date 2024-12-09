using Microsoft.AspNetCore.Mvc;
using Scolly.Services.Data.DTOs;
using Scolly.Services.Services.Contracts;

namespace Scolly.Controllers
{
    public class SpecialtyController : BaseController
    {
        public SpecialtyController(ICityService cityService, ITeacherService teacherService, ISpecialtyService specialtyService, IAgeGroupService ageGroupService, ICourseTypeService courseTypeService) : base(cityService, teacherService, specialtyService, ageGroupService, courseTypeService)
        {
        }

        public async Task<IActionResult> Index(int? page,
            bool? sortByName = null,
            string? searchInput = null)
        {

            var dtos = await _specialtyService.GetAll();
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
                dtos = await _specialtyService.GetAllByName(searchInput);
            }

            dtos = Pagination(page, dtos, 16);
            return View(dtos);
        }

        public IActionResult Add()
        {
            var dto = new SpecialtyDto();
            return View(dto);
        }

        [HttpPost]
        public async Task<IActionResult> Add(SpecialtyDto model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _specialtyService.Add(model);
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
            var dto = await _specialtyService.GetById(id);
            return View(dto);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(SpecialtyDto model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _specialtyService.EditById(model.Id, model);
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
                await _specialtyService.DeleteById(id);
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
