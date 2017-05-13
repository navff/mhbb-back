using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using API.Models;

namespace Models.Entities
{
    public class Reservation
    {
        public int Id { get; set; }

        [ForeignKey("Activity")]
        public int ActivityId { get; set; }
        public virtual Activity Activity { get; set; }

        [Index]
        [ForeignKey("User")]
        public string UserEmail { get; set; }
        public virtual User User { get; set; }

        [Index]
        [Column(TypeName = "VARCHAR")]
        [StringLength(255)]
        public string Name { get; set; }

        [Index]
        [Column(TypeName = "VARCHAR")]
        [StringLength(255)]
        public string Phone { get; set; }

        public string Comment { get; set; }


    }
}
