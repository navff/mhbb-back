using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API.ViewModels
{
    public class ReviewViewModelGet
    {
        public int Id { get; set; }

        public string UserEmail { get; set; }
        public virtual UserViewModelGet User { get; set; }

        public int ActivityId { get; set; }

        public string Text { get; set; }

        public DateTime DateCreated { get; set; }

        public int? ReplyToReviewId { get; set; }

        /// <summary>
        /// Отзыв проверен менеджером
        /// </summary>
        public bool IsChecked { get; set; }
    }

    public class ReviewViewModelPost
    {
        public string UserEmail { get; set; }

        public int ActivityId { get; set; }

        public string Text { get; set; }

        public int? ReplyToReviewId { get; set; }
    }
}