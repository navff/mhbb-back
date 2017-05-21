using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using API.Operations;
using Camps.Tools;
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
            try
            {
                UserOperations userOperations = new UserOperations(_context);
                var user = await userOperations.GetAsync(reservation.UserEmail);
                if (user == null)
                {
                    await userOperations.RegisterAsync(reservation.UserEmail);
                }

                reservation.Created = DateTime.Now;
                _context.Reservations.Add(reservation);
                await _context.SaveChangesAsync();
                return await GetAsync(reservation.Id);
            }
            catch (Exception ex)
            {
                ErrorLogger.Log("CANNOT ADD RESERVATION", ex);
                throw;
            }
        }

        public async Task<Reservation> GetAsync(int id)
        {
            try
            {
                return await _context.Reservations.Include(r => r.User)
                                                  .Include(r => r.Activity.ActivityUserVoices)
                                                  .FirstOrDefaultAsync(r => r.Id == id);
            }
            catch (Exception ex)
            {
                ErrorLogger.Log("CANNOT GET RESERVATION", ex);
                throw;
            }
        }

        public async Task<Reservation> UpdateAsync(Reservation reservation)
        {
            try
            {
                Contracts.Assert(reservation.Id!=0);
                var dbEntity = await _context.Reservations.FindAsync(reservation.Id);
                Contracts.Assert(dbEntity!=null);

                dbEntity.Name = reservation.Name;
                dbEntity.ActivityId = reservation.ActivityId;
                dbEntity.Comment = reservation.Comment;
                dbEntity.Phone = reservation.Phone;
                dbEntity.UserEmail = reservation.UserEmail;

                await _context.SaveChangesAsync();
                return dbEntity;
            }
            catch (Exception ex)
            {
                ErrorLogger.Log("CANNOT UPDATE RESERVATION", ex);
                throw;
            }
        }

        public async Task DeleteAsync(int id)
        {
            try
            {
                Contracts.Assert("ID CANNOT BE ZERO", id != 0);
                var reservation = await _context.Reservations.FirstAsync(r => r.Id == id);
                _context.Reservations.Remove(reservation);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                ErrorLogger.Log("CANNOT DELETE RESERVATION", ex);
                throw;
            }
        }

        /// <summary>
        /// Поиск по телефону, имени, email
        /// </summary>
        /// <param name="word"></param>
        /// <returns></returns>
        public async Task<IEnumerable<Reservation>> SearchAsync(string word)
        {
            try
            {
                return await _context.Reservations.Where(r => (r.UserEmail.Contains(word))
                                                                  ||(r.User.Name.Contains(word))
                                                                  ||(r.Phone.Contains(word))
                                                                  ||(r.Name.Contains(word))).ToListAsync();
            }
            catch (Exception ex)
            {
                ErrorLogger.Log("CANNOT SEARCH RESERVATION", ex);
                throw;
            }
        }
    }
}
