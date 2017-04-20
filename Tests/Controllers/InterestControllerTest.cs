using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Models;
using System.Data.Entity;
using System.Net;
using API.ViewModels;


namespace Tests.Controllers
{
    [TestClass]
    public class InterestControllerTest : BaseControllerTest
    {
        private string _admin_token;

        public InterestControllerTest()
        {
            _admin_token = _context.Users.First(u => u.Role == API.Models.Role.PortalAdmin).AuthToken;
        }

        [TestMethod]
        public void Get_Ok_Test()
        {
            var interest = _context.Interests.First();
            string url = $"api/interest/{interest.Id}";
            var result = HttpGet<InterestViewModelGet>(url);
            Assert.AreEqual(interest.Name, result.Name);
        }

        [TestMethod]
        [ExpectedException(typeof(WebException))]
        public void Get_WrongId_Test()
        {
            string url = $"api/interest/999999";
            HttpGet<InterestViewModelGet>(url);
        }

        [TestMethod]
        public void Post_Ok_Test()
        {
            var rndString = Guid.NewGuid().ToString();
            var viewModel = new InterestViewModelPost
            {
                Name = rndString
            };
            string url = $"api/interest";
            var result = HttpPost<InterestViewModelGet>(url, viewModel, _admin_token);
            Assert.AreEqual(rndString, result.Name);
        }

        [TestMethod]
        [ExpectedException(typeof(WebException))]
        public void Post_InvalidModel_Test()
        {
            var viewModel = new InterestViewModelPost
            {
                Name = null
            };
            string url = $"api/interest";
            HttpPost<InterestViewModelGet>(url, viewModel, _admin_token);
        }

        [TestMethod]
        public void Delete_OK_Test()
        {
            var interest = _context.Interests.Take(10).ToList().Last();
            HttpDelete<string>($"api/interest/{interest.Id}", _admin_token);

            using (var cntx = new HobbyContext())
            {
                Assert.IsNull(cntx.Interests.FirstOrDefault(i => i.Id == interest.Id));
            }

        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void Delete_WrongId_Test()
        {
            HttpDelete<string>($"api/interest/9999", _admin_token);
        }

        [TestMethod]
        public void GetAll_Ok_Test()
        {
            var result = HttpGet<IEnumerable<InterestViewModelGet>>($"api/interest/getall");
            Assert.IsTrue(result.Any());
        }

    }
}
