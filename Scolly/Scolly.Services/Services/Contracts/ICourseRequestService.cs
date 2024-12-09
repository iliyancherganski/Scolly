using Scolly.Services.Data.DTOs;
using Scolly.Services.DTOs.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scolly.Services.Services.Contracts
{
    public interface ICourseRequestService
    {
        Task<List<CourseRequestDto>> GetAll();
        Task<CourseRequestDto?> GetById(int id);
        abstract Task Add(CourseRequestDto model);
        Task DeleteById(int id);
        Task<CourseRequestDto?> MapData(int modelId);
        Task SetStatus(int modelId, RequestStatusDto status);

        Task<List<CourseRequestDto>> GetAllRequestsOfCourse(int courseId, bool onlyAccepted = false);
        Task<List<CourseRequestDto>> GetAllRequestsOfChild(int childId, bool onlyAccepted, bool raw = false);
        Task<int> GetAcceptedRequestsOfChildCount(int childId);

    }
}
