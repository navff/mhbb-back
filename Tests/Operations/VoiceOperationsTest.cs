using System;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Models.Entities;
using Models.Operations;

namespace Tests.Operations
{
    [TestClass]
    public class VoiceOperationsTest : BaseTest
    {
        private VoiceOperations _voiceOperations;

        public VoiceOperationsTest()
        {
            _voiceOperations = new VoiceOperations(_context);
        }

        [TestMethod]
        public void Add_Positive_Test()
        {
            var user = _context.Users.First();
            var activity = _context.Activities.First();
            _voiceOperations.AddVoice(user.Email, VoiceValue.Positive, activity.Id).Wait();
        }

        [TestMethod]
        public void Add_Negative_Test()
        {
            var user = _context.Users.First();
            var activity = _context.Activities.First();
            _voiceOperations.AddVoice(user.Email, VoiceValue.Negative, activity.Id).Wait();
        }

        

    }
}