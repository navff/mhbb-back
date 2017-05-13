using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Models;
using Models.Entities;
using Models.Operations;

namespace Tests.Operations
{
    [TestClass]
    public class ReservationOperationsTest : BaseTest
    {
        private readonly ReservationOperations _reservationOperations;

        public ReservationOperationsTest()
        {
            _reservationOperations = new ReservationOperations(_context);
        }

        [TestMethod]
        public void Add_Ok_Test()
        {
            var rndString = Guid.NewGuid().ToString();
            var activity = _context.Activities.First();
            var user = _context.Users.First();

            var result = _reservationOperations.AddAsync(new Reservation
            {
                Name    = rndString,
                ActivityId = activity.Id,
                Comment = rndString,
                Phone = rndString,
                UserEmail = user.Email
            }).Result;

            Assert.AreEqual(rndString, result.Name);
            Assert.AreEqual(rndString, result.Comment);
            Assert.IsFalse(String.IsNullOrEmpty(result.Activity.Name));
            Assert.IsFalse(String.IsNullOrEmpty(result.User.Email));
        }

        [TestMethod]
        public void AddEndRegister_Ok_Test()
        {
            var rndString = Guid.NewGuid().ToString();
            var activity = _context.Activities.First();
            var email = rndString + "@mhbb.ru";
            var result = _reservationOperations.AddAsync(new Reservation
            {
                Name = rndString,
                ActivityId = activity.Id,
                Comment = rndString,
                Phone = rndString,
                UserEmail = email
            }).Result;

            Assert.AreEqual(rndString, result.Name);
            Assert.AreEqual(rndString, result.Comment);
            Assert.IsFalse(String.IsNullOrEmpty(result.Activity.Name));
            Assert.IsFalse(String.IsNullOrEmpty(result.User.Email));
        }

        [TestMethod]
        [ExpectedException(typeof(AggregateException))]
        public void Add_WrongModel_Test()
        {
            _reservationOperations.AddAsync(new Reservation
            {
                Name = "",
                Comment = "",
                Phone = "",
                UserEmail = ""
            }).Wait();
        }

        [TestMethod]
        public void Get_Ok_Test()
        {
            var reservation = _context.Reservations.First();
            var result = _reservationOperations.GetAsync(reservation.Id).Result;

            Assert.AreEqual(reservation.Name, result.Name);
        }

        [TestMethod]
        public void Get_WrongId_Test()
        {
            var result = _reservationOperations.GetAsync(9999).Result;
            Assert.IsNull(result);
        }

        [TestMethod]
        public void Update_Ok_Test()
        {
            var rndString = Guid.NewGuid().ToString();
            var reservation = _context.Reservations.First();
            var result = _reservationOperations.UpdateAsync(new Reservation
            {
                Name = rndString,
                Id = reservation.Id,
                UserEmail = reservation.UserEmail,
                ActivityId = reservation.ActivityId,
                Comment = rndString,
                Phone = rndString
            }).Result;
            Assert.AreEqual(rndString, result.Name);
            Assert.AreEqual(rndString, result.Comment);
            Assert.AreEqual(rndString, result.Phone);

        }

        [TestMethod]
        [ExpectedException(typeof(AggregateException))]
        public void Update_WrongModel_Test()
        {
            var reservation = _context.Reservations.First();
            _reservationOperations.UpdateAsync(new Reservation
            {
                Name = "",
                Id = reservation.Id,
                UserEmail = null,
                ActivityId = reservation.ActivityId,
                Comment = "",
                Phone = ""
            }).Wait();
        }

        [TestMethod]
        public void Delete_Ok_Test()
        {
            var reservation = _context.Reservations.Take(10).ToList().Last();
            _reservationOperations.DeleteAsync(reservation.Id).Wait();

            using (var cntxt = new HobbyContext())
            {
                Assert.IsNull(cntxt.Reservations.Find(reservation.Id));
            }
        }

        [TestMethod]
        [ExpectedException(typeof(AggregateException))]
        public void Delete_WrongId_Test()
        {
            _reservationOperations.DeleteAsync(9999).Wait();
        }

        [TestMethod]
        public void Search_Name_Test()
        {
            var reservation = _context.Reservations.First();
            var result = _reservationOperations.SearchAsync(reservation.Name.Substring(2)).Result;
            Assert.IsTrue(result.Any(r => r.Id == reservation.Id));
        }

        [TestMethod]
        public void Search_Phone_Test()
        {
            var reservation = _context.Reservations.First();
            var result = _reservationOperations.SearchAsync(reservation.Phone.Substring(2)).Result;
            Assert.IsTrue(result.Any(r => r.Id == reservation.Id));
        }

        [TestMethod]
        public void Search_Email_Test()
        {
            var reservation = _context.Reservations.First();
            var result = _reservationOperations.SearchAsync(reservation.UserEmail.Substring(2)).Result;
            Assert.IsTrue(result.Any(r => r.Id == reservation.Id));
        }
    }
}
