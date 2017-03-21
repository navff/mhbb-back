using System;
using API.Models;
using Models;

namespace API.Operations
{
    public class UserOperations : IDisposable
    {
        private AppContext _context;
        private bool _externalContext = false;

        public UserOperations()
        {
            _context  = new AppContext();
        }

        public UserOperations(AppContext context)
        {
            _context = context;
            _externalContext = true;
        }

        public User GetUserByToken(string token)
        {
            //TODO: найти пользователя по токену в реальном хранилище
            if (token == "ABRAKADABRA")
            {
                return new User {AuthToken = token, Email = "var@33kita.ru", Role = Role.PortalAdmin};
            }
            else
            {
                return null;
            }
        }

        public void Dispose()
        {
            if (_externalContext)
            {
                _context?.Dispose();
            }
            
        }
    }
}