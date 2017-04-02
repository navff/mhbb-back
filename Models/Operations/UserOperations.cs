using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using API.Models;
using Camps.Tools;
using Models;

namespace API.Operations
{
    public class UserOperations : IDisposable
    {
        private AppContext _context;
        private bool _isExternalContext = false;

        public UserOperations()
        {
            _context  = new AppContext();
        }

        public UserOperations(AppContext context)
        {
            _context = context;
            _isExternalContext = true;
        }

        /// <summary>
        /// Получение пользователя по токену
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task<User> GetUserByTokenAsync(string token)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.AuthToken == token);
        }

        /// <summary>
        /// Получение пользователя по электронной почте
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public async Task<User> GetAsync(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        }

        /// <summary>
        /// Обновление пользователя
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public async Task<User> UpdateAsync(User user)
        {
            var userInDb = await _context.Users.FindAsync(user.Email);
            Contracts.Assert(userInDb!=null);

            userInDb.Name = user.Name;
            userInDb.CityId = user.CityId;
            userInDb.Phone = user.Phone;
            userInDb.PictureId = user.PictureId;

            await _context.SaveChangesAsync();
            return userInDb;
        }

        public async Task<User> AddAsync(User user)
        {
            Contracts.Assert(user!=null);
            Contracts.Assert(!String.IsNullOrEmpty(user.AuthToken));
            Contracts.Assert(!String.IsNullOrEmpty(user.Email));
            Contracts.Assert(!String.IsNullOrEmpty(user.Phone));

            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task DeleteAsync(string email)
        {
            var user = _context.Users.Find(email);
            Contracts.Assert(user!=null);

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
        }

        public async Task RegisterAsync(string email)
        {
            throw new NotImplementedException();
        }

        public async Task<IEquatable<User>> SearchAsync(string word)
        {
            throw new NotImplementedException();
        }



        public void Dispose()
        {
            if (_isExternalContext)
            {
                _context?.Dispose();
            }
        }


    }
}