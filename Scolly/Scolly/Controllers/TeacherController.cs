using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata;
using Scolly.Services.Services.Contracts;

namespace Scolly.Controllers
{
    public class TeacherController : BaseController
    {
        private readonly ICourseService _courseService;

        public TeacherController(ICityService cityService, ITeacherService teacherService, ISpecialtyService specialtyService, IAgeGroupService ageGroupService, ICourseTypeService courseTypeService, ICourseService courseService) : base(cityService, teacherService, specialtyService, ageGroupService, courseTypeService)
        {
            _courseService = courseService;
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
