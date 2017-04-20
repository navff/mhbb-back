using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace API.ViewModels
{
    public class InterestViewModelGet
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class InterestViewModelPost
    {
        [Required]
        public string Name { get; set; }
    }
}