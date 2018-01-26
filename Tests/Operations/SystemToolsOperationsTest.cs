using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Models.Operations;

namespace Tests.Operations
{
    [TestClass]
    public class SystemToolsOperationsTest : BaseTest
    {
        private PictureOperations _pictureOperations;
        private SystemToolsOperations _systemToolsOperations;

        public SystemToolsOperationsTest()
        {
            _pictureOperations = new PictureOperations(_context);
            _systemToolsOperations = new SystemToolsOperations(_context, _pictureOperations);
        }

        [TestMethod]
        public void ReformatOnePicture_Ok_Test()
        {
            var pic = _context.Pictures.First();
            _systemToolsOperations.ReformatOnePicture(pic.Id).Wait();

        }

        [TestMethod]
        public void ReformatAllPictures_Ok_Test()
        {
            _systemToolsOperations.ReformatAllPictures().Wait();
        }



    }

}
