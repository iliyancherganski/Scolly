using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Scolly.Services.Data.DTOs;
using Scolly.Services.Services.Contracts;

namespace Scolly.Controllers
{
    [Authorize(Roles = "Admin")]
    public class AgeGroupController : BaseController
    {
        private readonly IAgeGroupService _ageGroupService;

        public AgeGroupController(IAgeGroupService ageGroupService)
        {
            _ageGroupService = ageGroupService;
        }

        public async Task<IActionResult> Index(int? page)
        {
            var dtos = await _ageGroupService.GetAll();
            dtos = Pagination(page, dtos, 8);
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
            await _ageGroupService.Add( model);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Edit(int id)
        {
            var dto = await _ageGroupService.GetById(id);
            return View(dto);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(AgeGroupDto model)
        {
            await _ageGroupService.Add(model);
            return RedirectToAction("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            await _ageGroupService.DeleteById(id);
            return RedirectToAction("Index");
        }
    }
}
