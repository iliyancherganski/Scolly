using Scolly.Infrastructure.Data.Models;
using Scolly.Services.Data.DTOs;
using Scolly.Services.Data.Enums;

namespace Scolly.Services.Services.Contracts
{
    public interface IUserService
    {
        Task<List<UserDto>> GetAllUsers();
        Task<List<UserDto>> GetAllByName(string name);
        Task<UserDto?> GetUserById(string id);
        Task<string?> RegisterNewUser(UserDto model);
        Task EditUserById(string id, UserDto model);
        Task DeleteUserById(string id);
        Task<UserDto?> MapData(string modelId);
    }
}
