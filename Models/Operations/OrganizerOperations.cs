using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using Camps.Tools;
using Models.Entities;
using Models.Tools;

namespace Models.Operations
{
    

    public class OrganizerOperations
    {
        private HobbyContext _context;

        public OrganizerOperations(HobbyContext context)
        {
            _context = context;
        }

        public async Task<Organizer> GetAsync(int organizerId)
        {
            return await _context.Organizers
                                 .Include(o => o.City)
                                 .FirstOrDefaultAsync(o => o.Id == organizerId);
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
            orgInDb.Email = organizer.Email;
            orgInDb.Phone = organizer.Phone;

            await _context.SaveChangesAsync();
            return orgInDb;
        }

        public async Task DeleteAsync(int organizerId)
        {
            var org = await _context.Organizers.FirstAsync(o => o.Id == organizerId);
            _context.Organizers.Remove(org);
            await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<Organizer>> SearchAsync(int? cityId, String word="", int page=1)
        {

            Contracts.Assert(page >= 1);

            var result = _context.Organizers.Include(o => o.City);

            if (!String.IsNullOrEmpty(word))
            {
                result = result.Where(o => o.Name.Contains(word));
            }

            if (cityId != null)
            {
                result = result.Where(o => o.CityId == cityId.Value);
            }

            result =  result.OrderBy(o => o.Name)
                            .Skip((page - 1) * ModelsSettings.PAGE_SIZE)
                            .Take(ModelsSettings.PAGE_SIZE);

            return await result.ToListAsync();
        }

    }
}
