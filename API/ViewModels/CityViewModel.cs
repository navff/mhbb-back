using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API.ViewModels
{
    public class CityViewModelGet
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

    public class CityViewModelPost
    {
        public string Name { get; set; }
    }

}