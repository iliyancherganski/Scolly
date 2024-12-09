using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Scolly.Infrastructure.Data.Models;
using Scolly.Services.Data.DTOs;
using Scolly.Services.Services.Contracts;

namespace Scolly.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AgeGroupController : BaseController
    {
        public AgeGroupController(ICityService cityService, ITeacherService teacherService, ISpecialtyService specialtyService, IAgeGroupService ageGroupService, ICourseTypeService courseTypeService) : base(cityService, teacherService, specialtyService, ageGroupService, courseTypeService)
        {
        }

        public async Task<IActionResult> Index(int? page,
            bool? sortByName = null,
            string? searchInput = null)
        {

            var dtos = await _ageGroupService.GetAll();
            if (sortByName != null)
            {
                if (sortByName == false)
                {
                    dtos = dtos.OrderBy(x => x.Name.Length).ThenByDescending(x => x.Name).ToList();
                    ViewBag.SortByName = false;
                }
                else
                {
                    dtos = dtos.OrderBy(x => x.Name.Length).ThenBy(x => x.Name).ToList();
                    ViewBag.SortByName = true;
                }
            }

            if (searchInput != null)
            {
                dtos = await _ageGroupService.GetAllByName(searchInput);
                ViewBag.SearchInput = searchInput.TrimEnd();
            }

            dtos = Pagination(page, dtos, 16);
            return View(dtos);
        }

        public IActionResult Add()
        {
            var dto = new AgeGroupDto();
            return View(dto);
        }

        [HttpPost]
        public async Task<IActionResult> Add(AgeGroupDto model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _ageGroupService.Add(model);
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
            var dto = await _ageGroupService.GetById(id);
            return View(dto);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(AgeGroupDto model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _ageGroupService.EditById(model.Id, model);
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
                await _ageGroupService.DeleteById(id);
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
