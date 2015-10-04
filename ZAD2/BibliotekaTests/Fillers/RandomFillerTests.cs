using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Biblioteka;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace Biblioteka.Tests {
    [TestClass()]
    public class RandomFillerTests {
        [TestMethod()]
        public void FillTest() {
            int ile = 15;

            Baza b1 = new Baza();
            int ksPrzed = b1.LiczbaKsiazek;
            int czPrzed = b1.LiczbaCzytelnikow;
            int wyPrzed = b1.LiczbaWypozyczen;

            b1.Filler = new RandomFiller(ile);
            b1.UseFiller();
            int ksPo = b1.LiczbaKsiazek;
            int czPo = b1.LiczbaCzytelnikow;
            int wyPo = b1.LiczbaWypozyczen;

            Baza b2 = new Baza(new RandomFiller(ile));

            Assert.AreEqual(ksPo , b2.LiczbaKsiazek);
            Assert.AreEqual(czPo , b2.LiczbaCzytelnikow);
            Assert.AreEqual(wyPo , b2.LiczbaWypozyczen);

            Assert.AreEqual(ksPo-ksPrzed, ile);
            Assert.AreEqual(czPo - czPrzed, ile);
            Assert.AreEqual(wyPo - wyPrzed, ile);
        }
    }
}
