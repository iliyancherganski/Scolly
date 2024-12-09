using Scolly.Infrastructure.Data.Models;
using Scolly.Services.Data.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scolly.Services.Services.Contracts
{
    public interface IParentService : IBaseService<ParentDto>
    {
        Task<List<ChildDto>> GetAllChildren(int parentId);
        Task<ParentDto?> GetParentByUserId(string? userId);
    }
}
