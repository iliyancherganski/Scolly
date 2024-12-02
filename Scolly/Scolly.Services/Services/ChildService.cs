using Microsoft.EntityFrameworkCore;
using Scolly.Infrastructure.Data;
using Scolly.Infrastructure.Data.Models;
using Scolly.Services.Data.DTOs;
using Scolly.Services.Services.Contracts;

namespace Scolly.Services.Services
{
    public class ChildService : IChildService
    {
        private ApplicationDbContext _context;
        //private ParentService _parentService;

        public ChildService(ApplicationDbContext context)
        {
            _context = context;
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
                .Include(x=>x.Parent)
                .Include(x=>x.CourseRequests)
                .ThenInclude(x=>x.Select(x=>x.Course))
                .FirstOrDefaultAsync(x => x.Id == id);
            if (child != null)
            {
                var parent = child.Parent;
                parent.Children.Remove(child);
                foreach (var course in child.CourseRequests.Select(x => x.Course))
                {
                    //course.CourseRequests.Remove()
                }
                await _context.SaveChangesAsync();
            }
        }

        public Task EditById(int id, ChildDto model)
        {
            throw new NotImplementedException();
        }

        public Task<List<ChildDto>> GetAll()
        {
            throw new NotImplementedException();
        }

        public Task<List<ChildDto>> GetAllByBame(string name)
        {
            throw new NotImplementedException();
        }

        public Task<List<CourseDto>> GetAllCourses(int childId)
        {
            throw new NotImplementedException();
        }

        public Task<List<CourseRequestDto>> GetAllRequests(int childId)
        {
            throw new NotImplementedException();
        }

        public Task<ChildDto?> GetById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<ParentDto> GetParent(int childId)
        {
            throw new NotImplementedException();
        }

        /*public Task ManageChildRequestToCourse(CourseRequestDto courseRequestDto, bool isAccepted)
        {
            throw new NotImplementedException();
        }*/

        public ChildDto MapData(Child model)
        {
            var dto = new ChildDto
            {
                Id = model.Id,
                FirstName = model.FirstName,
                LastName = model.LastName,
                ParentDtoId = model.ParentId,
                //ParentDto = ,
                PhoneNumber = model.PhoneNumber,
                //CourseRequestsDtos = ,
            };

            return dto;

        }

        public Task RequestChildRegisterToCourse(int childId, int courseId)
        {
            throw new NotImplementedException();
        }

        public Task UnregisterChildToCourse(CourseRequestDto courseRequestDto)
        {
            throw new NotImplementedException();
        }
    }
}
