﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Models;

namespace Tests
{
    public class BaseTest
    {
        protected HobbyContext _context;

        public BaseTest()
        {
            _context = new HobbyContext();
        }


        [TestCleanup]
        public void Cleanup()
        {
            _context.Dispose();
        }
    }
}
