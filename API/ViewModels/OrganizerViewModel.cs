using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API.ViewModels
{
    /// <summary>
    /// Организатор
    /// </summary>
    public class OrganizerViewModelGet
    {
        /// <summary>
        /// ID
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Название
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// ID города
        /// </summary>
        public int CityId { get; set; }

        /// <summary>
        /// Город полностью
        /// </summary>
        public virtual CityViewModelGet City { get; set; }

        /// <summary>
        /// Трезвость (преподаватели вообще не пьют и не курят)
        /// </summary>
        public bool Sobriety { get; set; }
    }

    /// <summary>
    /// Организатор
    /// </summary>
    public class OrganizerViewModelPost
    {
        /// <summary>
        /// Название
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Город
        /// </summary>
        public int CityId { get; set; }

        /// <summary>
        /// Трезвость
        /// </summary>
        public bool Sobriety { get; set; }
    }
}