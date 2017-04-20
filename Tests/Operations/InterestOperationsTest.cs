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
    public class InterestOperationsTest : BaseTest
    {
        private InterestOperations _interestOperations;

        public InterestOperationsTest()
        {
            _interestOperations = new InterestOperations(_context);
        }

        [TestMethod]
        public void Get_Ok_Test()
        {
            var interest = _context.Interests.First();
            var result = _interestOperations.GetAsync(interest.Id).Result;
            Assert.AreEqual(interest.Name, result.Name);
        }

        [TestMethod]
        public void Get_WrongId_Test()
        {
            var result = _interestOperations.GetAsync(9999).Result;
            Assert.IsNull(result);
        }

        [TestMethod]
        public void GetAll_Ok_Test()
        {
            var result = _interestOperations.GetAllAsync().Result.ToList();
            Assert.IsTrue(result.Any());
            Assert.IsTrue(!String.IsNullOrEmpty(result.First().Name));
        }

        [TestMethod]
        public void Add_Ok_Test()
        {
            var rndString = Guid.NewGuid().ToString();
            var interest = new Interest
            {
                Name = rndString
            };
            var result = _interestOperations.AddAsync(interest).Result;
            Assert.AreEqual(rndString, result.Name);
        }

        [TestMethod]
        [ExpectedException(typeof(AggregateException))]
        public void Add_InvalidModel_Test()
        {
            var interest = new Interest
            {
                Name = null
            };
            _interestOperations.AddAsync(interest).Wait();
        }

        [TestMethod]
        public void Update_Ok_Test()
        {
            var rndString = Guid.NewGuid().ToString();
            var interest = _context.Interests.First();
            interest.Name = rndString;
            var result = _interestOperations.UpdateAsync(interest).Result;
            Assert.AreEqual(rndString, result.Name);
        }

        [TestMethod]
        [ExpectedException(typeof(AggregateException))]
        public void Update_InvalidModel_Test()
        {
            var interest = _context.Interests.First();
            interest.Name = null;
            _interestOperations.UpdateAsync(interest).Wait();
        }

        [TestMethod]
        public void Delete_Ok_Test()
        {
            var interest = _context.Interests.Take(10).ToList().Last();
            _interestOperations.DeleteAsync(interest.Id).Wait();

            using (var cnx = new HobbyContext())
            {
                Assert.IsNull(cnx.Interests.FirstOrDefault(i => i.Id == interest.Id));
            }
        }

    }
}
