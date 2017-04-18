using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Models;
using Models.Entities;
using Models.Operations;

namespace Tests.Operations
{
    [TestClass]
    public class CityOperationsTest : BaseTest
    {
        private CityOperations _cityOperations;

        public CityOperationsTest()
        {
            _cityOperations = new CityOperations(_context);
        }

        [TestMethod]
        public void Add_Ok_Test()
        {
            var rndString = Guid.NewGuid().ToString();
            var result = _cityOperations.AddAsync(new City
            {
                Name = rndString
            }).Result;

            Assert.AreEqual(rndString, result.Name);
        }

        [TestMethod]
        public void Update_Ok_Test()
        {
            var rndString = Guid.NewGuid().ToString();
            var city = _context.Cities.First();
            city.Name = rndString;

            var result = _cityOperations.UpdateAsync(city).Result;
            Assert.AreEqual(rndString, result.Name);
        }

        [TestMethod]
        public void Get_Ok_Test()
        {
            var city = _context.Cities.First();
            var result = _cityOperations.GetAsync(city.Id).Result;

            Assert.AreEqual(city.Name, result.Name);
        }

        [TestMethod]
        public void Delete_Ok_Test()
        {
            var city = _context.Cities.ToList().Take(2).Last();
            _cityOperations.DeleteAsync(city.Id).Wait();

            using (var context = new HobbyContext())
            {
                Assert.IsNull(context.Cities.FirstOrDefault(c => c.Id == city.Id));
            }
        }

        [TestMethod]
        public void Search_Ok_Test()
        {
            var city = _context.Cities.First();
            var keyword = city.Name.Substring(2);
            var result = _cityOperations.SearchAsync(keyword).Result;

            Assert.AreEqual(city.Name, result.First().Name);
        }

    }
}
