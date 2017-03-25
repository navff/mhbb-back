using System;
using System.Collections.Generic;
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

        [ForeignKey("User")]
        public int UserId { get; set; }
        public virtual User User { get; set; }

        public string Name { get; set; }

        public string Phone { get; set; }

        public string Email { get; set; }

        public string Comments { get; set; }


    }
}
