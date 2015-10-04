using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Biblioteka;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace Biblioteka.Tests {
    [TestClass()]
    public class BazaTests {
        [TestMethod()]
        public void AddCzytelnikTest() {
            Baza baza = new Baza();
            Czytelnik czyt = new Czytelnik("Adam", "Telekomunista");

            int expected = baza.LiczbaCzytelnikow + 1;
            baza.Add(czyt);
            int result = baza.LiczbaCzytelnikow;

            Assert.AreEqual(expected, result);
        }

        [TestMethod()]
        public void AddKsiazkaTest() {
            Baza baza = new Baza();
            Ksiazka ks = new Ksiazka(123, "Ogniem i mieczem");

            int expected = baza.LiczbaKsiazek + 1;
            baza.Add(ks);
            int result = baza.LiczbaKsiazek;

            Assert.AreEqual(expected, result);
        }

        [TestMethod()]
        public void AddWypozyczenieTest() {
            Baza baza = new Baza();
            Ksiazka ks = new Ksiazka(123, "Ogniem i mieczem");
            Czytelnik czyt = new Czytelnik("Adam", "Telekomunista");

            int expected = baza.LiczbaWypozyczen + 1;
            baza.Add(new Wypozyczenie(ks, czyt));
            int result = baza.LiczbaWypozyczen;

            Assert.AreEqual(expected, result);
        }

        [TestMethod()]
        public void RemoveKsiazkaTest() {
            Baza baza = new Baza();
            Ksiazka ks = new Ksiazka(123, "Ogniem i mieczem");
            baza.Add(ks);
            int start = baza.LiczbaKsiazek;
            baza.Remove(ks);
            int end = baza.LiczbaKsiazek;
            Assert.AreEqual(end, start-1);
        }

        [TestMethod()]
        public void RemoveWypozyczenieTest() {
            Baza baza = new Baza();
            Ksiazka ks = new Ksiazka(123, "Ogniem i mieczem");
            Czytelnik czyt = new Czytelnik("Jan", "Nowak");
            Wypozyczenie wyp = new Wypozyczenie(ks, czyt);
            baza.Add(wyp);
            int start = baza.LiczbaWypozyczen;
            baza.Remove(wyp);
            int end = baza.LiczbaWypozyczen;
            Assert.AreEqual(end, start - 1);
        }

        [TestMethod()]
        public void RemoveCzytelnikTest() {
            Baza baza = new Baza();
            Czytelnik cz = new Czytelnik("Jan", "Kowalsky");
            baza.Add(cz);
            int start = baza.LiczbaCzytelnikow;
            baza.Remove(cz);
            int end = baza.LiczbaCzytelnikow;
            Assert.AreEqual(end, start - 1);
        }

        [TestMethod()]
        public void GetReaderByIdTest() {
            Baza b = new Baza(new RandomFiller(15));
            Czytelnik czyt = new Czytelnik("J", "K", 500);
            b.Add(czyt);

            Czytelnik wyluskany = b.GetReaderById(500);
            Assert.AreEqual(czyt, wyluskany);
        }

        [TestMethod()]
        public void GetBookByIdTest() {
            Baza b = new Baza(new RandomFiller(15));
            var ks = new Ksiazka(43, "K");
            b.Add(ks);

            Ksiazka wyluskana = b.GetBookById(43);
            Assert.AreEqual(ks, wyluskana);
        }

        [TestMethod()]
        [ExpectedException(typeof(KeyNotFoundException))]
        public void RemoveBookWithIdTest() {
            Baza b = new Baza(new RandomFiller(15));
            var ks = new Ksiazka(43, "K");
            b.Add(ks);

            Ksiazka wyluskana = b.GetBookById(43);
            Assert.AreEqual(ks, wyluskana);

            b.RemoveBookWithId(43);
            Ksiazka wyluskana2 = b.GetBookById(43);
        }

        
        //
        [TestMethod()]
        [ExpectedException(typeof(KeyNotFoundException))]
        public void RemoveReaderWithIdTest() {
            Baza b = new Baza(new RandomFiller(15));
            Czytelnik czyt = new Czytelnik("J", "K", 1000);
            b.Add(czyt);

            Czytelnik wyluskany = b.GetReaderById(1000);
            Assert.AreEqual(czyt, wyluskany);

            b.RemoveReaderWithId(1000);
            var wyluskany2 = b.GetReaderById(1000);
        }

        [TestMethod()]
        public void GetRentByNumberTest() {
            Baza b = new Baza();
            Ksiazka ks = new Ksiazka(123, "Ogniem i mieczem");
            Czytelnik czyt1 = new Czytelnik("Adam", "Telekomunista");
            Czytelnik czyt2 = new Czytelnik("Adam", "Technologik");
            Wypozyczenie wyp1 = new Wypozyczenie(ks, czyt1);
            Wypozyczenie wyp2 = new Wypozyczenie(ks, czyt2);
            b.Add(wyp1);
            b.Add(wyp2);
            Assert.AreEqual(b.GetRentByNumber(0), wyp1);
            Assert.AreNotEqual(b.GetRentByNumber(0), wyp2);
            Assert.AreEqual(b.GetRentByNumber(1), wyp2);
        }

        [TestMethod()]
        public void AnulujWypozyczenieNrTest() {
            Baza b = new Baza();
            Ksiazka ks = new Ksiazka(123, "Ogniem i mieczem");
            Czytelnik czyt1 = new Czytelnik("Adam", "Telekomunista");
            Czytelnik czyt2 = new Czytelnik("Adam", "Technologik");
            Wypozyczenie wyp1 = new Wypozyczenie(ks, czyt1);
            Wypozyczenie wyp2 = new Wypozyczenie(ks, czyt2);
            b.Add(wyp1);
            b.Add(wyp2);
            Assert.AreNotEqual(b.GetRentByNumber(0), wyp2);
            Assert.AreEqual(b.GetRentByNumber(1), wyp2);
            b.AnulujWypozyczenieNr(0);
            Assert.AreEqual(b.GetRentByNumber(0), wyp2); //Drugi element przesuniety na miejsce pierwszego, usunietego.
        }

        
    }
}
