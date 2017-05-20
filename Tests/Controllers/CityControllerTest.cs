using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using API;
using API.Controllers;
using API.Models;
using API.ViewModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Models;
using Models.Operations;

namespace Tests.Controllers
{
    [TestClass]
    public class CityControllerTest : BaseControllerTest
    {
        private CityController _controller;

        public CityControllerTest()
        {
            var cityOperations = new CityOperations(_context);
            _controller = new CityController(cityOperations);
            MapperMappings.Map();
        }

        [TestMethod]
        public void GetAll_Ok_Test()
        {
            var result = HttpGet<IEnumerable<CityViewModelGet>>($"api/city");
            Assert.IsTrue(result.Any());
        }


        [TestMethod]
        public void Get_Ok_Test()
        {
            var city = _context.Cities.First();
            var result = HttpGet<CityViewModelGet>($"api/city/{city.Id}");
            Assert.AreEqual(city.Name, result.Name);
        }

        [TestMethod]
        [ExpectedException(typeof(WebException))]
        public void Get_WrongId_Test()
        {
            HttpGet<CityViewModelGet>($"api/city/99999");
        }

        [TestMethod]
        public void Put_Ok_Test()
        {
            var city = _context.Cities.First();
            var rndString = Guid.NewGuid().ToString();
            var viewModel = new CityViewModelPost
            {
                Name    = rndString
            };
            var user = _context.Users.First(u => u.Role == Role.PortalAdmin);
            var result = HttpPut<CityViewModelGet>($"api/city/{city.Id}", viewModel, user.AuthToken);
            Assert.AreEqual(rndString, result.Name);
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void Put_WrongId_Test()
        {
            var viewModel = new CityViewModelPost
            {
                Name = "Hooy"
            };
            var user = _context.Users.First(u => u.Role == Role.PortalAdmin);
            HttpPut<CityViewModelGet>($"api/city/99999", viewModel, user.AuthToken);
        }

        [TestMethod]
        [ExpectedException(typeof(WebException))]
        public void Put_NoAdmin_Test()
        {
            var user = _context.Users.First(u => u.Role != Role.PortalAdmin);
            var city = _context.Cities.First();
            var rndString = Guid.NewGuid().ToString();
            var viewModel = new CityViewModelPost
            {
                Name = rndString
            };

            HttpPut<CityViewModelGet>($"api/city/{city.Id}", viewModel, user.AuthToken);
        }

        [TestMethod]
        public void Post_Ok_Test()
        {
            var rndString = Guid.NewGuid().ToString();
            var viewModel = new CityViewModelPost
            {
                Name = rndString
            };
            var user = _context.Users.First(u => u.Role == Role.PortalAdmin);
            var result = HttpPost<CityViewModelGet>($"api/city", viewModel, user.AuthToken);
            Assert.AreEqual(rndString, result.Name);
        }

        [TestMethod]
        [ExpectedException(typeof(WebException))]
        public void Post_NoAdmin_Test()
        {
            var user = _context.Users.First(u => u.Role != Role.PortalAdmin);
            var rndString = Guid.NewGuid().ToString();
            var viewModel = new CityViewModelPost
            {
                Name = rndString
            };

            HttpPost<CityViewModelGet>($"api/city", viewModel, user.AuthToken);
        }

        [TestMethod]
        public void Delete_Ok_Test()
        {
            var user = _context.Users.First(u => u.Role == Role.PortalAdmin);
            var city = _context.Cities.Take(2).ToList().Last();
            HttpDelete<string>($"api/city/{city.Id}", user.AuthToken);

            using (var context = new HobbyContext())
            {
                Assert.IsNull(context.Cities.FirstOrDefault(c => c.Id == city.Id));
            }
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void Delete_WrongId_Test()
        {
            var user = _context.Users.First(u => u.Role == Role.PortalAdmin);
            HttpDelete<string>($"api/city/999999", user.AuthToken);
        }

        [TestMethod]
        [ExpectedException(typeof(WebException))]
        public void Delete_NoAdmin_Test()
        {
            var user = _context.Users.First(u => u.Role != Role.PortalAdmin);
            var city = _context.Cities.Take(2).ToList().Last();
            HttpDelete<string>($"api/city/{city.Id}", user.AuthToken);

            using (var context = new HobbyContext())
            {
                Assert.IsNull(context.Cities.FirstOrDefault(c => c.Id == city.Id));
            }
        }

        [TestMethod]
        public void Search_Ok_Test()
        {
            var city = _context.Cities.First();
            var result = HttpGet<IEnumerable<CityViewModelGet>>($"api/city/search?word={city.Name.Substring(2)}");
            Assert.AreEqual(city.Name, result.First().Name);
        }


    }
}
