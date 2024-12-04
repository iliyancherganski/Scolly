using Microsoft.EntityFrameworkCore;
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
    public class CourseTypeService : ICourseTypeService
    {
        private readonly ApplicationDbContext _context;

        public CourseTypeService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<CourseTypeDto>> GetAll()
        {
            var courseTypes = await _context.CourseTypes.ToListAsync();
            var courseTypeDtos = new List<CourseTypeDto>();
            foreach (var courseType in courseTypes)
            {
                var courseTypeDto = await MapData(courseType.Id);
                if (courseTypeDto != null)
                {
                    courseTypeDtos.Add(courseTypeDto);
                }
            }
            return courseTypeDtos;
        }

        public async Task<CourseTypeDto?> GetById(int id)
        {
            var courseType = await _context.CourseTypes.FirstOrDefaultAsync(x => x.Id == id);
            if (courseType != null)
            {
                var courseTypeDto = await MapData(courseType.Id);
                if (courseTypeDto != null)
                {
                   return courseTypeDto;
                }
            }
            return null;
        }

        public async Task<List<CourseTypeDto>> GetAllByName(string name)
        {
            var courseTypes = await _context.CourseTypes
                .Where(x => x.Name
                    .ToLower()
                    .Contains(name.ToLower()))
                .ToListAsync();
            var courseTypeDtos = new List<CourseTypeDto>();
            foreach (var courseType in courseTypes)
            {
                var courseTypeDto = await MapData(courseType.Id);
                if (courseTypeDto != null)
                {
                    courseTypeDtos.Add(courseTypeDto);
                }
            }
            return courseTypeDtos;
        }

        public async Task Add(CourseTypeDto model)
        {
            if (model != null && _context.CourseTypes.FirstOrDefault(x => x.Name.ToLower() == model.Name.ToLower()) != null)
            {
                var courseType = new CourseType { Name = model.Name };
                await _context.CourseTypes.AddAsync(courseType);
                await _context.SaveChangesAsync();
            }
        }

        public async Task EditById(int id, CourseTypeDto model)
        {
            var courseTypes = await _context.CourseTypes.FirstOrDefaultAsync(x => x.Id == id);
            if (courseTypes != null && await _context.CourseTypes.FirstOrDefaultAsync(x => x.Name == model.Name) == null)
            {
                courseTypes.Name = model.Name;
                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteById(int id)
        {
            var courseType = await _context.CourseTypes.FirstOrDefaultAsync(x => x.Id == id);
            if (courseType != null && await _context.Courses.Include(x => x.CourseType).FirstOrDefaultAsync(x => x.CourseType == courseType) == null)
            {
                _context.CourseTypes.Remove(courseType);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<CourseTypeDto?> MapData(int modelId)
        {
            var model = await _context.CourseTypes.FirstOrDefaultAsync(x => x.Id == modelId);
            if (model == null)
            {
                return null;
            }

            return new CourseTypeDto() { Id = model.Id, Name = model.Name };
        }
    }
}
