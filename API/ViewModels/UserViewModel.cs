using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace API.ViewModels
{
    /// <summary>
    /// Пользователь
    /// </summary>
    public class UserViewModelGet
    {
        /// <summary>
        /// Электропочта
        /// </summary>
        public string Email { get; set; }

        /// <summary>
        /// Токен для аффтаризации. Кладётся в заголовок «Authorization»
        /// со значением «Token [tokenValue]»
        /// </summary>
        public string AuthToken { get; set; }

        /// <summary>
        /// Человеческое имя
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Номер телефона
        /// </summary>
        public string Phone { get; set; }

        /// <summary>
        /// Аватарка
        /// </summary>
        public virtual PictureViewModelGet Picture { get; set; }

        /// <summary>
        /// Название роли
        /// </summary>
        public string RoleName { get; set; }

        /// <summary>
        /// Id города
        /// </summary>
        public int? CityId { get; set; }

        /// <summary>
        /// Название города
        /// </summary>
        public string CityName { get; set; }
    }

    public class UserRegisterViewModel
    {
        public string Email { get; set; }
    }
}