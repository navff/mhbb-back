using System;
using System.CodeDom;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using API.Models;

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

    /// <summary>
    /// Пользователь
    /// </summary>
    public class UserViewModelPut
    {
        /// <summary>
        /// Электропочта
        /// </summary>
        public string Email { get; set; }

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
        public int? PictureId { get; set; }

        /// <summary>
        /// Роль
        /// </summary>
        public Role Role { get; set; }

        /// <summary>
        /// Id города
        /// </summary>
        public int? CityId { get; set; }
    }

    /// <summary>
    /// Регистрация или аутентификация
    /// </summary>
    public class UserRegisterViewModel
    {
        /// <summary>
        /// Почта пользователя
        /// </summary>
        [Required(AllowEmptyStrings = false)]
        public string Email { get; set; }
    }
}