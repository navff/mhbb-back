using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models.Entities;

namespace Models.Operations
{
    public class CityOperations
    {
        private AppContext _context;

        public CityOperations(AppContext context)
        {
            _context = context;
        }

        public async Task<City> GetAsync(int id)
        {
            return await _context.Cities.FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<City> UpdateAsync(City city)
        {
            var cityFromDb = await _context.Cities.FirstOrDefaultAsync(c => c.Id == city.Id);
            cityFromDb.Name = city.Name;
            await _context.SaveChangesAsync();
            return cityFromDb;
        }

        public async Task<City> AddAsync(City city)
        {
            _context.Cities.Add(city);
            await _context.SaveChangesAsync();
            return city;
        }

        public async Task DeleteAsync(int id)
        {
            var city = await _context.Cities.FirstAsync(c => c.Id == id);
            _context.Cities.Remove(city);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<City>> SearchAsync(string word)
        {
            return await _context.Cities.Where(c => c.Name.Contains(word)).ToListAsync();
        }

    }
}
