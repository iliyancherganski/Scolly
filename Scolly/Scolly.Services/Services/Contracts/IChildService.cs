using Scolly.Infrastructure.Data.Models;
using Scolly.Services.Data.DTOs;

namespace Scolly.Services.Services.Contracts
{
    public interface IChildService : IBaseService<ChildDto>
    {
        Task<List<CourseRequestDto>> GetAllRequests(int childId);
        Task<List<CourseDto>> GetAllSignedUpCourses(int childId);
        Task<ParentDto?> GetParent(int childId);
        Task RequestChildRegisterToCourse(int childId, int courseId);
        Task UnregisterChildToCourse(int childId, int courseId);
        Task ManageChildRequestToCourse(int childId, int courseId, bool isAccepted);
    }
}
