using Microsoft.EntityFrameworkCore;
using Scolly.Infrastructure.Data;
using Scolly.Infrastructure.Data.Enums;
using Scolly.Infrastructure.Data.Models;
using Scolly.Services.Data.DTOs;
using Scolly.Services.Services.Contracts;
using System.Runtime.ExceptionServices;

namespace Scolly.Services.Services
{
    public class ChildService : IChildService
    {
        private ApplicationDbContext _context;
        private ParentService _parentService;


        public ChildService(ApplicationDbContext context, ParentService parentService)
        {
            _context = context;
            _parentService = parentService;
        }

        public async Task Add(ChildDto model)
        {
            var parent = await _context.Parents.FirstOrDefaultAsync(x => x.Id == model.ParentDtoId);
            if (parent != null)
            {
                var child = new Child()
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    ParentId = parent.Id,
                    Parent = parent,
                    PhoneNumber = model.PhoneNumber,
                    CourseRequests = new List<CourseRequest>()
                };
                parent.Children.Add(child);
                await _context.Children.AddAsync(child);
                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteById(int id)
        {
            var child = await _context.Children
                .Include(x => x.Parent)
                .ThenInclude(x => x.User)
                .Include(x => x.CourseRequests)
                .ThenInclude(x => x.Select(x => x.Course))
                .FirstOrDefaultAsync(x => x.Id == id);
            if (child != null)
            {
                var parent = child.Parent;
                parent.Children.Remove(child);
                foreach (var course in child.CourseRequests.Select(x => x.Course))
                {
                    await UnregisterChildToCourse(child.Id, course.Id);
                }
                await _context.SaveChangesAsync();
            }
        }

        public async Task EditById(int id, ChildDto model)
        {
            var child = await _context.Children
               .Include(x => x.Parent)
               .ThenInclude(x => x.User)
               .Include(x => x.CourseRequests)
               .ThenInclude(x => x.Select(x => x.Course))
               .FirstOrDefaultAsync(x => x.Id == id);
            if (child != null)
            {
                child.FirstName = model.FirstName;
                child.LastName = model.LastName;
                child.PhoneNumber = model.PhoneNumber;

                await _context.SaveChangesAsync();
            }

        }

        public async Task<List<ChildDto>> GetAll()
        {
            return await _context.Children
               .Include(x => x.Parent)
               .ThenInclude(x => x.User)
               .Include(x => x.CourseRequests)
               .ThenInclude(x => x.Select(x => x.Course))
               .ThenInclude(x => x.AgeGroup)
               .Include(x => x.CourseRequests)
               .ThenInclude(x => x.Select(x => x.Course))
               .ThenInclude(x => x.CourseType)

               .Select(x => MapData(x))
               .ToListAsync();
        }

        public async Task<List<ChildDto>> GetAllByName(string name)
        {
            if (!string.IsNullOrEmpty(name) && !string.IsNullOrWhiteSpace(name))
            {
                string tempName = string.Empty;
                for (int i = 0; i < name.Length; i++)
                {
                    if (name[i] != ' ')
                    {
                        tempName += name[i];
                    }
                }
                tempName = tempName.ToLower();

                var dtos = await GetAll();

                return dtos.Where(x => $"{x.FirstName}{x.LastName}".ToLower().Contains(tempName)).ToList();
            }
            return [];
        }

        public async Task<List<CourseDto>> GetAllCourses(int childId)
        {
            var child = await _context.Children
               .Include(x => x.Parent)
               .ThenInclude(x => x.User)
               .Include(x => x.CourseRequests)
               .ThenInclude(x => x.Select(x => x.Course))
               .ThenInclude(x => x.AgeGroup)
               .Include(x => x.CourseRequests)
               .ThenInclude(x => x.Select(x => x.Course))
               .ThenInclude(x => x.CourseType)
               .FirstOrDefaultAsync(x => x.Id == childId);

            var courseDtos = new List<CourseDto>();

            if (child != null)
            {
                foreach (var course in child.CourseRequests.Where(x => x.Status == RequestStatus.Accepted).Select(x => x.Course))
                {
                    // courseDtos.Add(_courseService.MapData(course));
                }
            }

            return courseDtos;
        }

        public Task<List<CourseRequestDto>> GetAllRequests(int childId)
        {
            throw new NotImplementedException();
        }

        public async Task<ChildDto?> GetById(int id)
        {
            var dtos = await GetAll();
            return dtos.FirstOrDefault(x => x.Id == id);
        }

        public async Task<ParentDto?> GetParent(int childId)
        {
            var dto = await GetById(childId);
            if (dto == null)
            {
                return null;
            }
            return dto.ParentDto;
        }

        public async Task ManageChildRequestToCourse(int childId, int courseId, bool isAccepted)
        {
            var child = await _context.Children
              .Include(x => x.CourseRequests)
              .FirstOrDefaultAsync(x => x.Id == childId);

            var course = await _context.Courses
                .Include(x => x.CourseRequests)
                .FirstOrDefaultAsync(x => x.Id == courseId);

            if (child != null && course != null)
            {
                var courseRequest = await _context.CourseRequests.FirstOrDefaultAsync(x => x.Course == course && x.Child == child);
                if (courseRequest != null)
                {
                    if (isAccepted)
                        courseRequest.Status = RequestStatus.Accepted;
                    else
                        courseRequest.Status = RequestStatus.Rejected;

                    await _context.SaveChangesAsync();
                }
            }
        }

        public ChildDto MapData(Child model)
        {
            var courseRequestDtos = new List<CourseRequestDto>();
            var courseRequests = _context.CourseRequests
                .Include(x => x.Child)
                .Include(x => x.Course)
                .ThenInclude(x => x.AgeGroup)
                .Include(x => x.Course)
                .ThenInclude(x => x.CourseType)
                .Where(x=>x.Child.Id == model.Id)
                .ToList();

            var dto = new ChildDto
            {
                Id = model.Id,
                FirstName = model.FirstName,
                LastName = model.LastName,
                ParentDtoId = model.ParentId,
                ParentDto = _parentService.MapData(model.Parent),
                PhoneNumber = model.PhoneNumber,
            };

            foreach (var courseRequest in courseRequests)
            {
                courseRequestDtos.Add(new CourseRequestDto()
                {
                    Id = courseRequest.Id,
                    CourseDtoId = courseRequest.CourseId,
                    //CourseDto = _courseService.Mapdata(courseRequest.Course);
                    ChildDtoId = courseRequest.ChildId,
                    ChildDto = dto,
                    Status = courseRequest.Status.ToString(),
                });
            }

            dto.CourseRequestsDtos = courseRequestDtos;

            return dto;
        }

        public async Task RequestChildRegisterToCourse(int childId, int courseId)
        {
            var child = await _context.Children
              .Include(x => x.CourseRequests)
              .FirstOrDefaultAsync(x => x.Id == childId);

            var course = await _context.Courses
                .Include(x => x.CourseRequests)
                .FirstOrDefaultAsync(x => x.Id == courseId);

            if (child != null && course != null)
            {
                var courseRequest = await _context.CourseRequests.FirstOrDefaultAsync(x => x.Course == course && x.Child == child);
                if (courseRequest == null)
                {
                    courseRequest = new CourseRequest()
                    {
                        ChildId = childId,
                        Child = child,
                        CourseId = courseId,
                        Course = course,
                        Status = RequestStatus.Pending
                    };
                    await _context.CourseRequests.AddAsync(courseRequest);
                    await _context.SaveChangesAsync();
                }
            }
        }

        public async Task UnregisterChildToCourse(int childId, int courseId)
        {
            var child = await _context.Children
              .Include(x => x.CourseRequests)
              .FirstOrDefaultAsync(x => x.Id == childId);

            var course = await _context.Courses
                .Include(x => x.CourseRequests)
                .FirstOrDefaultAsync(x => x.Id == courseId);

            if (child != null && course != null)
            {
                var courseRequest = await _context.CourseRequests.FirstOrDefaultAsync(x => x.Course == course && x.Child == child);
                if (courseRequest != null)
                {
                    child.CourseRequests.Remove(courseRequest);
                    course.CourseRequests.Remove(courseRequest);
                    _context.CourseRequests.Remove(courseRequest);

                    await _context.CourseRequests.AddAsync(courseRequest);
                    await _context.SaveChangesAsync();
                }
            }
        }
    }
}
