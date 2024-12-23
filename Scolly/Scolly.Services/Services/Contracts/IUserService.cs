﻿using Microsoft.AspNetCore.Identity;
using Scolly.Infrastructure.Data.Models;
using Scolly.Services.Data.DTOs;
using Scolly.Services.Data.Enums;
using System.Security.Claims;

namespace Scolly.Services.Services.Contracts
{
    public interface IUserService
    {
        Task<List<UserDto>> GetAllUsers();
        Task<List<UserDto>> GetAllByName(string name);
        Task<UserDto?> GetUserById(string id);
        Task<string?> RegisterNewUser(UserDto model, string role);
        Task EditUserById(string id, UserDto model);
        Task DeleteUserById(string id);
        Task<UserDto?> MapData(string modelId);
        bool IsSignedIn(ClaimsPrincipal user);
        Task Logout();
        Task<bool> SignIn(string username, string password);
        Task<bool> UserWithEmailExists(string email);
        Task<string> GreetingOfUser(string? email);
    }
}
