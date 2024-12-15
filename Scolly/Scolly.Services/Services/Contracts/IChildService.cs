using Scolly.Infrastructure.Data.Models;
using Scolly.Services.Data.DTOs;
using Scolly.Services.DTOs.Enums;

namespace Scolly.Services.Services.Contracts
{
    public interface IChildService : IBaseService<ChildDto>
    {
        Task<List<CourseRequestDto>> GetAllRequests(int childId, RequestStatusDto? status = null);
        Task<List<CourseDto>> GetAllSignedUpCourses(int childId);
        Task<ParentDto?> GetParent(int childId);
        Task RequestChildRegisterToCourse(int childId, int courseId);
        Task UnregisterChildToCourse(int childId, int courseId);
        Task ManageChildRequestToCourse(int childId, int courseId, bool isAccepted);
        //Task<string> DeleteByIdWithMessage(int childId, bool isError = false);
    }
}
