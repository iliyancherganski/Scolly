using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Scolly.Services.Data.DTOs;
using Scolly.Services.Services.Contracts;

namespace Scolly.Controllers
{
    [Authorize(Roles = "Admin")]
    public class CourseTypeController : BaseController
    {
        private readonly ICourseTypeService _courseTypeService;

        public CourseTypeController(ICourseTypeService courseTypeService)
        {
            _courseTypeService = courseTypeService;
        }

        public async Task<IActionResult> Index(int? page,
            bool? sortByName = null,
            string? searchInput = null)
        {

            var dtos = await _courseTypeService.GetAll();
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
                dtos = await _courseTypeService.GetAllByName(searchInput);
            }

            dtos = Pagination(page, dtos, 16);
            return View(dtos);
        }

        public IActionResult Add()
        {
            var dto = new CourseTypeDto();
            return View(dto);
        }

        [HttpPost]
        public async Task<IActionResult> Add(CourseTypeDto model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _courseTypeService.Add(model);
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
            var dto = await _courseTypeService.GetById(id);
            return View(dto);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(CourseTypeDto model)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _courseTypeService.EditById(model.Id, model);
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
                await _courseTypeService.DeleteById(id);
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
