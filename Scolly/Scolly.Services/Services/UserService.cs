using Scolly.Infrastructure.Data;
using Scolly.Infrastructure.Data.Models;
using Scolly.Services.Data.DTOs;
using Scolly.Services.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scolly.Services.Services
{
    public class UserService : IUserService
    {
        private ApplicationDbContext _context;

        public UserService(ApplicationDbContext context)
        {
            _context = context;
        }

        public Task DeleteUserById(int id)
        {
            throw new NotImplementedException();
        }

        public Task EditUserById(string id, UserDto model)
        {
            throw new NotImplementedException();
        }

        public Task<List<UserDto>> GetAllByName(string name)
        {
            throw new NotImplementedException();
        }

        public Task<List<UserDto>> GetAllUser()
        {
            throw new NotImplementedException();
        }

        public Task<UserDto?> GetUserById(string id)
        {
            throw new NotImplementedException();
        }

        public UserDto MapData(User model)
        {
            throw new NotImplementedException();
        }

        public Task RegitserNewUser(UserDto model)
        {
            throw new NotImplementedException();
        }
    }
}
