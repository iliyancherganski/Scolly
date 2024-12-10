using Scolly.Services.Data.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scolly.Services.Services.Contracts
{
    public interface ITeacherService : IBaseService<TeacherDto>
    {
        Task<List<TeacherDto>> GetAllTeachersByCourse(int courseId);
        Task<TeacherDto?> GetTeacherByUserId(string? userId);

    }
}
