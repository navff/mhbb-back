using System;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using API.Models;
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

        public async Task<User> GetUserByTokenAsync(string token)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.AuthToken == token);
        }

        public async Task<User> GetAsync(string email)
        {
            throw new NotImplementedException();
        }

        public async Task<User> UpdateAsync(User user)
        {
            throw new NotImplementedException();
        }

        public async Task<User> AddAsync(User user)
        {
            throw new NotImplementedException();
        }

        public async Task DeleteAsync(string email)
        {
            throw new NotImplementedException();
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