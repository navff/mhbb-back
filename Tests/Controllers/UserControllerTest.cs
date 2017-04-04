using System.Linq;
using System.Web.Http.Results;
using System.Web.Mvc;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using API;
using API.Controllers;
using API.Models;
using API.Operations;

namespace Tests.Controllers
{
    [TestClass]
    public class UserControllerTest : BaseTest
    {
        private UserController _controller;

        public UserControllerTest()
        {
            UserOperations userOperations = new UserOperations(_context);
            _controller = new UserController(userOperations);
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
    }
}
