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
    public class Review
    {
        [Key]
        public int Id { get; set; }

        [ForeignKey("User")]
        public int UserId { get; set; }
        public virtual User User { get; set; }

        [ForeignKey("Activity")]
        public int ActivityId { get; set; }
        public virtual Activity Activity { get; set; }

        public string Text { get; set; }

        public DateTime DateCreated { get; set; }

        [ForeignKey("ReplyTo")]
        public int ReplyToReviewId { get; set; }
        public Review ReplyTo { get; set; }

        /// <summary>
        /// Отзыв проверен менеджером
        /// </summary>
        public bool IsChecked { get; set; }

    }
}
