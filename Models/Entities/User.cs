using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using Models.Entities;

namespace API.Models
{
    public class User
    {
        [Key]
        public string Email { get; set; }
        public string AuthToken { get; set; }
        public string Name { get; set; }
        public string Phone { get; set; }

        [ForeignKey("Picture")]
        public int? PictureId { get; set; }
        public virtual Picture Picture { get; set; }

        public Role Role { get; set; }

        [ForeignKey("City")]
        public int? CityId { get; set; }
        public virtual City City { get; set; }
    }

    public enum Role
    {
        PortalAdmin = 0,
        PortalManager = 1,
        RegisteredUser = 2
    }

}