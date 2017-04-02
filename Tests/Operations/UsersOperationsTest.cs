﻿using System;
using System.Linq;
using API.Models;
using API.Operations;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests.Operations
{
    [TestClass]
    public class UsersOperationsTest : BaseTest
    {
        private UserOperations _userOperations;

        public UsersOperationsTest()
        {
            _userOperations = new UserOperations(_context);
        }

        [TestMethod]
        public void GetUserByToken_Ok_Test()
        {
            var token = _context.Users.First().AuthToken;

            var user = _userOperations.GetUserByTokenAsync(token).Result;

            Assert.IsNotNull(user);
            Assert.AreEqual(token, user.AuthToken);
        }

        [TestMethod]
        public void GetUserByToken_WrongToken_Test()
        {

            var user = _userOperations.GetUserByTokenAsync("hooy").Result;

            Assert.IsNull(user);
        }

        [TestMethod]
        public void GetUserByEmail_Ok_Test()
        {
            var user = _context.Users.First();
            var result = _userOperations.GetAsync(user.Email).Result;
            Assert.AreEqual(user.Email, result.Email);
            Assert.AreEqual(user.Name, result.Name);
            Assert.AreEqual(user.Role, result.Role);
            Assert.AreEqual(user.AuthToken, result.AuthToken);
        }

        [TestMethod]
        public void GetUserByEmail_WrongEmail_Test()
        {
            var result = _userOperations.GetAsync("piska").Result;
            Assert.IsNull(result);
        }

        [TestMethod]
        public void Update_Ok_Test()
        {
            var randomString = Guid.NewGuid().ToString();
            var user = _context.Users.First();
            user.Name = randomString;
            user.Phone = randomString;
            var result = _userOperations.UpdateAsync(user).Result;

            Assert.AreEqual(randomString, result.Name);
            Assert.AreEqual(randomString, result.Phone);
        }

        [TestMethod]
        public void Add_Ok_Test()
        {
            var city = _context.Cities.First();
            var picture = _context.Pictures.First();

            var randomString = Guid.NewGuid().ToString();
            var user = new User
            {
                Name = randomString,
                Phone = randomString,
                Email = randomString + "@33kita.ru",
                AuthToken = randomString,
                CityId =  city.Id,
                PictureId = picture.Id,
                Role = Role.RegisteredUser
            };
            var result = _userOperations.AddAsync(user).Result;

            Assert.AreEqual(randomString, result.Name);
            Assert.AreEqual(randomString, result.Phone);
        }

        [TestMethod]
        public void Delete_ok_Test()
        {
            var user = _context.Users.ToList().Last();
            _userOperations.DeleteAsync(user.Email).Wait();

            var deletedUser = _context.Users.Find(user.Email);
            Assert.IsNull(deletedUser);
        }

    }
}
