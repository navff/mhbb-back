using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using API.Models;
using Models.Entities;

namespace Models
{
    public static class Seeder
    {
        public static void Seed(AppContext context)
        {

            #region CITIES

            var city = context.Cities.FirstOrDefault(c => c.Name == "Череповец") 
                     ?? new City {Name = "Череповец"};

            context.Cities.AddOrUpdate(city);
            context.SaveChanges();

            #endregion

            #region USERS


            context.Users.AddOrUpdate(new User()
            {
                AuthToken = "ABRAKADABRA",
                Email = "var@33kita.ru",
                Role = Role.PortalAdmin,
                CityId = city.Id,
                DateRegistered = DateTime.Now,
                Name = "Вова Петросян",
                Phone = "+79062990044"
                
            });

            Debug.WriteLine(city.Id);

            context.Users.AddOrUpdate(new User()
            {
                AuthToken = "test",
                Email = "test@33kita.ru",
                Role = Role.PortalAdmin,
                CityId = city.Id,
                DateRegistered = DateTime.Now,
                Name = "Морковка"
            });

            context.SaveChanges();

            #endregion

            #region INTERESTS

            Interest interestSport = new Interest { Name = "Спорт" };
            Interest interestScience = new Interest { Name = "Наука" };
            if (!context.Interests.Any())
            {
                interestSport = context.Interests.Add(interestSport);
                context.Interests.Add(new Interest { Name = "Музыка" });
                context.Interests.Add(new Interest { Name = "Танцы" });
                context.Interests.Add(new Interest { Name = "Народное творчество" });
                context.Interests.Add(new Interest { Name = "Религия" });
                context.Interests.Add(new Interest { Name = "Философия" });
                interestScience = context.Interests.Add(interestScience);
                context.Interests.Add(new Interest { Name = "Информационные технологии" });
                context.Interests.Add(new Interest { Name = "Общественная деятельность" });
                context.Interests.Add(new Interest { Name = "Материнство" });

                context.SaveChanges();
            }

            #endregion

            #region ORGANIZERS

            Organizer orgMN = new Organizer {CityId = city.Id, Name = "Мастера науки", Sobriety = false};
            Organizer orgDM = new Organizer {CityId = city.Id, Name = "Дворец Металлургов", Sobriety = false};
            Organizer orgOD = new Organizer {CityId = city.Id, Name = "Общее дело", Sobriety = true};
            Organizer orgBz = new Organizer {CityId = city.Id, Name = "Артель «Буза»", Sobriety = true};

            if (!context.Organizers.Any())
            {
                context.Organizers.Add(orgDM);
                context.Organizers.Add(orgBz);
                context.Organizers.Add(orgOD);
                context.Organizers.Add(orgMN);

                context.SaveChanges();
            }

            #endregion

            #region ACTIVITIES

            if (!context.Activities.Any())
            {
                context.Activities.Add(new Activity
                {
                    Interest = interestScience,
                    Name = "Цирк Супер-Скок",
                    Organizer = orgDM,
                    Address = "ул. Сталеваров, 45",
                    AgeFrom = 4,
                    AgeTo = 18,
                    Description = "Приглашаем деток в наш удивительный цирк. Дети учатся красиво ходить, бегать, прыгать. Имеют прекрасную физическую подготовку.",
                    IsChecked = false,
                    Mentor = "Кимарисанд Муркамбаев",
                    Phones = "(8202)77-77-77, 55-55-55",
                    Prices = "1500 ₽ в месяц"
                });

                context.Activities.Add(new Activity
                {
                    Interest = interestScience,
                    Name = "Мастерская научной деятельности",
                    Organizer = orgMN,
                    Address = "пр. Луначарского",
                    AgeFrom = 8,
                    AgeTo = 18,
                    Description = "Увлекательные научные занятия с опытами, рассказами, интерактивом. Мы ставик красочные экспериенты. Дети в восторге",
                    IsChecked = false,
                    Mentor = "Эммануил Рудаковский",
                    Phones = "+7905-555-5555",
                    Prices = "От 400 ₽ за занятие"
                });

                context.Activities.Add(new Activity
                {
                    Interest = interestSport,
                    Name = "Проект «Здоровая Россия — Общее дело»",
                    Organizer = orgOD,
                    Address = "пр. Советский, 67а",
                    AgeFrom = 18,
                    AgeTo = 55,
                    Description = "Мы ведём работу по отрезвлению страны, улучшению уровня жизни граждан, ведём просветительскую деятельность",
                    IsChecked = true,
                    Mentor = "Эдуард Насоновский",
                    Phones = "Руководитель: (8202)74-00-44, Занятия в школах: 55-55-55",
                    Prices = ""
                });

                context.Activities.Add(new Activity
                {
                    Interest = interestSport,
                    Name = "Русский рукопашный бой",
                    Organizer = orgBz,
                    Address = "ул. Парковая",
                    AgeFrom = 10,
                    AgeTo = 35,
                    Description = "Тренировки по русскому рукопашному бою. Активное участие в общегородских мероприятиях, постоянные выезды в другие города",
                    IsChecked = true,
                    Mentor = "Денис Антипов",
                    Phones = "Запись на тренировкы: 55-55-55",
                    Prices = "500 ₽ в месяц"
                });

                context.SaveChanges();
            }

            #endregion

            #region PICTURES

            
            if (!context.Pictures.Any())
            {
                var activityId = context.Activities.First().Id;

                context.Pictures.Add(new Picture
                {
                    LinkedObjectType = LinkedObjectType.Activity,
                    Description = "Главная картинка",
                    Extension = "jpg",
                    IsMain = true,
                    LinkedObjectId = activityId
                });

                context.Pictures.Add(new Picture
                {
                    LinkedObjectType = LinkedObjectType.Activity,
                    Description = "Неглавная картинка",
                    Extension = "jpg",
                    IsMain = false,
                    LinkedObjectId = activityId
                });

                context.Pictures.Add(new Picture
                {
                    LinkedObjectType = LinkedObjectType.Activity,
                    Description = "Ещё одна картинка",
                    Extension = "jpg",
                    IsMain = false,
                    LinkedObjectId = activityId
                });

                context.SaveChanges();
            }

            #endregion

            #region RESERVATIONS

            if (!context.Reservations.Any())
            {
                var activityId = context.Activities.First().Id;

                context.Reservations.Add(new Reservation
                {
                    ActivityId = activityId,
                    Comment = "Комментарий к заказу",
                    UserEmail = "var@33kita.ru",
                    Name = "Вова",
                    Phone = "+79062990044",
                });

                context.Reservations.Add(new Reservation
                {
                    ActivityId = activityId,
                    Comment = "Комментарий к заказу",
                    UserEmail = "test@33kita.ru",
                    Name = "Тест",
                    Phone = "+7-982-666-2323",
                });

                context.SaveChanges();
            }

            #endregion

            #region REVIEW

            if (!context.Reviews.Any())
            {
                var activityId = context.Activities.First().Id;

                var review1 = context.Reviews.Add(new Review
                {
                    ActivityId = activityId,
                    UserEmail = "test@33kita.ru",
                    DateCreated = DateTime.Now,
                    IsChecked = false,
                    Text = "Всё хорошо!"
                });
                context.SaveChanges();

                context.Reviews.Add(new Review
                {
                    ActivityId = activityId,
                    UserEmail = "var@33kita.ru",
                    DateCreated = DateTime.Now,
                    IsChecked = false,
                    Text = "Спасибо!",
                    ReplyToReviewId = review1.Id
                });
                context.SaveChanges();
            }

            #endregion
        }
    }
}
