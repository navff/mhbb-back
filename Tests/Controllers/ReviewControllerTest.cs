using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using API.ViewModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Models;

namespace Tests.Controllers
{
    [TestClass]
    public class ReviewControllerTest : BaseControllerTest
    {
        [TestMethod]
        public void Get_Ok_Test()
        {
            var review = _context.Reviews.First();
            string url = $"/api/review/{review.Id}";
            var result = HttpGet<ReviewViewModelGet>(url);
            Assert.AreEqual(review.Id, result.Id);
            Assert.AreEqual(review.User.Name, result.User.Name);
        }

        [TestMethod]
        public void GetByUser_Ok_Test()
        {
            var review = _context.Reviews.First();
            string url = $"/api/review/byuser?email={review.UserEmail}";
            var result = HttpGet<IEnumerable<ReviewViewModelGet>>(url);
            Assert.IsTrue(result.Any());
        }

        [TestMethod]
        public void GetByActivity_Ok_Test()
        {
            var review = _context.Reviews.First();
            string url = $"/api/review/byactivity?activityId={review.ActivityId}";
            var result = HttpGet<IEnumerable<ReviewViewModelGet>>(url);
            Assert.IsTrue(result.Any());
        }

        [TestMethod]
        public void Put_Ok_Test()
        {
            var review = _context.Reviews.First();
            var rndString = Guid.NewGuid().ToString();
            var viewModel = new ReviewViewModelPost
            {
                UserEmail = review.UserEmail,
                ActivityId = review.ActivityId,
                Text = rndString,
            };
            string url = $"/api/review/{review.Id}";
            var result = HttpPut<ReviewViewModelGet>(url, viewModel, "ABRAKADABRA");
            Assert.AreEqual(rndString, result.Text);
            Assert.AreEqual(review.UserEmail, result.UserEmail);
        }

        [TestMethod]
        public void Post_Ok_Test()
        {
            var review = _context.Reviews.First();
            var rndString = Guid.NewGuid().ToString();
            var viewModel = new ReviewViewModelPost
            {
                UserEmail = review.UserEmail,
                ActivityId = review.ActivityId,
                Text = rndString,
            };
            string url = $"/api/review";
            var result = HttpPost<ReviewViewModelGet>(url, viewModel, "ABRAKADABRA");
            Assert.AreEqual(rndString, result.Text);
            Assert.AreEqual(review.UserEmail, result.UserEmail);
        }

        [TestMethod]
        public void Delete_Ok_Test()
        {
            var review = _context.Reviews.Take(10).ToList().Last();
            string url = $"/api/review/{review.Id}";
            HttpDelete<string>(url, "ABRAKADABRA");

            using (var cntxt = new HobbyContext())
            {
                Assert.IsNull(cntxt.Reviews.FirstOrDefault(r => r.Id == review.Id));
            }
        }

        [TestMethod]
        public void SetChecked_Ok_Test()
        {
            var review = _context.Reviews.First();
            review.IsChecked = false;
            _context.SaveChanges();

            string url = $"/api/review/setchecked?reviewId={review.Id}&isChecked=true";
            var result = HttpPut<ReviewViewModelGet>(url, null, "ABRAKADABRA");
            Assert.IsTrue(result.IsChecked);
        }
    }
}
