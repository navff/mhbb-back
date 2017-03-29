using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Models.Entities;

namespace API.ViewModels
{
    /// <summary>
    /// Картинка
    /// </summary>
    public class PictureViewModelGet
    {
        /// <summary>
        /// ID
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Описание. Можно использовать в ALT
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Расширение
        /// </summary>
        public string Extension { get; set; }

        /// <summary>
        /// Главная картинка объекта
        /// </summary>
        public bool IsMain { get; set; }

        /// <summary>
        /// Ссылка на объект
        /// </summary>
        public int? LinkedObjectId { get; set; }

        /// <summary>
        /// Тип объекта
        /// </summary>
        public LinkedObjectType LinkedObjectType { get; set; }
    }
}