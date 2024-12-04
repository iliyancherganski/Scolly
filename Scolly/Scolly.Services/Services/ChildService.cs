﻿using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.Json;
using Scolly.Infrastructure.Data;
using Scolly.Infrastructure.Data.Enums;
using Scolly.Infrastructure.Data.Models;
using Scolly.Services.Data.DTOs;
using Scolly.Services.Services.Contracts;
using System.Runtime.ExceptionServices;

namespace Scolly.Services.Services
{
    public class ChildService : IChildService
    {
        private readonly ApplicationDbContext _context;
        private readonly IParentService _parentService;
        private readonly ICourseRequestService _courseRequestService;

        public ChildService(ApplicationDbContext context, IParentService parentService, ICourseRequestService courseRequestService)
        {
            _context = context;
            _parentService = parentService;
            _courseRequestService = courseRequestService;
        }

        public async Task Add(ChildDto model)
        {
            var parent = await _context.Parents.FirstOrDefaultAsync(x => x.Id == model.ParentDtoId);
            if (parent != null)
            {
                var child = new Child()
                {
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    ParentId = parent.Id,
                    Parent = parent,
                    PhoneNumber = model.PhoneNumber,
                    CourseRequests = new List<CourseRequest>()
                };
                parent.Children.Add(child);
                await _context.Children.AddAsync(child);
                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteById(int id)
        {
            var child = await _context.Children
                .Include(x => x.Parent)
                .ThenInclude(x => x.User)
                .Include(x => x.CourseRequests)
                .ThenInclude(x => x.Select(x => x.Course))
                .FirstOrDefaultAsync(x => x.Id == id);
            if (child != null)
            {
                var parent = child.Parent;
                parent.Children.Remove(child);
                foreach (var course in child.CourseRequests.Select(x => x.Course))
                {
                    await UnregisterChildToCourse(child.Id, course.Id);
                }
                await _context.SaveChangesAsync();
            }
        }

        public async Task EditById(int id, ChildDto model)
        {
            var child = await _context.Children
               .Include(x => x.Parent)
               .ThenInclude(x => x.User)
               .Include(x => x.CourseRequests)
               .ThenInclude(x => x.Select(x => x.Course))
               .FirstOrDefaultAsync(x => x.Id == id);
            if (child != null)
            {
                child.FirstName = model.FirstName;
                child.LastName = model.LastName;
                child.PhoneNumber = model.PhoneNumber;

                await _context.SaveChangesAsync();
            }

        }

        public async Task<List<ChildDto>> GetAll()
        {
            var children = await _context.Children.ToListAsync();
            var childDtos = new List<ChildDto>();
            var childDto = new ChildDto();
            foreach (var child in children)
            {
                childDto = await MapData(child.Id);
                if (childDto != null)
                {
                    childDtos.Add(childDto);
                }
            }
            return childDtos;
        }

        public async Task<List<ChildDto>> GetAllByName(string name)
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

                return dtos.Where(x => $"{x.FirstName}{x.LastName}".ToLower().Contains(tempName)).ToList();
            }
            return [];
        }

        public async Task<List<CourseDto>> GetAllSignedUpCourses(int childId)
        {
            var child = await _context.Children
               .FirstOrDefaultAsync(x => x.Id == childId);

            var courseDtos = new List<CourseDto>();

            if (child != null)
            {
                var courseRequestDtos = await GetAllRequests(child.Id);


                foreach (var courseRequestDto in courseRequestDtos)
                {
                    courseDtos.Add(courseRequestDto.CourseDto);
                }
            }

            return courseDtos;
        }

        public async Task<List<CourseRequestDto>> GetAllRequests(int childId)
        {
            var child = await _context.Children
               .FirstOrDefaultAsync(x => x.Id == childId);

            var courseRequestDtos = new List<CourseRequestDto>();
            if (child == null) return courseRequestDtos;

            courseRequestDtos = await _courseRequestService.GetAll();
            courseRequestDtos = courseRequestDtos.Where(x => x.ChildDtoId == child.Id).ToList();
            return courseRequestDtos;
        }

        public async Task<ChildDto?> GetById(int id)
        {
            var dtos = await GetAll();
            return dtos.FirstOrDefault(x => x.Id == id);
        }

        public async Task<ParentDto?> GetParent(int childId)
        {
            var dto = await GetById(childId);
            if (dto == null)
            {
                return null;
            }
            return dto.ParentDto;
        }

        public async Task ManageChildRequestToCourse(int childId, int courseId, bool isAccepted)
        {
            var child = await _context.Children
              .Include(x => x.CourseRequests)
              .FirstOrDefaultAsync(x => x.Id == childId);

            var course = await _context.Courses
                .Include(x => x.CourseRequests)
                .FirstOrDefaultAsync(x => x.Id == courseId);

            if (child != null && course != null)
            {
                var courseRequest = await _context.CourseRequests.FirstOrDefaultAsync(x => x.Course == course && x.Child == child);
                if (courseRequest != null)
                {
                    if (isAccepted)
                        courseRequest.Status = RequestStatus.Accepted;
                    else
                        courseRequest.Status = RequestStatus.Rejected;

                    await _context.SaveChangesAsync();
                }
            }
        }

        public async Task<ChildDto?> MapData(int modelId)
        {
            var model = await _context.Children
                .Include(x => x.Parent)
                .ThenInclude(x => x.User)
                .ThenInclude(x => x.City)
                .FirstOrDefaultAsync(x => x.Id == modelId);

            if (model == null)
            {
                return null;
            }

            var dto = new ChildDto
            {
                Id = model.Id,
                FirstName = model.FirstName,
                LastName = model.LastName,
                PhoneNumber = model.PhoneNumber,
            };

            var parent = await _context.Parents.FirstOrDefaultAsync(x => x.Id == model.ParentId);
            if (parent != null)
            {
                var parentDto = await _parentService.MapData(model.Parent.Id);
                if (parentDto != null)
                {
                    dto.ParentDtoId = model.ParentId;
                    dto.ParentDto = parentDto;
                }
            }

            return dto;
        }

        public async Task RequestChildRegisterToCourse(int childId, int courseId)
        {
            var child = await _context.Children
              .Include(x => x.CourseRequests)
              .FirstOrDefaultAsync(x => x.Id == childId);

            var course = await _context.Courses
                .Include(x => x.CourseRequests)
                .FirstOrDefaultAsync(x => x.Id == courseId);

            if (child != null && course != null)
            {
                var courseRequest = await _context.CourseRequests.FirstOrDefaultAsync(x => x.Course == course && x.Child == child);
                if (courseRequest == null)
                {
                    courseRequest = new CourseRequest()
                    {
                        ChildId = childId,
                        Child = child,
                        CourseId = courseId,
                        Course = course,
                        Status = RequestStatus.Pending
                    };
                    await _context.CourseRequests.AddAsync(courseRequest);
                    await _context.SaveChangesAsync();
                }
            }
        }

        public async Task UnregisterChildToCourse(int childId, int courseId)
        {
            var courseRequest = await _context.CourseRequests.FirstOrDefaultAsync(x => x.ChildId == childId && x.CourseId == courseId);
            if (courseRequest != null)
            {
                await _courseRequestService.DeleteById(courseRequest.Id);
            }
        }
    }
}
