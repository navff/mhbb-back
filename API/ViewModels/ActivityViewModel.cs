using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;
using Models.Entities;

namespace API.ViewModels
{
    /// <summary>
    /// Активность
    /// </summary>
    public class ActivityViewModelGet
    {
        /// <summary>
        /// Идентификатор
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Название
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Организатор
        /// </summary>
        public virtual OrganizerViewModelGet Organizer { get; set; }

        /// <summary>
        /// От скольки лет можно участвовать в активности
        /// </summary>
        public int AgeFrom { get; set; }

        /// <summary>
        /// До скольки лет можно участвовать
        /// </summary>
        public int AgeTo { get; set; }

        /// <summary>
        /// Телефоны (простым текстом)
        /// </summary>
        public string Phones { get; set; }

        /// <summary>
        /// Адрес проведения
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// Ценовая политика (простым текстом)
        /// </summary>
        public string Prices { get; set; }

        /// <summary>
        /// Ведущий
        /// </summary>
        public string Mentor { get; set; }

        /// <summary>
        /// Описание
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Категория интересов
        /// </summary>
        public virtual InterestViewModelGet Interest { get; set; }

        /// <summary>
        /// Проверена админом
        /// </summary>
        public bool IsChecked { get; set; }

        /// <summary>
        /// Картинки
        /// </summary>
        public IEnumerable<PictureViewModelShortGet> Pictures { get; set; }

        /// <summary>
        /// Сумма голосов за активность
        /// </summary>
        public int Voices { get; set; }

        /// <summary>
        /// Бесплатная
        /// </summary>
        public bool Free { get; set; }
    }

    /// <summary>
    /// Активность
    /// </summary>
    public class ActivityViewModelShortGet
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
        /// Организатор
        /// </summary>
        public virtual OrganizerViewModelGet Organizer { get; set; }

        /// <summary>
        /// Описание
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Главная картинка
        /// </summary>
        public PictureViewModelShortGet MainPicture { get; set; }

        /// <summary>
        /// Сумма голосов за активность
        /// </summary>
        public int Voices { get; set; }

        /// <summary>
        /// Бесплатная
        /// </summary>
        public bool Free { get; set; }

        /// <summary>
        /// Возраст, с которого можно участвовать
        /// </summary>
        public int AgeFrom { get; set; }

        /// <summary>
        /// Возраст, до которого можно участвовать
        /// </summary>
        public int AgeTo { get; set; }
    }

    /// <summary>
    /// Активность
    /// </summary>
    public class ActivityViewModelPost
    {
        /// <summary>
        /// Название
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Организатор
        /// </summary>
        public int? OrganizerId { get; set; }
        public virtual OrganizerViewModelPost Organizer { get; set; }

        /// <summary>
        /// С какого возраста
        /// </summary>
        public int AgeFrom { get; set; }

        /// <summary>
        /// До какого возраста
        /// </summary>
        public int AgeTo { get; set; }

        /// <summary>
        /// Телефоны
        /// </summary>
        public string Phones { get; set; }

        /// <summary>
        /// Адрес проведения
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Prices { get; set; }

        /// <summary>
        /// Ведущий, преподаватель
        /// </summary>
        public string Mentor { get; set; }

        /// <summary>
        /// Текстовое описание
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// ID сферы интересов
        /// </summary>
        public int? InterestId { get; set; }

        /// <summary>
        /// Проверено менеджером
        /// </summary>
        public bool IsChecked { get; set; }

        /// <summary>
        /// Бесплатно
        /// </summary>
        public bool Free { get; set; }


        /// <summary>
        /// ID формы. По этому идентификатору связываются временные файлы
        /// с активностями. После сохранения активности, все TempFile с указанным FormId
        /// превратятся в Picture и привяжутся в данной активности.
        /// </summary>
        public string FormId { get; set; }


    }


    /// <summary>
    /// Активность
    /// </summary>
    public class ActivityViewModelPut
    {
        /// <summary>
        /// Название
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Организатор
        /// </summary>
        public int OrganizerId { get; set; }

        /// <summary>
        /// С какого возраста
        /// </summary>
        public int AgeFrom { get; set; }

        /// <summary>
        /// До какого возраста
        /// </summary>
        public int AgeTo { get; set; }

        /// <summary>
        /// Телефоны
        /// </summary>
        public string Phones { get; set; }

        /// <summary>
        /// Адрес проведения
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// 
        /// </summary>
        public string Prices { get; set; }

        /// <summary>
        /// Ведущий, преподаватель
        /// </summary>
        public string Mentor { get; set; }

        /// <summary>
        /// Текстовое описание
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// ID сферы интересов
        /// </summary>
        public int? InterestId { get; set; }

        /// <summary>
        /// Проверено менеджером
        /// </summary>
        public bool IsChecked { get; set; }

        /// <summary>
        /// Бесплатно
        /// </summary>
        public bool Free { get; set; }


        /// <summary>
        /// ID формы. По этому идентификатору связываются временные файлы
        /// с активностями. После сохранения активности, все TempFile с указанным FormId
        /// превратятся в Picture и привяжутся в данной активности.
        /// </summary>
        public string FormId { get; set; }


    }
}