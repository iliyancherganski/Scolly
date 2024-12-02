using Microsoft.EntityFrameworkCore;
using Scolly.Infrastructure.Data;
using Scolly.Infrastructure.Data.Models;
using Scolly.Services.Data.DTOs;
using Scolly.Services.Services.Contracts;

namespace Scolly.Services.Services
{
    public class CityService : ICityService
    {
        private ApplicationDbContext _context;

        public CityService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<CityDto>> GetAll()
        {
            var cities = await _context.Cities.ToListAsync();
            var cityDtos = new List<CityDto>();
            foreach (var city in cities)
            {
                cityDtos.Add(MapData(city));
            }
            return cityDtos;
        }

        public async Task<CityDto?> GetById(int id)
        {
            var city = await _context.Cities.FirstOrDefaultAsync(x => x.Id == id);
            if (city != null)
            {
                return MapData(city);
            }
            return null;
        }

        public async Task<List<CityDto>> GetAllByName(string name)
        {
            var cities = await _context.Cities
                .Where(x => x.Name
                    .ToLower()
                    .Contains(name.ToLower()))
                .ToListAsync();
            var cityDtos = new List<CityDto>();
            foreach (var city in cities)
            {
                cityDtos.Add(MapData(city));
            }
            return cityDtos;
        }

        public async Task Add(CityDto model)
        {
            if (model != null && _context.Cities.FirstOrDefault(x => x.Name.ToLower() == model.Name.ToLower()) != null)
            {
                var city = new City { Name = model.Name };
                await _context.Cities.AddAsync(city);
                await _context.SaveChangesAsync();
            }
        }

        public async Task EditById(int id, CityDto model)
        {
            var city = await _context.Cities.FirstOrDefaultAsync(x => x.Id == id);
            if (city != null && await _context.Cities.FirstOrDefaultAsync(x=>x.Name == model.Name) == null)
            {

                city.Name = model.Name;
                await _context.SaveChangesAsync();
            }
        }

        public async Task DeleteById(int id)
        {
            var city = await _context.Cities.FirstOrDefaultAsync(x => x.Id == id);
            if (city != null && await _context.Users.Include(x=>x.City).FirstOrDefaultAsync(x => x.City == city) == null)
            {
                _context.Cities.Remove(city);
                await _context.SaveChangesAsync();
            }
        }

        public CityDto MapData(City model)
        {
            return new CityDto { Id = model.Id, Name = model.Name };
        }
    }
}
