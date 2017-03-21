using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace API.Models
{
    public class User
    {
        [Key]
        public string Email { get; set; }
        public string AuthToken { get; set; }
        public Role Role { get; set; }
    }

    public enum Role
    {
        PortalAdmin = 0,
        PortalManager = 1,
        RegisteredUser = 2
    }

}