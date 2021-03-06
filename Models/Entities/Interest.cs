﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Entities
{
    public class Interest
    {
        public int Id { get; set; }

        [Required]
        [MinLength(2)]
        public string Name { get; set; }
    }
}
