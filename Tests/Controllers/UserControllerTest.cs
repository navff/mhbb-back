using System;
using System.Linq;
using System.Net;
using System.Web.Http.Results;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using API;
using API.Controllers;
using API.Models;
using API.Operations;
using API.ViewModels;

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
        public void GetByEmail_Ok_Test()
        {
            var user = _context.Users.First();
            var result = _controller.Get(user.Email).Result;
            var userFromResult = ( (OkNegotiatedContentResult<User>) result).Content;
            Assert.IsNotNull(userFromResult);
            Assert.AreEqual(user.Email, userFromResult.Email);
            Assert.AreEqual(user.AuthToken, userFromResult.AuthToken);
            Assert.AreEqual(user.CityId, userFromResult.CityId);
            Assert.AreEqual(user.Phone, userFromResult.Phone);
        }

        [TestMethod]
        public void Put_Ok_Test()
        {
            PrepareController(_controller, userName:"var@33kita.ru", role:"PortalAdmin");
            var user = _context.Users.First();
            var rndString = Guid.NewGuid().ToString();

            var result = _controller.Put(user.Email, new UserViewModelPut
            {
                Email = user.Email,
                Name = rndString,
                Phone = rndString,
                Role = Role.PortalAdmin,
                CityId = user.CityId
            }).Result;

            var userFromResult = ((OkNegotiatedContentResult<User>)result).Content;
            Assert.IsNotNull(userFromResult);
            Assert.AreEqual(user.Email, userFromResult.Email);
            Assert.AreEqual(user.AuthToken, userFromResult.AuthToken);
            Assert.AreEqual(user.CityId, userFromResult.CityId);
            Assert.AreEqual(rndString, userFromResult.Phone);
        }

        [TestMethod]
        public void Put_WrongUser_Test()
        {
            PrepareController(_controller, userName: "sdsdsdsdsdsd@33kita.ru", role: "RegisteredUser");
            var user = _context.Users.First();
            var rndString = Guid.NewGuid().ToString();

            var result = _controller.Put(user.Email, new UserViewModelPut
            {
                Email = user.Email,
                Name = rndString,
                Phone = rndString,
                Role = Role.PortalAdmin,
                CityId = user.CityId
            }).Result;

            Assert.IsInstanceOfType(result, typeof(ResponseMessageResult));
            var responseMessageResult = (ResponseMessageResult) result;
            Assert.IsTrue(responseMessageResult.Response.StatusCode == HttpStatusCode.Unauthorized); 

        }
    }
}
