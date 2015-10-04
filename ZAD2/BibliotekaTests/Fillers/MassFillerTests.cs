using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Biblioteka;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace Biblioteka.Tests {
    [TestClass()]
    public class MassFillerTests {
        [TestMethod()]
        public void MassFillTest1000() {
            Baza b = new Baza(new MassFiller(1000));
            Assert.IsTrue(true);
        }

        [TestMethod()]
        public void MassFillTest10000() {
            Baza b = new Baza(new MassFiller(10000));
            Assert.IsTrue(true);
        }

        [TestMethod()]
        public void MassFillTest100000() {
            Baza b = new Baza(new MassFiller(100000));
            Assert.IsTrue(true);
        }
    }
}
