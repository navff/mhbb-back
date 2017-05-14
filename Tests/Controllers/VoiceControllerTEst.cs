using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Tests.Controllers
{
    [TestClass]
    public class VoiceControllerTest : BaseControllerTest
    {
        [TestMethod]
        public void Positive_Ok_Test()
        {
            var activity = _context.Activities.First();
            string url = $"api/voice/positive/{activity.Id}";
            var result = HttpPost<int>(url, null, "ABRAKADABRA");
        }

        [TestMethod]
        public void Negative_Ok_Test()
        {
            var activity = _context.Activities.First();
            string url = $"api/voice/negative/{activity.Id}";
            var result = HttpPost<int>(url, null, "ABRAKADABRA");
        }
    }
}
