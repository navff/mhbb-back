using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using API.Operations;
using Camps.Tools;
using Models.Entities;
using Models.Tools;

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
                var user = await userOperations.GetAsync(reservation.UserId);
                if (user == null)
                {
                    await userOperations.RegisterAsync(reservation.User.Email);
                }

                reservation.Created = DateTime.Now;
                _context.Reservations.Add(reservation);
                await _context.SaveChangesAsync();
                var newReservation = await this.GetAsync(reservation.Id);
                SendEmail(newReservation);
                return newReservation;
            }
            catch (Exception ex)
            {
                ErrorLogger.Log("CANNOT ADD RESERVATION", ex);
                throw;
            }
        }

        private static bool SendEmail(Reservation reservation)
        {
            var dto = new EmailMessage()
            {
                From = "Моё хобби <site@mhbb.ru>",
                To = reservation.Activity.Organizer.Email,
                Body = GenerateNotificationEmailBody(reservation),
                EmailSubject = UserMessages.SubjectReservationCreated + $" [{reservation.Id}]"
            };
            return EmailService.SendEmail(dto);
        }

        private static string GenerateNotificationEmailBody(Reservation reservation)
        {
            var s = new StringBuilder();
            s.Append("Здравствуйте!<br/>");
            s.Append($"На сайте «Моё хобби» к вашему мероприятию «{reservation.Activity.Name}» проявили интерес. <br/><br/>");
            s.Append($"Время: {reservation.Created.ToString("G")} <br/>");
            s.Append($"Имя: {reservation.Name} <br/>");
            s.Append($"Телефон: {reservation.Phone} <br/>");
            s.Append($"Почта: {reservation.User.Email} <br/><br/>");
            s.Append($"Комментарий:<br/>");
            s.Append($"{reservation.Comment} <br/>");
            return s.ToString();
        }

        public async Task<Reservation> GetAsync(int id)
        {
            try
            {
                return await _context.Reservations.Include(r => r.User)
                                                  .Include(r => r.Activity.ActivityUserVoices)
                                                  .Include(r => r.Activity.Organizer)
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
                dbEntity.UserId = reservation.UserId;

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
                return await _context.Reservations.Where(r => (r.User.Email.Contains(word))
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
