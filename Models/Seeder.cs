using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using API.Models;

namespace Models
{
    public static class Seeder
    {
        public static void Seed(AppContext context)
        {
            if (!context.Users.Any())
            {
                context.Users.Add(new User()
                {
                    AuthToken = "ABRAKADABRA",
                    Email = "var@33kita.ru",
                    Role = Role.PortalAdmin
                });
                context.SaveChanges();
            }
        }
    }
}
