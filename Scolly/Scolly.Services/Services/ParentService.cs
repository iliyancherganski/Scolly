using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Scolly.Infrastructure.Data;
using Scolly.Infrastructure.Data.Models;
using Scolly.Services.Data.DTOs;
using Scolly.Services.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace Scolly.Services.Services
{
    public class ParentService : IParentService
    {
        // ctros when done
        private ApplicationDbContext _context;
        private ChildService _childService;
        //private UserService _userService;


        public async Task<List<ParentDto>> GetAll()
        {
            var parents = await _context.Parents
                .Include(x => x.User)
                .Include(x => x.Children)
                .ToListAsync();

            var parentDtos = parents.Select(MapData).ToList();
            return parentDtos;
        }

        public async Task<List<ParentDto>> GetAllByBame(string name)
        {
            if (!string.IsNullOrEmpty(name) && !string.IsNullOrWhiteSpace(name))
            {
                string tempName = string.Empty;
                for (int i = 0; i < name.Length; i++)
                {
                    if (name[i] != ' ')
                    {
                        tempName += name[i];
                    }
                }
                tempName = tempName.ToLower();

                var dtos = await GetAll();

                return dtos.Where(x => $"{x.UserDto.FirstName}{x.UserDto.LastName}".ToLower().Contains(tempName)).ToList();
            }
            return new List<ParentDto> { };
        }

        public async Task<List<ChildDto>> GetAllChildren(int parentId)
        {
            var parentDto = await GetById(parentId);
            if (parentDto != null)
                return parentDto.ChildDtos;
            return new List<ChildDto> { };
        }

        public async Task<ParentDto?> GetById(int id)
        {
            var parentDtos = await GetAll();
            return parentDtos.FirstOrDefault(x => x.Id == id);
        }

        public Task Add(ParentDto model)
        {
            throw new NotImplementedException();
        }

        public Task DeleteById(int id)
        {
            throw new NotImplementedException();
        }

        public Task EditById(int id, ParentDto model)
        {
            throw new NotImplementedException();
        }

        public ParentDto MapData(Parent model)
        {
            var dto = new ParentDto
            {
                Id = model.Id,
                UserDtoId = model.UserId,
                //UserDto = _userService.MapData(model.User)
                ChildDtos = model.Children.Select(_childService.MapData).ToList(),
            };

            return dto;
        }
    }
}
