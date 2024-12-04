using Microsoft.EntityFrameworkCore;
using Scolly.Infrastructure.Data;
using Scolly.Infrastructure.Data.Models;
using Scolly.Services.Data.DTOs;
using Scolly.Services.Services.Contracts;
using Scolly.Services.Services.Interfaces;

namespace Scolly.Services.Services
{
    public class CourseService : ICourseService
    {
        private readonly ApplicationDbContext _context;
        private readonly IAgeGroupService _ageGroupService;
        private readonly ICourseTypeService _courseTypeService;
        private readonly IChildService _childService;
        private readonly ITeacherService _teacherService;

        public CourseService(ApplicationDbContext context, IAgeGroupService ageGroupService, ICourseTypeService courseTypeService, IChildService childService, ITeacherService teacherService)
        {
            _context = context;
            _ageGroupService = ageGroupService;
            _courseTypeService = courseTypeService;
            _childService = childService;
            _teacherService = teacherService;
        }

        public async Task Add(CourseDto model)
        {
            throw new NotImplementedException();
        }

        public async Task DeleteById(int id)
        {
            throw new NotImplementedException();
        }

        public async Task EditById(int id, CourseDto model)
        {
            throw new NotImplementedException();
        }

        public async Task<List<CourseDto>> GetAll()
        {
            var courses = await _context.Courses.ToListAsync();
            var courseDtos = new List<CourseDto>();
            var courseDto = new CourseDto();
            foreach (var course in courses)
            {
                if (course != null)
                {
                    courseDto = await MapData(course.Id);
                    if (courseDto != null)
                    {
                        courseDtos.Add(courseDto);
                    }
                }
            }
            return courseDtos;

        }

        public async Task<List<ChildDto>> GetAllRegisteredChildren(int courseId)
        {
            throw new NotImplementedException();
        }

        public async Task<List<CourseRequestDto>> GetAllRequests(int courseId)
        {
            throw new NotImplementedException();
        }

        public async Task<List<TeacherDto>> GetAllTeachers(int courseId)
        {
            throw new NotImplementedException();
        }

        public async Task<CourseDto?> GetById(int id)
        {
            var dtos = await GetAll();
            return dtos.FirstOrDefault(x => x.Id == id);
        }

        public async Task<CourseDto?> MapData(int modelId)
        {
            var model = await _context.Courses
                .Include(x => x.AgeGroup)
                .Include(x => x.CourseType)
                .Include(x => x.TeachersCourse)
                .ThenInclude(x => x.Teacher)
                .ThenInclude(x => x.User)
                .FirstOrDefaultAsync(x => x.Id == modelId);

            if (model == null)
                return null;

            var ageGroupDto = await _ageGroupService.MapData(model.AgeGroup.Id);
            if (ageGroupDto == null)
                return null;

            var courseTypeDto = await _courseTypeService.MapData(model.CourseType.Id);
            if (courseTypeDto == null)
                return null;

            var dto = new CourseDto()
            {
                Id = model.Id,
                StartDate = model.StartDate,
                EndDate = model.EndDate,
                Price = model.Price,
                Description = model.Description,

                AgeGroupDtoId = ageGroupDto.Id,
                AgeGroupDto = ageGroupDto,
                CourseTypeDtoId = courseTypeDto.Id,
                CourseTypeDto = courseTypeDto,
            };

            var teacherDtos = new List<TeacherDto>();
            var teacherDto = new TeacherDto();
            foreach (var teacher in model.TeachersCourse.Select(x => x.Teacher))
            {
                if (teacher != null)
                {
                    teacherDto = await _teacherService.MapData(teacher.Id);
                    if (teacherDto != null)
                    {
                        teacherDtos.Add(teacherDto);
                    }
                }
            }
            dto.TeacherDtos = teacherDtos;

            return dto;
        }
    }
}
