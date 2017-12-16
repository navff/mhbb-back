using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using API.Models;
using API.ViewModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Models;
using System.Data.Entity;

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
                Sobriety = true,
                Email = "test@mhbb.ru",
                Phone = "77777"
            };

            var result = HttpPost<OrganizerViewModelGet>("api/organizer", viewModel, _admin_token);
            Assert.AreEqual(rndString, result.Name);
            Assert.AreEqual(city.Name, result.City.Name);
        }

        [TestMethod]
        [ExpectedException(typeof(WebException))]
        public void Post_InvalidModel_Test()
        {
            var city = _context.Cities.First();
            var viewModel = new OrganizerViewModelPost
            {
                Name = null,
                CityId = city.Id,
                Sobriety = true
            };

            HttpPost<OrganizerViewModelGet>("api/organizer", viewModel, _admin_token);
        }

        [TestMethod]
        public void Put_Ok_Test()
        {
            var org = _context.Organizers.Include(o => o.City).First();
            var rndString = Guid.NewGuid().ToString();
            var viewModel = new OrganizerViewModelPost
            {
                Name = rndString,
                CityId = org.CityId,
                Sobriety = true,
                Email = "test@mhbb.ru",
                Phone = "77777"
            };

            var result = HttpPut<OrganizerViewModelGet>($"api/organizer/{org.Id}", viewModel, _admin_token);
            Assert.AreEqual(rndString, result.Name);
            Assert.AreEqual(org.City.Name, result.City.Name);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void Put_InvalidModel_Test()
        {
            var org = _context.Organizers.First();
            var rndString = Guid.NewGuid().ToString();
            var viewModel = new OrganizerViewModelPost
            {
                Name = rndString,
                CityId = 99999,
                Sobriety = true
            };

            HttpPut<OrganizerViewModelGet>($"api/organizer/{org.Id}", viewModel, _admin_token);
        }

        [TestMethod]
        public void Delete_OK_Test()
        {
            var org = _context.Organizers.Take(10).ToList().Last();
            HttpDelete<string>($"api/organizer/{org.Id}", _admin_token);

            using (var cntx = new HobbyContext())
            {
                Assert.IsNull(cntx.Organizers.FirstOrDefault(o => o.Id == org.Id));
            }
        }

        [TestMethod]
        [ExpectedException(typeof(WebException))]
        public void Delete_WrongId_Test()
        {
            HttpDelete<string>($"api/organizer/999999", _admin_token);
        }

        [TestMethod]
        public void Search_Ok_Test()
        {
            var org = _context.Organizers.First();
            var result = HttpGet<IEnumerable<OrganizerViewModelGet>>($"api/organizer/search?word={org.Name.Substring(2)}&cityId={org.CityId}");
            Assert.AreEqual(org.Name, result.FirstOrDefault(o => o.Name == org.Name)?.Name);
        }

        [TestMethod]
        public void Search_NotFound_Test()
        {
            var result = HttpGet<IEnumerable<OrganizerViewModelGet>>($"api/organizer/search?word=8484848484848484");
            Assert.IsFalse(result.Any());
        }

    }
}
