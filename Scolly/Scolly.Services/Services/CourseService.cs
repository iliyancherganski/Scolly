using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using NuGet.DependencyResolver;
using Scolly.Infrastructure.Data;
using Scolly.Infrastructure.Data.Models;
using Scolly.Services.Data.DTOs;
using Scolly.Services.DTOs.Enums;
using Scolly.Services.Services.Contracts;

namespace Scolly.Services.Services
{
    public class CourseService : ICourseService
    {
        private readonly ApplicationDbContext _context;
        private readonly IAgeGroupService _ageGroupService;
        private readonly ICourseTypeService _courseTypeService;
        private readonly ICourseRequestService _courseRequestService;
        private readonly ITeacherService _teacherService;

        public CourseService(ApplicationDbContext context, IAgeGroupService ageGroupService, ICourseTypeService courseTypeService, ICourseRequestService courseRequestService, ITeacherService teacherService)
        {
            _context = context;
            _ageGroupService = ageGroupService;
            _courseTypeService = courseTypeService;
            _courseRequestService = courseRequestService;
            _teacherService = teacherService;
        }

        public async Task Add(CourseDto model)
        {
            var ageGroup = await _context.AgeGroups.FirstOrDefaultAsync(x => x.Id == model.AgeGroupDtoId);
            if (ageGroup == null) return;

            var courseType = await _context.CourseTypes.FirstOrDefaultAsync(x => x.Id == model.CourseTypeDtoId);
            if (courseType == null) return;

            var course = new Course()
            {
                AgeGroupId = ageGroup.Id,
                AgeGroup = ageGroup,
                CourseTypeId = courseType.Id,
                CourseType = courseType,
                Description = model.Description,
                StartDate = model.StartDate,
                EndDate = model.EndDate,
                Price = model.Price,
            };

            await _context.Courses.AddAsync(course);

            var teacherDtos = model.TeacherDtos;
            var teacher = new Teacher();
            foreach (var teacherDto in teacherDtos)
            {
                teacher = await _context.Teachers
                    .Include(x=>x.TeacherCourses)
                    .FirstOrDefaultAsync(x => x.Id == teacherDto.Id);
                if (teacher != null)
                {
                    var tc = new TeacherCourse()
                    {
                        TeacherId = teacher.Id,
                        Teacher = teacher,
                        CourseId = course.Id,
                        Course = course,
                    };
                    teacher.TeacherCourses.Add(tc);
                    course.TeachersCourse.Add(tc);
                    await _context.TeachersCourses.AddAsync(tc);
                }
            } 

            await _context.SaveChangesAsync();
        }

        public async Task DeleteById(int id)
        {
            var course = await _context.Courses
                .Include(x => x.TeachersCourse)
                .ThenInclude(x => x.Teacher)
                .Include(x => x.AgeGroup)
                .Include(x => x.CourseType)
                .FirstOrDefaultAsync(x => x.Id == id);
            if (course == null) return;

            var teachersCourse = await _context.TeachersCourses
                .Include(x => x.Teacher)
                .Where(x => x.CourseId == course.Id).ToListAsync();
            foreach (var tc in teachersCourse)
            {
                tc.Teacher.TeacherCourses.Remove(tc);
                _context.TeachersCourses.Remove(tc);
            }

            var courseRequestIds = await _context.CourseRequests.Where(x => x.CourseId == course.Id).Select(x => x.Id).ToListAsync();
            foreach (var crId in courseRequestIds)
            {
                await _courseRequestService.DeleteById(crId);
            }

            _context.Courses.Remove(course);
            await _context.SaveChangesAsync();
        }

        public async Task EditById(int id, CourseDto model)
        {
            var ageGroup = await _context.AgeGroups.FirstOrDefaultAsync(x => x.Id == model.AgeGroupDtoId);
            if (ageGroup == null) return;

            var courseType = await _context.CourseTypes.FirstOrDefaultAsync(x => x.Id == model.CourseTypeDtoId);
            if (courseType == null) return;

            var course = await _context.Courses
                .Include(x=>x.TeachersCourse)
                .ThenInclude(x=>x.Teacher)
                .Include(x=>x.AgeGroup)
                .Include(x=>x.CourseType)
                .FirstOrDefaultAsync(x => x.Id == id);
            if (course == null) return;

            course.AgeGroupId = ageGroup.Id;
            course.AgeGroup = ageGroup;
            course.CourseTypeId = courseType.Id;
            course.CourseType = courseType;
            course.Description = model.Description;
            course.StartDate = model.StartDate;
            course.EndDate = model.EndDate;
            course.Price = model.Price;

            var teachersCourse = await _context.TeachersCourses
                .Include(x => x.Teacher)
                .Where(x => x.CourseId == course.Id).ToListAsync();

            foreach (var tc in teachersCourse)
            {
                tc.Teacher.TeacherCourses.Remove(tc);
                _context.TeachersCourses.Remove(tc);
            }

            var teacher = new Teacher();
            foreach (var teacherDto in model.TeacherDtos)
            {
                teacher = await _context.Teachers.FirstOrDefaultAsync(x => x.Id == teacherDto.Id);
                if (teacher != null)
                {
                    var teacherCourse = new TeacherCourse()
                    {
                        TeacherId = teacher.Id,
                        Teacher = teacher,
                        CourseId = course.Id,
                        Course = course,
                    };
                    await _context.TeachersCourses.AddAsync(teacherCourse);
                    course.TeachersCourse.Add(teacherCourse);
                    teacher.TeacherCourses.Add(teacherCourse);
                }
            }
            await _context.SaveChangesAsync();

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
            var courseRequestDtos = new List<CourseRequestDto>();
            courseRequestDtos = await GetAllRequests(courseId);
            var childDtos = courseRequestDtos.Where(x => x.Status == RequestStatusDto.Accepted).Select(x => x.ChildDto).ToList();
            return childDtos;
        }

        public async Task<List<CourseRequestDto>> GetAllRequests(int courseId)
        {
            var course = await _context.Courses
                .Include(x => x.CourseRequests)
                .FirstOrDefaultAsync(x => x.Id == courseId);
            var courseRequestDtos = new List<CourseRequestDto>();
            var courseRequestDto = new CourseRequestDto();

            if (course == null) return courseRequestDtos;

            foreach (var courseRequestId in course.CourseRequests.Select(x => x.Id))
            {
                courseRequestDto = await _courseRequestService.GetById(courseRequestId);
                if (courseRequestDto != null)
                {
                    courseRequestDtos.Add(courseRequestDto);
                }
            }

            return courseRequestDtos;
        }

        public async Task<List<TeacherDto>> GetAllTeachers(int courseId)
        {
            var courseDto = await MapData(courseId);
            var teacherDtos = new List<TeacherDto>();
            if (courseDto == null) return teacherDtos;
            foreach (var teacherDto in courseDto.TeacherDtos)
            {
                if (teacherDto != null)
                {
                    teacherDtos.Add(teacherDto);
                }
            }
            return teacherDtos;
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
        public async Task<List<CourseDto>> GetCoursesOfTeacher(int teacherId)
        {
            var courseDtos = new List<CourseDto>();

            var teacher = await _context.Teachers
                .Include(x => x.TeacherCourses)
                .FirstOrDefaultAsync(x => x.Id == teacherId);

            if (teacher == null) return courseDtos;

            foreach (var courseId in teacher.TeacherCourses.Select(x => x.CourseId))
            {
                var courseDto = await MapData(courseId);
                if (courseDto != null)
                {
                    courseDtos.Add(courseDto);
                }
            }

            return courseDtos;
        }
    }
}
