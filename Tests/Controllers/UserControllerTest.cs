﻿using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Formatting;
using System.Net.Http.Headers;
using System.Threading;
using System.Web.Http;
using System.Web.Http.Results;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using API;
using API.App_Start;
using API.Common;
using API.Controllers;
using API.Models;
using API.Operations;
using API.ViewModels;
using Ninject.Web.Common;
using NLog;
using Tests.Helpers;

namespace Tests.Controllers
{
    [TestClass]
    public class UserControllerTest : BaseControllerTest
    {
        private UserController _controller;

        public UserControllerTest()
        {
            UserOperations userOperations = new UserOperations(_context);
            _controller = new UserController(userOperations);
            MapperMappings.Map();
        }


        [TestMethod]
        public void HTTP_GetByEmail_OK_Test()
        {
            var result = HttpGet<UserViewModelGet>("api/user?email=var@33kita.ru");
            Assert.AreEqual("var@33kita.ru", result.Email);
        }

        [TestMethod]
        public void HTTP_GetUserByToken_OK_Test()
        {
            var result = HttpGet<string>("api/user/getuser", "ABRAKADABRA");
            Assert.AreEqual("var@33kita.ru", result);
        }

        [TestMethod]
        public void HTTP_Delete_OK_Test()
        {
            var user = _context.Users.First();
            var result = HttpDelete<string>($"api/user/delete?email={user.Email}", user.AuthToken);
            Assert.AreEqual("Deleted "+user.Email, result);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void HTTP_Delete_WrongToken_Test()
        {
            var user = _context.Users.First();
            HttpDelete<string>($"api/user/delete?email={user.Email}", "wrong_token");
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void HTTP_Delete_AnotherUserToken_Test()
        {
            var user = _context.Users.First();
            var anotherUser = _context.Users.Where(u => u.Role == Role.RegisteredUser).Take(2).ToList().Last();
            HttpDelete<string>($"api/user/delete?email={user.Email}", anotherUser.AuthToken);
        }

        [TestMethod]
        public void HTTP_Put_Ok_Test()
        {
            var user = _context.Users.First();
            var rndString = Guid.NewGuid().ToString();

            var viewModel = new UserViewModelPut
            {
                Email = user.Email,
                Name = rndString,
                Phone = rndString,
                CityId = user.CityId,
                Role = user.Role,
            };

            var result = HttpPut<UserViewModelGet>($"api/user?email={user.Email}", viewModel, "ABRAKADABRA");

            Assert.AreEqual(rndString, result.Name);
            Assert.AreEqual(rndString, result.Phone);

        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void HTTP_Put_AnotherUser_Test()
        {
            var user = _context.Users.First();
            var anotherUser = _context.Users.Where(u => u.Role == Role.RegisteredUser).Take(2).ToList().Last();

            var rndString = Guid.NewGuid().ToString();

            var viewModel = new UserViewModelPut
            {
                Email = user.Email,
                Name = rndString,
                Phone = rndString,
                CityId = user.CityId,
                Role = user.Role,
            };

            HttpPut<UserViewModelGet>($"api/user?email={user.Email}", viewModel, anotherUser.AuthToken);
        }

        [TestMethod]
        public void HTTP_Post_Ok_Test()
        {
            var rndString = Guid.NewGuid().ToString();
            var viewModel = new UserRegisterViewModel
            {
                Email = rndString+"@33kita.ru"
            };
            var result = HttpPost<UserViewModelGet>($"api/user", viewModel);

            Assert.AreEqual(rndString+"@33kita.ru", result.Email);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void HTTP_Post_EmptyEmail_Test()
        {
            var viewModel = new UserRegisterViewModel
            {
                Email = ""
            };
            HttpPost<UserViewModelGet>($"api/user", viewModel);
        }
    }
}
