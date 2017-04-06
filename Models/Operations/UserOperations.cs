﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity;
using System.Diagnostics;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using API.Models;
using Camps.Tools;
using Models;
using Models.Tools;

namespace API.Operations
{
    public class UserOperations
    {
        private AppContext _context;

        public UserOperations(AppContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Получение пользователя по токену
        /// </summary>
        /// <param name="token"></param>
        /// <returns></returns>
        public async Task<User> GetUserByTokenAsync(string token)
        {
            return _context.Users.FirstOrDefault(u => u.AuthToken == token);
        }

        /// <summary>
        /// Получение пользователя по электронной почте
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public async Task<User> GetAsync(string email)
        {
            return await _context.Users.FirstOrDefaultAsync(u => u.Email == email);
        }

        /// <summary>
        /// Обновление пользователя
        /// </summary>
        /// <param name="user"></param>
        /// <returns></returns>
        public async Task<User> UpdateAsync(User user)
        {
            var userInDb = await _context.Users.FindAsync(user.Email);
            Contracts.Assert(userInDb!=null);

            userInDb.Name = user.Name;
            userInDb.CityId = user.CityId;
            userInDb.Phone = user.Phone;
            userInDb.PictureId = user.PictureId;

            await _context.SaveChangesAsync();
            return userInDb;
        }

        public async Task<User> AddAsync(User user)
        {
            Contracts.Assert(user!=null);
            Contracts.Assert(!String.IsNullOrEmpty(user.AuthToken));
            Contracts.Assert(!String.IsNullOrEmpty(user.Email));
            Contracts.Assert(!String.IsNullOrEmpty(user.Phone));

            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return user;
        }

        public async Task DeleteAsync(string email)
        {
            var user = _context.Users.Find(email);
            Contracts.Assert(user!=null);

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
        }

        public async Task<User> RegisterAsync(string email)
        {
            //Если пользователь существует, создаем новый токен и отправляем ссылку
            var usr = await GetAsync(email);
            if (usr != null)
            {
                SendEmail(usr.AuthToken, usr.Email);
                return usr;
            }
            //Иначе - добавляем пользователя, создаем токен и отправляем ссылку
            else
            {
                var user = new User
                {
                    Email = email,
                    AuthToken = GenerateToken(),
                    Role = Role.RegisteredUser,
                    DateRegistered = DateTime.Now
                };
                try
                {
                    _context.Users.Add(user);
                    await _context.SaveChangesAsync();
                    return user;
                }
                catch (Exception ex)
                {
                    ErrorLogger.ThrowAndLog("Exception in UserService", ex);
                    return null;
                }
            }
        }

        public async Task<IEnumerable<User>> SearchAsync(string word)
        {
            var result = new List<User>();

            var usersByEmail = await _context.Users.Where(u => u.Email.Contains(word)).ToListAsync();
            result.AddRange(usersByEmail);

            var usersByName = await _context.Users.Where(u => u.Name.Contains(word)).ToListAsync();
            result.AddRange(usersByName);

            return result;
        }


        //=============================================================================================================
        //Отправка мейла
        private static bool SendEmail(string token, string to)
        {
            var dto = new EmailMessage()
            {
                From = "Моё хобби <site@orientirum.ru>",
                To = to,
                Body = GenerateEmailBody(token),
                EmailSubject = UserMessages.SubjectConfirmEmail
            };
            return EmailService.SendEmail(dto);
        }


        //TODO: заменить на html шаблон
        //Создание шаблона письма
        private static string GenerateEmailBody(string token)
        {
            var s = new StringBuilder();
            s.Append("Здравствуйте!<br/>");
            s.AppendFormat("Для подтверждения регистрарции в «Моём Хобби», пожалуйста перейдите по " +
                           "<a href='https://test.mhbb.ru/#/validate-token?token={0}'>ссылке</a>.<br/>Token= {0}", token);
            return s.ToString();
        }

        //Генерация токена
        private static string GenerateToken()
        {
            var guid = Guid.NewGuid().ToString();
            byte[] bytes = Encoding.UTF8.GetBytes(guid);
            var hash = SHA256.Create().ComputeHash(bytes, 0, 32);
            return hash.Aggregate(string.Empty, (current, x) => current + $"{x:x2}");
        }

    }
}