using System;
using System.Collections.Generic;
using System.Data.Entity;
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
    public class PictureOperationsTest : BaseTest
    {
        private PictureOperations _pictureOperations;

        public PictureOperationsTest()
        {
            _pictureOperations = new PictureOperations(_context);
        }

        [TestMethod]
        public void Get_Ok_Test()
        {
            var picture = _context.Pictures.First();
            var result = _pictureOperations.GetAsync(picture.Id).Result;
            Assert.AreEqual(picture.Id, result.Id);
        }

        [TestMethod]
        public void Get_WrongId_Test()
        {
            var result = _pictureOperations.GetAsync(99999).Result;
            Assert.IsNull(result);
        }

        [TestMethod]
        public void Add_Ok_Test()
        {
            var tempFile = _context.TempFiles.First();
            var activity = _context.Activities.Take(10).ToList().Last();
            var result = _pictureOperations.AddAsync(tempFile.Id, true, activity.Id, LinkedObjectType.Activity).Result;
            Assert.IsTrue(!String.IsNullOrEmpty(result.Filename));

            using (var cntxt = new HobbyContext())
            {
                var deletedTempFile = cntxt.TempFiles.FirstOrDefaultAsync(tf => tf.Id == tempFile.Id).Result;
                Assert.IsNull(deletedTempFile);
            }
        }

        [TestMethod]
        public void Delete_Ok_Test()
        {
            var picture = _context.Pictures.Take(10).ToList().Last();
            _pictureOperations.DeleteAsync(picture.Id).Wait();

            using (var cntxt = new HobbyContext())
            {
                Assert.IsNull(cntxt.Pictures.FirstOrDefault(p => p.Id == picture.Id));
            }
        }

        [TestMethod]
        public void GetByLinkedObject_Ok_Test()
        {
            var activityId = _context.Pictures.Where(p => (p.LinkedObjectType == LinkedObjectType.Activity))
                                               .Select(p => p.LinkedObjectId).First();

            var result = _pictureOperations.GetByLinkedObject(LinkedObjectType.Activity, activityId).Result.ToList();
            Assert.IsTrue(result.Any());
            Assert.IsTrue(result.First().LinkedObjectId == activityId);
        }

        [TestMethod]
        public void DeleteByLinkedObject_Ok_Test()
        {
            var activityId = _context.Pictures.Where(p => (p.LinkedObjectType == LinkedObjectType.Activity))
                                               .Select(p => p.LinkedObjectId).First();

            _pictureOperations.DeleteByLinkedObject(LinkedObjectType.Activity, activityId).Wait();

            using (var cntxt = new HobbyContext())
            {
                var pictures = cntxt.Pictures.Where(p => (p.LinkedObjectType == LinkedObjectType.Activity)
                                          && (p.LinkedObjectId == activityId));
                Assert.IsFalse(pictures.Any());
            }
        }




    }

   

}
