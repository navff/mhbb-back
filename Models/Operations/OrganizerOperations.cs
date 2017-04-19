using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Camps.Tools;
using Models.Entities;
using Models.Tools;

namespace Models.Operations
{
    

    public class OrganizerOperations
    {
        private AppContext _context;

        public OrganizerOperations(AppContext context)
        {
            _context = context;
        }

        public async Task<Organizer> GetAsync(int organizerId)
        {
            return await _context.Organizers.FirstOrDefaultAsync(o => o.Id == organizerId);
        }

        public async Task<Organizer> AddAsync(Organizer organizer)
        {
            _context.Organizers.Add(organizer);
            await _context.SaveChangesAsync();
            return organizer;
        }

        public async Task<Organizer> UpdateAsync(Organizer organizer)
        {
            var orgInDb = await _context.Organizers
                                    .Include(o => o.City)
                                    .FirstAsync(o => o.Id == organizer.Id);

            orgInDb.Name = organizer.Name;
            orgInDb.CityId = organizer.CityId;
            orgInDb.Sobriety = organizer.Sobriety;

            await _context.SaveChangesAsync();
            return orgInDb;
        }

        public async Task DeleteAsync(int organizerId)
        {
            var org = await _context.Organizers.FirstAsync(o => o.Id == organizerId);
            _context.Organizers.Remove(org);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Organizer>> SearchAsync(string word)
        {
            var result = new List<Organizer>();
            var orgsByName = await _context.Organizers.Where(o => o.Name.Contains(word)).ToListAsync();
            result.AddRange(orgsByName);
            return result;
        }

        public async Task<IEnumerable<Organizer>> GetAllAsync(int page=1)
        {
            Contracts.Assert(page>=1);

            var result = await _context.Organizers
                                    .OrderBy(o => o.Name)
                                    .Skip((page-1)*ModelsSettings.PAGE_SIZE)
                                    .Take(ModelsSettings.PAGE_SIZE)
                                    .ToListAsync();
            return result;
        }

    }
}
