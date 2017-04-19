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
    public class OrganizerOperationsTest : BaseTest
    {
        private OrganizerOperations _organizerOperations;

        public OrganizerOperationsTest()
        {
            _organizerOperations = new OrganizerOperations(_context);
        }


        [TestMethod]
        public void Get_Ok_Test()
        {
            var org = _context.Organizers.First();
            var result = _organizerOperations.GetAsync(org.Id).Result;
            Assert.AreEqual(org.Name, result.Name);
        }

        [TestMethod]
        public void Get_WrongId_Test()
        {
            var result = _organizerOperations.GetAsync(99999).Result;
            Assert.IsNull(result);
        }

        [TestMethod]
        public void Add_Ok_Test()
        {
            var rndString = Guid.NewGuid().ToString();
            var city = _context.Cities.First();

            var org = new Organizer
            {
                Name = rndString,
                CityId = city.Id
            };

            var result = _organizerOperations.AddAsync(org).Result;

            Assert.IsNotNull(result);
            Assert.AreEqual(rndString, result.Name);
            Assert.AreEqual(city.Name, result.City.Name);
        }

        [TestMethod]
        [ExpectedException(typeof(AggregateException))]
        public void Add_ModelErrors_Test()
        {
            var city = _context.Cities.First();

            var org = new Organizer
            {
                Name = null,
                CityId = city.Id
            };
            _organizerOperations.AddAsync(org).Wait();
        }

        [TestMethod]
        public void Update_Ok_Test()
        {
            var orgInDb = _context.Organizers.First();
            var rndString = Guid.NewGuid().ToString();

            var org = new Organizer
            {
                Id = orgInDb.Id,
                Name = rndString,
                CityId = orgInDb.CityId
            };

            var result = _organizerOperations.UpdateAsync(org).Result;

            Assert.IsNotNull(result);
            Assert.AreEqual(rndString, result.Name);
            Assert.AreEqual(orgInDb.City.Name, result.City.Name);
            Assert.AreEqual(orgInDb.Id, result.Id);
        }

        [TestMethod]
        [ExpectedException(typeof(AggregateException))]
        public void Update_ModelErrors_Test()
        {
            var orgInDb = _context.Organizers.First();
            var rndString = Guid.NewGuid().ToString();

            var org = new Organizer
            {
                Id = orgInDb.Id,
                Name = rndString,
                CityId = 999999
            };

            _organizerOperations.AddAsync(org).Wait();
        }

        [TestMethod]
        public void Delete_Ok_Test()
        {
            var org = _context.Organizers.Take(10).ToList().Last();
            _organizerOperations.DeleteAsync(org.Id).Wait();

            using (var cntxt = new HobbyContext())
            {
                Assert.IsNull(cntxt.Cities.FirstOrDefault(o => o.Id == org.Id));
            }
        }

        [TestMethod]
        [ExpectedException(typeof(AggregateException))]
        public void Delete_WrongId_Test()
        {
            _organizerOperations.DeleteAsync(99999).Wait();
        }

        [TestMethod]
        public void Search_Ok_Test()
        {
            var orgInDb = _context.Organizers.First();
            var result = _organizerOperations.SearchAsync(orgInDb.Name.Substring(2)).Result;
            Assert.IsTrue(result.Any());
        }

        [TestMethod]
        public void Search_NoResults_Test()
        {
            var result = _organizerOperations.SearchAsync("8ebew8eb3b38b4bmnsds9323nakjadhdf0").Result;
            Assert.IsFalse(result.Any());
        }

        [TestMethod]
        public void GetAll_Ok_Test()
        {
            var result = _organizerOperations.GetAllAsync().Result;
            Assert.IsTrue(result.Any());
        }

        [TestMethod]
        public void GetAll_SecondPage_Test()
        {
            var result = _organizerOperations.GetAllAsync(2).Result;
            Assert.IsTrue(result.Any());
        }

    }
}
