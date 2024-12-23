﻿using Microsoft.EntityFrameworkCore;
using Scolly.Infrastructure.Data;
using Scolly.Infrastructure.Data.Models;
using Scolly.Services.Data.DTOs;
using Scolly.Services.Data.Enums;
using Scolly.Services.Services.Contracts;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Scolly.Services.Services
{
    public class TeacherService : ITeacherService
    {
        private readonly ApplicationDbContext _context;
        private readonly ISpecialtyService _specialtyService;
        private readonly IUserService _userService;

        public TeacherService(ApplicationDbContext context, ISpecialtyService specialtyService, IUserService userService)
        {
            _context = context;
            _specialtyService = specialtyService;
            _userService = userService;
        }

        public async Task Add(TeacherDto model)
        {
            var teacher = new Teacher();

            var specialties = new List<Specialty>();
            var specialty = new Specialty();
            foreach (var specialtyDto in model.SpecialtyDtos)
            {
                specialty = await _context.Specialties.FirstOrDefaultAsync(x => x.Id == specialtyDto.Id);
                if (specialty != null)
                {
                    specialties.Add(specialty);
                }
            }

            string? userId = await _userService.RegisterNewUser(model.UserDto, "Teacher");
            if (userId == null)
                throw new ArgumentException("Вече има регистриран потребител с този имейл.");
            var user = await _context.Users.FirstOrDefaultAsync(x => x.Id == userId);
            if (user == null) return;

            teacher.UserId = user.Id;
            teacher.User = user;

            await _context.Teachers.AddAsync(teacher);

            foreach (var s in specialties)
            {
                var ts = new TeacherSpecialty()
                {
                    TeacherId = teacher.Id,
                    Teacher = teacher,
                    SpecialtyId = s.Id,
                    Specialty = s
                };
                await _context.TeachersSpecialties.AddAsync(ts);
                teacher.TeacherSpecialties.Add(ts);
            }
            await _context.SaveChangesAsync();
        }

        public async Task DeleteById(int id)
        {
            var teacher = await _context.Teachers.FirstOrDefaultAsync(x => x.Id == id);
            if (teacher == null) return;

            var teacherSpecialties = await _context.TeachersSpecialties.Where(x => x.TeacherId == teacher.Id).ToListAsync();

            foreach (var ts in teacherSpecialties)
            {
                _context.TeachersSpecialties.Remove(ts);
            }

            var teacherCourses = await _context.TeachersCourses
                .Include(x => x.Course)
                .Where(x => x.TeacherId == teacher.Id).ToListAsync();

            foreach (var tc in teacherCourses)
            {
                tc.Course.TeachersCourse.Remove(tc);
                _context.TeachersCourses.Remove(tc);
            }

            string userId = teacher.UserId;
            _context.Teachers.Remove(teacher);
            await _userService.DeleteUserById(userId);
            await _context.SaveChangesAsync();
        }

        public async Task EditById(int id, TeacherDto model)
        {
            var teacher = await _context.Teachers
                .Include(x => x.User)
                .FirstOrDefaultAsync(x => x.Id == id);
            if (teacher == null) return;
            var teacherSpecialties = await _context.TeachersSpecialties.Where(x => x.TeacherId == teacher.Id).ToListAsync();

            foreach (var ts in teacherSpecialties)
            {
                teacher.TeacherSpecialties.Remove(ts);
                _context.TeachersSpecialties.Remove(ts);
            }

            var specialties = new List<Specialty>();
            var specialty = new Specialty();
            foreach (var specialtyDto in model.SpecialtyDtos)
            {
                specialty = await _context.Specialties.FirstOrDefaultAsync(x => x.Id == specialtyDto.Id);
                if (specialty != null)
                {
                    specialties.Add(specialty);
                }
            }

            foreach (var s in specialties)
            {
                var ts = new TeacherSpecialty()
                {
                    TeacherId = teacher.Id,
                    Teacher = teacher,
                    SpecialtyId = s.Id,
                    Specialty = s
                };
                await _context.TeachersSpecialties.AddAsync(ts);
                teacher.TeacherSpecialties.Add(ts);
            }

            await _userService.EditUserById(teacher.User.Id, model.UserDto);

            await _context.SaveChangesAsync();
        }

        public async Task<List<TeacherDto>> GetAll()
        {
            var teachers = await _context.Teachers.ToListAsync();
            var teacherDtos = new List<TeacherDto>();
            var teacherDto = new TeacherDto();
            foreach (var teacher in teachers)
            {
                teacherDto = await MapData(teacher.Id);
                if (teacherDto != null)
                {
                    teacherDtos.Add(teacherDto);
                }
            }
            return teacherDtos;
        }

        public async Task<List<TeacherDto>> GetAllByName(string name)
        {
            var teacherDtos = new List<TeacherDto>();
            var userDtos = await _userService.GetAllByName(name);

            var teacher = new Teacher();
            var teacherDto = new TeacherDto();
            foreach (var userDto in userDtos)
            {
                teacher = await _context.Teachers
                    .Include(x => x.User)
                    .FirstOrDefaultAsync(x => x.UserId == userDto.Id);
                if (teacher != null)
                {
                    teacherDto = await MapData(teacher.Id);
                    if (teacherDto != null)
                    {
                        teacherDtos.Add(teacherDto);
                    }
                }
            }
            return teacherDtos;
        }

        public async Task<List<TeacherDto>> GetAllTeachersByCourse(int courseId)
        {
            var course = await _context.Courses
                .Include(x => x.TeachersCourse)
                .ThenInclude(x => x.Teacher)
                .FirstOrDefaultAsync(x => x.Id == courseId);
            if (course == null)
            {
                return new List<TeacherDto>();
            }

            var dto = new TeacherDto();
            var dtos = new List<TeacherDto>();
            foreach (var teacherId in course.TeachersCourse.Select(x => x.TeacherId))
            {
                dto = await GetById(teacherId);
                if (dto != null)
                {
                    dtos.Add(dto);
                }
            }

            return dtos;
        }

        public async Task<TeacherDto?> GetById(int id)
        {
            var teacher = await _context.Teachers.FirstOrDefaultAsync(x => x.Id == id);
            if (teacher == null) return null;
            var teacherDto = await MapData(teacher.Id);
            return teacherDto;
        }

        public async Task<TeacherDto?> GetTeacherByUserId(string? userId)
        {
            if (userId == null)
            {
                return null;
            }

            var teacher = await _context.Teachers
                .Include(x => x.User)
                .FirstOrDefaultAsync(x => x.UserId == userId);
            if (teacher != null)
            {
                return await MapData(teacher.Id);
            }
            return null;
        }

        public async Task<TeacherDto?> MapData(int modelId)
        {
            var model = await _context.Teachers
                .Include(x => x.TeacherSpecialties)
                .ThenInclude(x => x.Specialty)
                .Include(x => x.TeacherCourses)
                .Include(x => x.User)
                .FirstOrDefaultAsync(x => x.Id == modelId);
            if (model == null) return null;

            var specialtyDtos = new List<SpecialtyDto>();
            var specialtyDto = new SpecialtyDto();
            foreach (var specialty in model.TeacherSpecialties.Select(x => x.Specialty))
            {
                if (specialty != null)
                {
                    specialtyDto = await _specialtyService.MapData(specialty.Id);
                    if (specialtyDto != null)
                    {
                        specialtyDtos.Add(specialtyDto);
                    }
                }
            }

            var userDto = await _userService.MapData(model.User.Id);
            if (userDto == null) { return null; }
            userDto.Role = UserRoles.Teacher;

            var dto = new TeacherDto()
            {
                Id = model.Id,
                SpecialtyDtos = specialtyDtos,
                UserDtoId = userDto.Id,
                UserDto = userDto
            };
            return dto;
        }
    }
}
