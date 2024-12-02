using Microsoft.EntityFrameworkCore;
using Scolly.Infrastructure.Data;
using Scolly.Infrastructure.Data.Models;
using Scolly.Services.Data.DTOs;
using Scolly.Services.Services.Interfaces;

namespace Scolly.Services.Services
{
    public class AgeGroupService : IAgeGroupService
    {
        private ApplicationDbContext _context;

        public AgeGroupService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<AgeGroupDto>> GetAll()
        {
            var ageGroups = await _context.AgeGroups.ToListAsync();
            var ageGroupDtos = new List<AgeGroupDto>();
            foreach (var ageGroup in ageGroups)
            {
                ageGroupDtos.Add(MapData(ageGroup));
            }
            return ageGroupDtos;
        }

        public async Task<AgeGroupDto?> GetById(int id)
        {
            var ageGroup = await _context.AgeGroups.FirstOrDefaultAsync(x => x.Id == id);
            if (ageGroup != null)
            {
                return MapData(ageGroup);
            }
            return null;
        }

        public async Task<List<AgeGroupDto>> GetAllByBame(string name)
        {
            var ageGroups = await _context.AgeGroups
                .Where(x => x.Name
                    .ToLower()
                    .Contains(name.ToLower()))
                .ToListAsync();
            var ageGroupDtos = new List<AgeGroupDto>();
            foreach (var ageGroup in ageGroups)
            {
                ageGroupDtos.Add(MapData(ageGroup));
            }
            return ageGroupDtos;
        }

        public async Task Add(AgeGroupDto model)
        {
            if (model != null && _context.AgeGroups.FirstOrDefault(x=>x.Name.ToLower() == model.Name.ToLower()) != null)
            {
                var ageGoup = new AgeGroup {  Name = model.Name };
                await _context.AgeGroups.AddAsync(ageGoup);
                await _context.SaveChangesAsync();
            }
        }

        public async Task EditById(int id, AgeGroupDto model)
        {
            var ageGroup = await _context.AgeGroups.FirstOrDefaultAsync(x => x.Id == id);
            if (ageGroup != null)
            {
                ageGroup.Name = model.Name;
                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteById(int id)
        {
            var ageGroup = await _context.AgeGroups.FirstOrDefaultAsync(x => x.Id == id);
            if (ageGroup != null)
            {
                _context.AgeGroups.Remove(ageGroup);
                await _context.SaveChangesAsync();
            }
        }

        public AgeGroupDto MapData(AgeGroup model)
        {
            return new AgeGroupDto { Id = model.Id, Name = model.Name };
        }
    }
}
