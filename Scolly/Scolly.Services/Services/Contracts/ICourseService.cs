using Scolly.Infrastructure.Data.Models;
using Scolly.Services.Data.DTOs;

namespace Scolly.Services.Services.Contracts
{
    public interface ICourseService
    {
        Task<List<CourseDto>> GetAll();
        Task<CourseDto?> GetById(int id);
        Task Add(CourseDto model);
        Task EditById(int id, CourseDto model);
        Task DeleteById(int id);
        Task<CourseDto?> MapData(int modelId);
        Task<List<CourseRequestDto>> GetAllRequests(int courseId);
        Task<List<ChildDto>> GetAllRegisteredChildren(int courseId);
        Task<List<TeacherDto>> GetAllTeachers(int courseId);
    }
}
