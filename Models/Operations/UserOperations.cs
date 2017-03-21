using System;
using System.Linq;
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

        public User GetUserByToken(string token)
        {
            return _context.Users.FirstOrDefault(u => u.AuthToken == token);
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