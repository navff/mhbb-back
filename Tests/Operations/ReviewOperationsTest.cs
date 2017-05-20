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
    public class ReviewOperationsTest : BaseTest
    {
        private ReviewOperations _reviewOperations;

        public ReviewOperationsTest()
        {
            _reviewOperations = new ReviewOperations(_context);
        }

        [TestMethod]
        public void Get_Ok_Test()
        {
            var review = _context.Reviews.Include(r => r.User).First();
            var result = _reviewOperations.GetAsync(review.Id).Result;
            Assert.AreEqual(review.Text, result.Text);
            Assert.AreEqual(review.User.Name, review.User.Name);
        }

        [TestMethod]
        public void Get_WrongId_Test()
        {
            var result = _reviewOperations.GetAsync(99999).Result;
            Assert.IsNull(result);
        }

        [TestMethod]
        public void Update_Ok_Test()
        {
            var review = _context.Reviews.First();
            var rndString = Guid.NewGuid().ToString();
            var date = DateTime.Now.Date;
            var result = _reviewOperations.UpdateAsync(new Review
            {
                Id = review.Id,
                ActivityId = review.ActivityId,
                DateCreated = date,
                IsChecked = review.IsChecked,
                Text = rndString,
                UserEmail = review.UserEmail,
            }).Result;
            Assert.AreEqual(rndString, result.Text);
            Assert.AreEqual(date, result.DateCreated);
        }

        [TestMethod]
        [ExpectedException(typeof(AggregateException))]
        public void Update_InvalidModel_Test()
        {
            _reviewOperations.UpdateAsync(new Review
            {
                Id = 999,
                ActivityId = 99999,
                DateCreated = DateTime.Now,
                IsChecked = false,
                Text = null,
                UserEmail = null,
            }).Wait();
        }

        [TestMethod]
        public void Add_Ok_Test()
        {
            var user = _context.Users.First();
            var activity = _context.Activities.First();
            var rndString = Guid.NewGuid().ToString();
            var date = DateTime.Now.Date;
            var result = _reviewOperations.AddAsync(new Review
            {
                ActivityId = activity.Id,
                DateCreated = date,
                IsChecked = false,
                Text = rndString,
                UserEmail = user.Email
            }).Result;
            Assert.AreEqual(rndString, result.Text);
            Assert.AreEqual(date, result.DateCreated);
        }

        [TestMethod]
        [ExpectedException(typeof(AggregateException))]
        public void Add_InvalidModel_Test()
        {
            _reviewOperations.AddAsync(new Review
            {
                ActivityId = 999999,
                DateCreated = DateTime.Now,
                IsChecked = false,
                Text = null,
                UserEmail = null
            }).Wait();
        }

        [TestMethod]
        public void Delete_Ok_Test()
        {
            var review = _context.Reviews.Take(10).ToList().Last();
            _reviewOperations.DeleteAsync(review.Id).Wait();

            using (var cntxt = new HobbyContext())
            {
                Assert.IsNull(cntxt.Reviews.FirstOrDefault(r => r.Id == review.Id));
            }
        }

        [TestMethod]
        [ExpectedException(typeof(AggregateException))]
        public void Delete_WrongId_Test()
        {
            _reviewOperations.DeleteAsync(9999).Wait();
        }

        [TestMethod]
        public void GetByUserEmail_Ok_Test()
        {
            var review = _context.Reviews.First();
            var result = _reviewOperations.GetByUserEmailAsync(review.UserEmail).Result;
            Assert.IsTrue(result.Any());
        }

        [TestMethod]
        public void GetByUserEmail_WrongEmail_Test()
        {
            var result = _reviewOperations.GetByUserEmailAsync("uwffwebfbyu3293bejbjs").Result;
            Assert.IsFalse(result.Any());
        }

        [TestMethod]
        public void GetByActivity_Ok_Test()
        {
            var review = _context.Reviews.First();
            var result = _reviewOperations.GetByActivityAsync(review.ActivityId).Result;
            Assert.IsTrue(result.Any());
        }

        [TestMethod]
        public void GetByActivity_WrongActitivy_Test()
        {
            var result = _reviewOperations.GetByActivityAsync(999999).Result;
            Assert.IsFalse(result.Any());
        }
    }
}
