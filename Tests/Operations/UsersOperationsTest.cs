using System;
using System.Linq;
using API.Operations;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests.Operations
{
    [TestClass]
    public class UsersOperationsTest : BaseTest
    {
        [TestMethod]
        public void GetUserByToken_Ok_Test()
        {
            var token = _context.Users.First().AuthToken;

            var userOperations = new UserOperations(_context);
            var user = userOperations.GetUserByTokenAsync(token).Result;

            Assert.IsNotNull(user);
            Assert.AreEqual(token, user.AuthToken);
        }

        [TestMethod]
        public void GetUserByToken_WrongToken_Test()
        {

            var userOperations = new UserOperations(_context);
            var user = userOperations.GetUserByTokenAsync("hooy");

            Assert.IsNull(user);
        }
    }
}
