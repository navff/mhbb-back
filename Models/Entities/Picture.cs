using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Entities
{
    public class Picture
    {
        public int Id { get; set; }

        public string Description { get; set; }

        [Required]
        [StringLength(10)]
        public string Extension { get; set; }

        public bool IsMain { get; set; }
       

        public int? LinkedObjectId { get; set; }

        public LinkedObjectType LinkedObjectType { get; set; }
    }

    public enum LinkedObjectType
    {
        Activity = 1,
        Organizer = 2
    }
}
