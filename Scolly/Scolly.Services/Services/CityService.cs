using Microsoft.EntityFrameworkCore;
using Scolly.Infrastructure.Data;
using Scolly.Infrastructure.Data.Models;
using Scolly.Services.Data.DTOs;
using Scolly.Services.Services.Contracts;

namespace Scolly.Services.Services
{
    public class CityService : ICityService
    {
        private readonly ApplicationDbContext _context;

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
                var cityDto = await MapData(city.Id);
                if (cityDto != null)
                {
                    cityDtos.Add(cityDto);
                }
            }
            return cityDtos;
        }

        public async Task<CityDto?> GetById(int id)
        {
            var city = await _context.Cities.FirstOrDefaultAsync(x => x.Id == id);
            if (city != null)
            {
                var cityDto = await MapData(city.Id);
                if (cityDto != null)
                {
                    return cityDto;
                }
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
                var cityDto = await MapData(city.Id);
                if (cityDto != null)
                {
                    cityDtos.Add(cityDto);
                }
            }
            return cityDtos;
        }

        public async Task Add(CityDto model)
        {
            if (await _context.Cities.FirstOrDefaultAsync(x => x.Name.ToLower() == model.Name.ToLower()) == null)
            {
                var city = new City { Name = model.Name };
                await _context.Cities.AddAsync(city);
                await _context.SaveChangesAsync();
                return;
            }
            throw new ArgumentException($"Вече съществува град с даденото име - '{model.Name}'.");
        }

        public async Task EditById(int id, CityDto model)
        {
            var city = await _context.Cities.FirstOrDefaultAsync(x => x.Id == id);
            if (city != null && await _context.Cities.FirstOrDefaultAsync(x => x.Name == model.Name) == null)
            {
                city.Name = model.Name;
                await _context.SaveChangesAsync();
                return;
            }
            throw new ArgumentException($"Вече съществува град с даденото име - '{model.Name}'.");
        }

        public async Task DeleteById(int id)
        {
            var city = await _context.Cities.FirstOrDefaultAsync(x => x.Id == id);
            if (city != null)
            {
                if (await _context.Users.Include(x => x.City).FirstOrDefaultAsync(x => x.City == city) == null)
                {
                    _context.Cities.Remove(city);
                    await _context.SaveChangesAsync();
                    return;
                }
                throw new ArgumentException($"Град {city.Name} не беше изтрит, защото същестува потребител, който е регистриран с този град.");
            }
        }

        public async Task<CityDto?> MapData(int modelId)
        {
            var model = await _context.Cities.FirstOrDefaultAsync(x => x.Id == modelId);
            if (model == null)
            {
                return null;
            }

            return new CityDto { Id = model.Id, Name = model.Name };
        }
    }
}
