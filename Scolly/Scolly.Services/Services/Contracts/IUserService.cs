using Scolly.Infrastructure.Data.Models;
using Scolly.Services.Data.DTOs;

namespace Scolly.Services.Services.Contracts
{
    public interface IUserService
    {
        Task<List<UserDto>> GetAllUser();
        Task<List<UserDto>> GetAllByName(string name);
        Task<UserDto?> GetUserById(string id);
        abstract Task RegitserNewUser(UserDto model);
        Task EditUserById(string id, UserDto model);
        Task DeleteUserById(int id);
        UserDto MapData(User model);
    }
}
