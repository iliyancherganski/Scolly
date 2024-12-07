using Microsoft.EntityFrameworkCore;
using Scolly.Infrastructure.Data;
using Scolly.Infrastructure.Data.Models;
using Scolly.Services.Data.DTOs;
using Scolly.Services.Services.Contracts;

namespace Scolly.Services.Services
{
    public class SpecialtyService : ISpecialtyService
    {
        private readonly ApplicationDbContext _context;

        public SpecialtyService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<SpecialtyDto>> GetAll()
        {
            var specialties = await _context.Specialties.ToListAsync();
            var specialtyDtos = new List<SpecialtyDto>();
            foreach (var specialty in specialties)
            {
                var specialtyDto = await MapData(specialty.Id);
                if (specialtyDto != null)
                {
                    specialtyDtos.Add(specialtyDto);
                }
            }
            return specialtyDtos;
        }
        public async Task<SpecialtyDto?> GetById(int id)
        {
            var specialty = await _context.Specialties.FirstOrDefaultAsync(x => x.Id == id);
            if (specialty != null)
            {
                var specialtyDto = await MapData(specialty.Id);
                if (specialtyDto != null)
                {
                    return specialtyDto;
                }

            }
            return null;
        }

        public async Task<List<SpecialtyDto>> GetAllByName(string name)
        {
            var specialties = await _context.Specialties
                .Where(x => x.Name
                    .ToLower()
                    .Contains(name.ToLower()))
                .ToListAsync();
            var specialtyDtos = new List<SpecialtyDto>();
            foreach (var specialty in specialties)
            {
                var specialtyDto = await MapData(specialty.Id);
                if (specialtyDto != null)
                {
                    specialtyDtos.Add(specialtyDto);
                }
            }
            return specialtyDtos;
        }

        public async Task Add(SpecialtyDto model)
        {
            if (_context.Specialties.FirstOrDefault(x => x.Name.ToLower() == model.Name.ToLower()) == null)
            {
                var specialty = new Specialty { Name = model.Name };
                await _context.Specialties.AddAsync(specialty);
                await _context.SaveChangesAsync();
                return;
            }
            throw new ArgumentException($"Вече съществува специалност с даденото име - '{model.Name}'.");
        }

        public async Task EditById(int id, SpecialtyDto model)
        {
            var specialty = await _context.Specialties.FirstOrDefaultAsync(x => x.Id == id);
            if (specialty != null)
            {
                if (await _context.Specialties.FirstOrDefaultAsync(x => x.Name == model.Name) == null)
                {
                    specialty.Name = model.Name;
                    await _context.SaveChangesAsync();
                    return;
                }
                throw new ArgumentException($"Вече съществува специалност с даденото име - '{model.Name}'.");
            }
        }

        public async Task DeleteById(int id)
        {
            var specialty = await _context.Specialties.FirstOrDefaultAsync(x => x.Id == id);
            if (specialty != null)
            {
                var teacherSpecialty = new TeacherSpecialty();
                foreach (var teacher in _context.Teachers.Include(x => x.TeacherSpecialties).ThenInclude(x => x.Specialty))
                {
                    teacherSpecialty = teacher.TeacherSpecialties.FirstOrDefault(x => x.Specialty == specialty);
                    if (teacherSpecialty != null)
                    {
                        teacher.TeacherSpecialties.Remove(teacherSpecialty);
                    }
                }

                _context.Specialties.Remove(specialty);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<SpecialtyDto?> MapData(int modelId)
        {
            var model = await _context.Specialties.FirstOrDefaultAsync(x => x.Id == modelId);
            if (model == null)
            {
                return null;
            }
            return new SpecialtyDto { Id = model.Id, Name = model.Name, };
        }
    }
}
