using Scolly.Infrastructure.Data.Models;
using Scolly.Services.Data.DTOs;

namespace Scolly.Services.Services.Contracts
{
    public interface ICourseService : IBaseService<CourseDto>
    {
        Task<List<CourseRequestDto>> GetAllRequests(int courseId);
        Task<List<ChildDto>> GetAllRegisteredChildren(int courseId);
        Task<List<TeacherDto>> GetAllTeachers(int courseId);
        Task<List<TeacherCourseDto>> GetAllTeacherCourses(int courseId);
    }
}
