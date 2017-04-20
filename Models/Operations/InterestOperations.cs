using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Camps.Tools;
using Models.Entities;

namespace Models.Operations
{
    public class InterestOperations
    {
        private HobbyContext _context;

        public InterestOperations(HobbyContext context)
        {
            _context = context;
        }

        public async Task<Interest> GetAsync(int id)
        {
            try
            {
                return await _context.Interests.FirstOrDefaultAsync(i => i.Id == id);
            }
            catch (Exception ex)
            {
                ErrorLogger.Log("CANNOT GET INTEREST", ex);
                throw;
            }
        }

        public async Task<Interest> UpdateAsync(Interest interest)
        {
            try
            {
                var existingInterest = await _context.Interests.FirstOrDefaultAsync(i => i.Id == interest.Id);
                existingInterest.Name = interest.Name;
                await _context.SaveChangesAsync();
                return existingInterest;
            }
            catch (Exception ex)
            {
                ErrorLogger.Log("CANNOT UPDATE INTEREST", ex);
                throw;
            }
        }

        public async Task<Interest> AddAsync(Interest interest)
        {
            try
            {
                _context.Interests.Add(interest);
                await _context.SaveChangesAsync();
                return interest;
            }
            catch (Exception ex)
            {
                ErrorLogger.Log("CANNOT ADD INTEREST", ex);
                throw;
            }
        }

        public async Task DeleteAsync(int id)
        {
            try
            {
                var interest = await GetAsync(id);
                _context.Interests.Remove(interest);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                ErrorLogger.Log("CANNOT DELETE INTEREST", ex);
                throw;
            }
        }

        public async Task<IEnumerable<Interest>> GetAllAsync()
        {
            try
            {
                return await _context.Interests.ToListAsync();
            }
            catch (Exception ex)
            {
                ErrorLogger.Log("CANNOT GET ALL INTERESTS", ex);
                throw;
            }
        }
    }
}
