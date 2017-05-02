using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Models;
using Models.Entities;
using Models.Operations;
using System.Data.Entity;

namespace Tests.Operations
{
    [TestClass]
    public class ActivityOperationsTest : BaseTest
    {
        private ActivityOperations _activityOperations;

        public ActivityOperationsTest()
        {
            _activityOperations = new ActivityOperations(_context);
        }

        [TestMethod]
        public void Get_Ok_Test()
        {
            var activity = _context.Activities.First();
            var result = _activityOperations.GetAsync(activity.Id).Result;
            Assert.AreEqual(activity.Name, result.Name);
        }

        [TestMethod]
        public void Get_WrongId_Test()
        {
            var result = _activityOperations.GetAsync(9999).Result;
            Assert.IsNull(result);
        }

        [TestMethod]
        public void SearchNoParameters_Ok_Test()
        {
            var result = _activityOperations.SearchAsync(null, null, null, null, null, null, null).Result;
            Assert.IsTrue(result.Any());
        }

        [TestMethod]
        public void SearchByWord_Ok_Test()
        {
            var activity = _context.Activities.First();
            var result = _activityOperations.SearchAsync(word: activity.Name.Substring(2)).Result.ToList();
            Assert.IsTrue(result.Any());
            Assert.AreEqual(activity.Name, result.First().Name);
        }


        [TestMethod]
        public void Search_Pagination_Test()
        {
            var result = _activityOperations.SearchAsync(page: 2).Result;
            Assert.IsTrue(result.Any());
        }

        [TestMethod]
        public void SearchByInterest_Ok_Test()
        {
            var activity = _context.Activities.First();
            var result = _activityOperations.SearchAsync(interestId: activity.InterestId).Result;
            Assert.IsTrue(result.Any());

        }

        [TestMethod]
        public void SearchBySobriety_Ok_Test()
        {
            var result = _activityOperations.SearchAsync(sobriety: true).Result;
            Assert.IsTrue(result.Any());
        }

        [TestMethod]
        public void SearchByAllFields_Ok_Test()
        {
            var activity = _context.Activities.Include(a => a.Organizer).First();
            var result = _activityOperations.SearchAsync(word: activity.Name.Substring(2),
                                                         age:10, 
                                                         interestId: activity.InterestId,
                                                         cityId: activity.Organizer.CityId,
                                                         sobriety:true,
                                                         free:true,
                                                         page:1).Result;
            Assert.IsTrue(result.Any());
        }

        [TestMethod]
        public void Add_Ok_Test()
        {
            var rndString = Guid.NewGuid().ToString();
            var interest = _context.Interests.First();
            var city = _context.Cities.First();
            var result = _activityOperations.AddAsync(new Activity()
            {
                Name = rndString,
                Address = rndString,
                AgeFrom = 1,
                AgeTo = 20,
                Description = rndString,
                InterestId = interest.Id,
                IsChecked = false,
                Mentor = rndString,
                Organizer = new Organizer
                {
                    Name = rndString,
                    CityId = city.Id,
                    Sobriety = true
                },
                Phones = rndString,
                Prices = rndString
            }).Result;

            Assert.AreEqual(rndString, result.Organizer.Name);
            Assert.AreEqual(rndString, result.Address);
        }

        [TestMethod]
        public void Update_Ok_Test()
        {
            var rndString = Guid.NewGuid().ToString();
            var activity = _context.Activities.First();
            activity.Name = rndString;

            var result = _activityOperations.UpdateAsync(activity).Result;
            Assert.AreEqual(rndString, result.Name);
        }

        [TestMethod]
        public void Delete_Ok_Test()
        {
            var activity = _context.Activities.Take(10).ToList().Last();
            _activityOperations.DeleteAsync(activity.Id).Wait();

            using (var cntxt = new HobbyContext())
            {
                Assert.IsNull(cntxt.Activities.FirstOrDefault(a => a.Id == activity.Id));
            }
        }
    }
}
