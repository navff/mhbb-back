using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Models.Entities;

namespace Models.Operations
{
    public class ReservationOperations
    {
        private HobbyContext _context;
        public ReservationOperations(HobbyContext context)
        {
            _context = context;
        }

        public async Task<Reservation> AddAsync(Reservation reservation)
        {
            throw new NotImplementedException();
        }

        public async Task<Reservation> GetAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<Reservation> UpdateAsync(Reservation reservation)
        {
            throw new NotImplementedException();
        }

        public async Task DeleteAsync(int id)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Reservation>> SearchAsync(string word)
        {
            throw new NotImplementedException();
        }
    }
}
