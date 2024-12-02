using Scolly.Infrastructure.Data.Models;
using Scolly.Services.Data.DTOs;

namespace Scolly.Services.Services.Contracts
{
    public interface IChildService : IBaseService<ChildDto, Child>
    {
        Task<List<CourseRequestDto>> GetAllRequests(int childId);
        Task<List<CourseDto>> GetAllCourses(int childId);
        Task<ParentDto?> GetParent(int childId);
        Task RequestChildRegisterToCourse(int childId, int courseId);
        //Task ManageChildRequestToCourse(CourseRequestDto courseRequestDto, bool isAccepted);
        Task UnregisterChildToCourse(int childId, int courseId);
        Task ManageChildRequestToCourse(int childId, int courseId, bool isAccepted);
    }
}
