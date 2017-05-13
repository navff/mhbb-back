﻿using System;
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
        [StringLength(maximumLength:255, MinimumLength = 1)]
        public string Email { get; set; }

        [Index]
        [Required]
        [Column(TypeName = "VARCHAR")]
        [StringLength(255)]
        public string AuthToken { get; set; }

        [Index]
        [Column(TypeName = "VARCHAR")]
        [StringLength(255)]
        public string Name { get; set; }

        [Index]
        [Column(TypeName = "VARCHAR")]
        [StringLength(255)]
        public string Phone { get; set; }

        [ForeignKey("Picture")]
        public int? PictureId { get; set; }

        public virtual Picture Picture { get; set; }

        public Role Role { get; set; }

        [ForeignKey("City")]
        public int? CityId { get; set; }
        public virtual City City { get; set; }

        [Required]
        public DateTime DateRegistered { get; set; }
    }

    public enum Role
    {
        PortalAdmin = 0,
        PortalManager = 1,
        RegisteredUser = 2
    }

}