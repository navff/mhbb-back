using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using API.ViewModels;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Data.Entity;

namespace Tests.Controllers
{
    [TestClass]
    public class ReservationControllerTest : BaseControllerTest
    {
        [TestMethod]
        public void Get_Ok_Test()
        {
            var reservation = _context.Reservations.Include(r => r.Activity).First();
            string url = $"api/reservation/{reservation.Id}";
            var result = HttpGet<ReservationViewModelGet>(url);
            Assert.AreEqual(reservation.Name, result.Name);
            Assert.AreEqual(reservation.Activity.Name, result.Activity.Name);
        }

        [TestMethod]
        [ExpectedException(typeof(WebException))]
        public void Get_WrongId_Test()
        {
            string url = $"api/reservation/9999";
            var result = HttpGet<ReservationViewModelGet>(url);
            Assert.IsNull(result==null);
        }

        [TestMethod]
        public void Put_Ok_Test()
        {
            var reservation = _context.Reservations.Include(r => r.Activity).First();
            var rndString = Guid.NewGuid().ToString();
            var viewModel = new ReservationViewModelPost
            {
                Name = rndString,
                UserEmail = reservation.UserEmail,
                Phone = rndString,
                ActivityId = reservation.ActivityId,
                Comment = rndString
            };
            string url = $"api/reservation/{reservation.Id}";
            var result = HttpPut<ReservationViewModelGet>(url, viewModel, "ABRAKADABRA");
            Assert.AreEqual(rndString, result.Name);
            Assert.AreEqual(rndString, result.Phone);
            Assert.AreEqual(rndString, result.Comment);
            Assert.AreEqual(reservation.Activity.Name, result.Activity.Name);
        }

        [TestMethod]
        [ExpectedException(typeof(WebException))]
        public void Put_WrongId_Test()
        {
            var rndString = Guid.NewGuid().ToString();
            var viewModel = new ReservationViewModelPost
            {
                Name = rndString,
                UserEmail = rndString,
                Phone = rndString,
                ActivityId = 1,
                Comment = rndString
            };
            string url = $"api/reservation/9999";
            HttpPut<ReservationViewModelGet>(url, viewModel, "ABRAKADABRA");
        }

        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void Put_WrongModel_Test()
        {
            var reservation = _context.Reservations.First();
            var rndString = Guid.NewGuid().ToString();
            var viewModel = new ReservationViewModelPost
            {
                Name = rndString,
                UserEmail = rndString,
                Phone = rndString,
                ActivityId = 1,
                Comment = rndString
            };
            string url = $"api/reservation/{reservation.Id}";
            HttpPut<ReservationViewModelGet>(url, viewModel, "ABRAKADABRA");
        }

        [TestMethod]
        public void Post_Ok_Test()
        {
            var rndString = Guid.NewGuid().ToString();
            var activity = _context.Activities.First();

            var viewModel = new ReservationViewModelPost
            {
                Name = rndString,
                UserEmail = rndString+"@mhbb.ru",
                Phone = rndString,
                Comment = rndString,
                ActivityId = activity.Id
            };

            string url = $"api/reservation";
            var result = HttpPost<ReservationViewModelGet>(url, viewModel);
            Assert.AreEqual(rndString, result.Name);
            Assert.AreEqual(rndString, result.Comment);
            Assert.AreEqual(rndString, result.Phone);
            Assert.AreEqual(activity.Name, result.Activity.Name);
        }

        [TestMethod]
        [ExpectedException(typeof(WebException))]
        public void Post_WrongModel_Test()
        {

            var viewModel = new ReservationViewModelPost
            {
                Name = null,
                UserEmail = null,
                Phone = null,
                Comment = null,
                ActivityId = 9999
            };

            string url = $"api/reservation";
            HttpPost<ReservationViewModelGet>(url, viewModel);
        }

        [TestMethod]
        public void Delete_Ok_Test()
        {
            var reservation = _context.Reservations.Take(10).ToList().Last();
            string url = $"api/reservation/{reservation.Id}";
            HttpDelete<string>(url, "ABRAKADABRA");
        }

        [TestMethod]
        [ExpectedException(typeof(WebException))]
        public void Delete_WrongId_Test()
        {
            string url = $"api/reservation/99999";
            HttpDelete<string>(url, "ABRAKADABRA");
        }

        [TestMethod]
        public void Search_ByName_Test()
        {
            var reservation = _context.Reservations.Take(10).ToList().Last();
            string url = $"api/reservation/search?word={reservation.Name.Substring(2)}";
            var result = HttpGet<IEnumerable<ReservationViewModelGet>>(url);
            Assert.IsTrue(result.Any());
        }

        [TestMethod]
        public void Search_ByEmail_Test()
        {
            var reservation = _context.Reservations.Take(10).ToList().Last();
            string url = $"api/reservation/search?word={reservation.UserEmail.Substring(2)}";
            var result = HttpGet<IEnumerable<ReservationViewModelGet>>(url);
            Assert.IsTrue(result.Any());
        }

        [TestMethod]
        public void Search_ByPhone_Test()
        {
            var reservation = _context.Reservations.Take(10).ToList().Last();
            string url = $"api/reservation/search?word={reservation.Phone.Substring(2)}";
            var result = HttpGet<IEnumerable<ReservationViewModelGet>>(url);
            Assert.IsTrue(result.Any());
        }

        [TestMethod]
        public void Search_NoResults_Test()
        {
            string url = $"api/reservation/search?word=jd8b389ebe83b3298s";
            var result = HttpGet<IEnumerable<ReservationViewModelGet>>(url);
            Assert.IsFalse(result.Any());
        }

    }
}
