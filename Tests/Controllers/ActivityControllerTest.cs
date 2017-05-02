using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Models.Operations;

namespace Tests.Controllers
{
    [TestClass]
    public class ActivityControllerTest : BaseControllerTest
    {
        private ActivityOperations _activityOperations;

        public ActivityControllerTest()
        {
            _activityOperations = new ActivityOperations(_context);
        }

        [TestMethod]
        public void Get_Ok_Test()
        {
            throw new NotImplementedException();
        }

        [TestMethod]
        public void Get_WrongId_Test()
        {
            throw new NotImplementedException();
        }

        [TestMethod]
        public void Search_Ok_Test()
        {
            throw new NotImplementedException();
        }

        [TestMethod]
        public void Search_Pagination_Test()
        {
            throw new NotImplementedException();
        }

        [TestMethod]
        public void Put_Ok_Test()
        {
            throw new NotImplementedException();
        }

        [TestMethod]
        public void Post_Ok_Test()
        {
            throw new NotImplementedException();
        }

        [TestMethod]
        public void Delete_Ok_Test()
        {
            throw new NotImplementedException();
        }
    }
}
