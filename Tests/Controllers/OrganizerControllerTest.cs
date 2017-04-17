using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using API.Models;
using API.ViewModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests.Controllers
{
    [TestClass]
    public class OrganizerControllerTest : BaseControllerTest
    {
        private string _admin_token;
        public OrganizerControllerTest()
        {
            _admin_token = _context.Users.First(u => u.Role == Role.PortalAdmin).AuthToken;
        }

        [TestMethod]
        public void Get_OK_Test()
        {
            var organizer = _context.Organizers.First();
            var result = HttpGet<OrganizerViewModelGet>($"api/organizer/{organizer.Id}");
            Assert.AreEqual(organizer.Name, result.Name);
        }

        [TestMethod]
        [ExpectedException(typeof(WebException))]
        public void Get_WrongId_Test()
        {
            HttpGet<OrganizerViewModelGet>($"api/organizer/999999");
        }

        [TestMethod]
        public void Post_Ok_Test()
        {
            var city = _context.Cities.First();
            var rndString = Guid.NewGuid().ToString();
            var viewModel = new OrganizerViewModelPost
            {
                Name = rndString,
                CityId = city.Id,
                Sobriety = true
            };

            var result = HttpPost<OrganizerViewModelGet>("api/organizer", viewModel, _admin_token);
            Assert.AreEqual(rndString, result.Name);
            Assert.AreEqual(city.Name, result.City.Name);
        }
    }
}
