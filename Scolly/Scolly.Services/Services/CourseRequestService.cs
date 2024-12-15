using Humanizer;
using Microsoft.EntityFrameworkCore;
using Scolly.Infrastructure.Data;
using Scolly.Infrastructure.Data.Enums;
using Scolly.Infrastructure.Data.Models;
using Scolly.Services.Data.DTOs;
using Scolly.Services.DTOs.Enums;
using Scolly.Services.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scolly.Services.Services
{
    public class CourseRequestService : ICourseRequestService
    {
        private readonly ApplicationDbContext _context;
        private readonly IAgeGroupService _ageGroupService;
        private readonly ICourseTypeService _courseTypeService;
        private readonly ITeacherService _teacherService;

        public CourseRequestService(ApplicationDbContext context, IAgeGroupService ageGroupService, ICourseTypeService courseTypeService, ITeacherService teacherService)
        {
            _context = context;
            _ageGroupService = ageGroupService;
            _courseTypeService = courseTypeService;
            _teacherService = teacherService;
        }

        public async Task Add(CourseRequestDto model)
        {
            var child = await _context.Children.FirstOrDefaultAsync(x => x.Id == model.ChildDtoId);
            if (child == null)
                return;

            var course = await _context.Courses.FirstOrDefaultAsync(x => x.Id == model.CourseDtoId);
            if (course == null)
                return;

            var courseRequest = new CourseRequest()
            {
                Id = model.Id,
                ChildId = child.Id,
                Child = child,
                CourseId = course.Id,
                Course = course,
                Status = RequestStatus.Pending,
            };
            await _context.CourseRequests.AddAsync(courseRequest);

            child.CourseRequests.Add(courseRequest);
            course.CourseRequests.Add(courseRequest);

            await _context.SaveChangesAsync();
        }

        public async Task DeleteById(int id)
        {
            var courseRequest = await _context.CourseRequests
                .Include(x => x.Child)
                .Include(x => x.Course)
                .FirstOrDefaultAsync(x => x.Id == id);
            if (courseRequest == null)
                return;

            var child = courseRequest.Child;
            if (child == null)
                return;

            var course = courseRequest.Course;
            if (course == null)
                return;

            child.CourseRequests.Remove(courseRequest);
            course.CourseRequests.Remove(courseRequest);

            _context.CourseRequests.Remove(courseRequest);
            await _context.SaveChangesAsync();
        }

        public async Task<int> GetAcceptedRequestsOfChildCount(int childId)
        {
            var child = await _context.Children
                .Include(x => x.CourseRequests)
                .FirstOrDefaultAsync(x => x.Id == childId);
            if (child != null)
            {
                return child.CourseRequests.Where(x => x.Status == RequestStatus.Accepted).Count();
            }
            return 0;
        }

        public async Task<List<CourseRequestDto>> GetAll()
        {
            var courseRequests = await _context.CourseRequests.ToListAsync();
            var courseRequestDtos = new List<CourseRequestDto>();

            var courseRequestDto = new CourseRequestDto();
            foreach (var courseRequest in courseRequests)
            {
                courseRequestDto = await MapData(courseRequest.Id);
                if (courseRequestDto != null)
                {
                    courseRequestDtos.Add(courseRequestDto);
                }
            }
            return courseRequestDtos;
        }

        public async Task<List<CourseRequestDto>> GetAllRequestsOfChild(int childId, bool onlyAccepted, bool raw = false)
        {
            var child = await _context.Children.FirstOrDefaultAsync(x => x.Id == childId);
            if (child == null) return new List<CourseRequestDto>();
            if (raw)
            {
                var dtosRaw = new List<CourseRequestDto>();

                if (onlyAccepted)
                {
                    return dtosRaw = await _context.CourseRequests.Where(x => x.ChildId == child.Id)
                        .Where(x => x.Status == RequestStatus.Accepted)
                        .Select(x => new CourseRequestDto()
                        {
                            Id = x.Id,
                            ChildDtoId = child.Id,
                            CourseDtoId = x.CourseId,
                        })
                        .ToListAsync();
                }
                else
                {
                    dtosRaw = await _context.CourseRequests.Where(x => x.ChildId == child.Id)
                        .Select(x => new CourseRequestDto()
                        {
                            Id = x.Id,
                            ChildDtoId = child.Id,
                            CourseDtoId = x.CourseId,
                        })
                        .ToListAsync();
                }
            }
            var dtos = await GetAll();
            dtos = dtos.Where(x => x.ChildDtoId == child.Id).ToList();
            if (onlyAccepted) return dtos.Where(x => x.Status == RequestStatusDto.Accepted).ToList();
            return dtos;
        }

        public async Task<List<CourseRequestDto>> GetAllRequestsOfCourse(int courseId, bool onlyAccepted, bool raw = false)
        {
            var course = await _context.Courses.FirstOrDefaultAsync(x => x.Id == courseId);
            if (course == null) return new List<CourseRequestDto>();
            if (raw)
            {
                var dtosRaw = new List<CourseRequestDto>();

                if (onlyAccepted)
                {
                    return dtosRaw = await _context.CourseRequests.Where(x => x.CourseId == course.Id)
                        .Where(x => x.Status == RequestStatus.Accepted)
                        .Select(x => new CourseRequestDto()
                        {
                            Id = x.Id,
                            ChildDtoId = x.ChildId,
                            CourseDtoId = course.Id,
                        })
                        .ToListAsync();
                }
                else
                {
                    return dtosRaw = await _context.CourseRequests.Where(x => x.CourseId == course.Id)
                        .Select(x => new CourseRequestDto()
                        {
                            Id = x.Id,
                            ChildDtoId = x.ChildId,
                            CourseDtoId = course.Id,
                        })
                        .ToListAsync();
                }
            }

            var dtos = await GetAll();
            dtos = dtos.Where(x => x.CourseDtoId == course.Id).ToList();
            if (onlyAccepted) return dtos.Where(x => x.Status == RequestStatusDto.Accepted).ToList();
            return dtos;
        }

        public async Task<CourseRequestDto?> GetById(int id)
        {
            var dtos = await GetAll();
            return dtos.FirstOrDefault(x => x.Id == id);
        }

        public async Task<CourseRequestDto?> MapData(int modelId)
        {
            var model = await _context.CourseRequests
                .Include(x => x.Child)
                .ThenInclude(x => x.Parent)
                .ThenInclude(x => x.User)
                .Include(x => x.Course)
                .ThenInclude(x => x.AgeGroup)
                .Include(x => x.Course)
                .ThenInclude(x => x.CourseType)
                .Include(x => x.Course)
                .ThenInclude(x => x.TeachersCourse)
                .ThenInclude(x => x.Teacher)
                .ThenInclude(x => x.User)
                .FirstOrDefaultAsync(x => x.Id == modelId);

            if (model == null) return null;

            var dto = new CourseRequestDto();

            dto.Id = model.Id;

            RequestStatusDto status = RequestStatusDto.Pending;
            if (model.Status == RequestStatus.Accepted)
                status = RequestStatusDto.Accepted;
            else if (model.Status == RequestStatus.Rejected)
                status = RequestStatusDto.Rejected;
            dto.Status = status;

            var child = model.Child;
            if (child == null) return null;
            var childDto = new ChildDto()
            {
                Id = child.Id,
                FirstName = child.FirstName,
                LastName = child.LastName,
                ParentDto = new ParentDto()
                {
                    Id = child.Parent.Id,
                    UserDtoId = child.Parent.UserId,
                    UserDto = new UserDto()
                    {
                        Id = child.Parent.User.Id,
                        FirstName = child.Parent.User.FirstName,
                        LastName = child.Parent.User.LastName,
                        PhoneNumber = child.Parent.User.PhoneNumber,
                    }
                }
            };

            dto.ChildDtoId = childDto.Id;
            dto.ChildDto = childDto;

            var course = model.Course;
            if (course == null) return null;
            //var courseDto = await _courseService.MapData(course.Id);
            //if (courseDto == null) return null;

            var ageGroupDto = await _ageGroupService.MapData(course.AgeGroupId);
            var courseTypeDto = await _courseTypeService.MapData(course.CourseTypeId);

            if (ageGroupDto != null && courseTypeDto != null)
            {
                var courseDto = new CourseDto()
                {
                    Id = course.Id,
                    CourseTypeDto = courseTypeDto,
                    AgeGroupDto = ageGroupDto,
                    StartDate = course.StartDate,
                    EndDate = course.EndDate,
                    Price = course.Price,
                    TeacherDtos = await _teacherService.GetAllTeachersByCourse(course.Id),
                };

                dto.CourseDtoId = courseDto.Id;
                dto.CourseDto = courseDto;
            }

            return dto;
        }

        public async Task SetStatus(int modelId, RequestStatusDto status)
        {
            var model = await _context.CourseRequests.FirstOrDefaultAsync(x => x.Id == modelId);
            if (model == null) return;

            if (status == RequestStatusDto.Accepted)
                model.Status = RequestStatus.Accepted;
            else if (status == RequestStatusDto.Rejected)
                model.Status = RequestStatus.Rejected;
            else if (status == RequestStatusDto.Pending)
                model.Status = RequestStatus.Pending;
            await _context.SaveChangesAsync();
        }
    }
}
