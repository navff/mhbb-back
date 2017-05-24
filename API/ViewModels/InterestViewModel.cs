using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace API.ViewModels
{
    /// <summary>
    /// Интерес. По сути, это категория активностей. Типа «Наука и техника», «рукоделие» и т.д.
    /// </summary>
    public class InterestViewModelGet
    {
        /// <summary>
        /// ID
        /// </summary>
        public int Id { get; set; }
        
        /// <summary>
        /// Название
        /// </summary>
        public string Name { get; set; }
    }

    /// <summary>
    /// Интерес. По сути, это категория активностей. Типа «Наука и техника», «рукоделие» и т.д.
    /// </summary>
    public class InterestViewModelPost
    {
        /// <summary>
        /// Название
        /// </summary>
        [Required]
        public string Name { get; set; }
    }
}