using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API.ViewModels
{
    /// <summary>
    /// Отзыв об активности
    /// </summary>
    public class ReviewViewModelGet
    {
        /// <summary>
        /// ID
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Логин пользователя, оставившего отзыв
        /// </summary>
        public string UserEmail { get; set; }
        public virtual UserViewModelGet User { get; set; }

        /// <summary>
        /// Id активности, на которую оставлен отзыв
        /// </summary>
        public int ActivityId { get; set; }

        /// <summary>
        /// Название активности, на которую оставлен отзыв
        /// </summary>
        public string ActivityName { get; set; }

        /// <summary>
        /// Текст отзыва
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// Дата создания
        /// </summary>
        public DateTime DateCreated { get; set; }

        /// <summary>
        /// Родительский отзыв (если есть). Ответом на какой отзыв является текущий отзыв
        /// </summary>
        public int? ReplyToReviewId { get; set; }

        /// <summary>
        /// Отзыв проверен менеджером
        /// </summary>
        public bool IsChecked { get; set; }
    }

    /// <summary>
    /// Отзыв
    /// </summary>
    public class ReviewViewModelPost
    {
        /// <summary>
        /// ID активности, на которую пишется отзыв
        /// </summary>
        public int ActivityId { get; set; }

        /// <summary>
        /// Текст отзыва
        /// </summary>
        public string Text { get; set; }

        /// <summary>
        /// Родительский отзыв (если есть). Ответом на какой отзыв является текущий отзыв
        /// </summary>
        public int? ReplyToReviewId { get; set; }
    }
}