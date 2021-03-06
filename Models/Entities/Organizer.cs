﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Entities
{
    public class Organizer
    {
        [Key]
        public int Id { get; set; }

        [Index]
        [StringLength(150)]
        [Required]
        public string Name { get; set; }

        [ForeignKey("City")]
        public int CityId { get; set; }
        public virtual City City { get; set; }

        public bool Sobriety { get; set; }

        public string Email { get; set; }

        public string Phone { get; set; }
    }
}
