using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using API.Models;
using API.ViewModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Models;
using Models.Operations;

namespace Tests.Controllers
{
    [TestClass]
    public class ActivityControllerTest : BaseControllerTest
    {
        private readonly ActivityOperations _activityOperations;

        public ActivityControllerTest()
        {
            _activityOperations = new ActivityOperations(_context);
        }

        [TestMethod]
        public void Get_Ok_Test()
        {
            var activity = _context.Activities.First();
            string url = $"api/activity/{activity.Id}";
            var result = HttpGet<ActivityViewModelGet>(url);
            Assert.AreEqual(activity.Name, result.Name);
        }

        [TestMethod]
        [ExpectedException(typeof(WebException))]
        public void Get_WrongId_Test()
        {
            string url = $"api/activity/99999";
            HttpGet<ActivityViewModelGet>(url);
        }

        [TestMethod]
        public void Search_Ok_Test()
        {
            string url = $"api/activity/search";
            var result = HttpGet<IEnumerable<ActivityViewModelGet>>(url);
            Assert.IsTrue(result.Any());
        }

        [TestMethod]
        public void SearchUnchecked_Ok_Test()
        {
            string url = $"api/activity/searchunchecked";
            var result = HttpGet<IEnumerable<ActivityViewModelGet>>(url, "ABRAKADABRA");
            Assert.IsTrue(result.Any());
        }

        [TestMethod]
        public void Search_Pagination_Test()
        {
            string url = $"api/activity/search?page=1";
            var result = HttpGet<IEnumerable<ActivityViewModelGet>>(url);
            Assert.IsTrue(result.Any());
        }

        [TestMethod]
        public void Put_Ok_Test()
        {
            var rndString = Guid.NewGuid().ToString();
            var activity = _context.Activities.First();
            string url = $"api/activity/{activity.Id}";
            var user = _context.Users.First(u => u.Role == Role.PortalAdmin);

            var viewModel = new ActivityViewModelPost
            {
                Name = rndString,
                Address = rndString,
                AgeFrom = activity.AgeFrom,
                AgeTo = activity.AgeTo,
                Description = rndString,
                InterestId = activity.InterestId,
                IsChecked = activity.IsChecked,
                Mentor = rndString,
                OrganizerId = activity.OrganizerId,
                Phones = rndString,
                Prices = rndString
            };
            var result = HttpPut<ActivityViewModelGet>(url, viewModel, user.AuthToken);

            Assert.AreEqual(rndString, result.Name);
            Assert.AreEqual(rndString, result.Address);
            Assert.AreEqual(rndString, result.Description);
            Assert.AreEqual(rndString, result.Phones);
        }

        [TestMethod]
        public void Post_Ok_Test()
        {
            var rndString = Guid.NewGuid().ToString();
            var rndInt = DateTime.Now.Second;
            string url = $"api/activity";
            var user = _context.Users.First(u => u.Role == Role.PortalAdmin);
            var interest = _context.Interests.First();
            var city = _context.Cities.First();
            var tempFile = _context.TempFiles.FirstOrDefault(tf => tf.FormId != null);

            var viewModel = new ActivityViewModelPost
            {
                Name = rndString,
                Address = rndString,
                AgeFrom = rndInt,
                AgeTo = rndInt,
                Description = rndString,
                InterestId = interest.Id,
                IsChecked = true,
                Mentor = rndString,
                Organizer = new OrganizerViewModelPost {CityId = city.Id, Name = rndString, Sobriety = true},
                Phones = rndString,
                Prices = rndString,
                FormId = tempFile?.FormId
            };
            var result = HttpPost<ActivityViewModelGet>(url, viewModel, user.AuthToken);

            Assert.AreEqual(rndString, result.Name);
            Assert.AreEqual(rndString, result.Address);
            Assert.AreEqual(rndString, result.Description);
            Assert.AreEqual(rndString, result.Phones);
            Assert.AreEqual(rndString, result.Organizer.Name);
        }

        [TestMethod]
        public void Delete_Ok_Test()
        {
            var activity = _context.Activities.Take(10).ToList().Last();
            _activityOperations.DeleteAsync(activity.Id).Wait();

            using (var cntxt = new HobbyContext())
            {
                Assert.IsNull(cntxt.Activities.Find(activity.Id));
            }
        }
    }
}
