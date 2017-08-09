using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace API.ViewModels
{
    /// <summary>
    /// Заказ активности. Если пользователь хочет участвовать, — он отправляет заказ и этот заказ приходит к организатору. 
    /// </summary>
    public class ReservationViewModelGet
    {
        /// <summary>
        /// ID
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// ID активности, на которую чел  записывается
        /// </summary>
        public int ActivityId { get; set; }
        public virtual ActivityViewModelShortGet Activity { get; set; }

        /// <summary>
        /// Почта записываемого
        /// </summary>
        public string UserEmail { get; set; }

        /// <summary>
        /// Имя записываемого
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Телефон записываемого
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// Коммент, который оставил записываемый
        /// </summary>
        public string Comment { get; set; }

        /// <summary>
        /// Время создания резервирования
        /// </summary>
        public DateTime Created { get; set; }
    }

    public class ReservationViewModelPost
    {
        /// <summary>
        /// ID активности, на которую чел  записывается
        /// </summary>
        [Required]
        public int ActivityId { get; set; }

        /// <summary>
        /// Почта записываемого
        /// </summary>
        [Required]
        public string  UserEmail { get; set; }

        /// <summary>
        /// Имя записываемого
        /// </summary>
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// Телефон записываемого
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// Коммент, который оставил записываемый
        /// </summary>
        public string Comment { get; set; }
    }
}