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
            Reader czyt = new Reader("Adam", "Telekomunista");

            int expected = baza.LiczbaCzytelnikow + 1;
            baza.Add(czyt);
            int result = baza.LiczbaCzytelnikow;

            Assert.AreEqual(expected, result);
        }

        [TestMethod()]
        public void AddKsiazkaTest() {
            Baza baza = new Baza();
            Book ks = new Book(123, "Ogniem i mieczem");

            int expected = baza.LiczbaKsiazek + 1;
            baza.Add(ks);
            int result = baza.LiczbaKsiazek;

            Assert.AreEqual(expected, result);
        }

        [TestMethod()]
        public void AddWypozyczenieTest() {
            Baza baza = new Baza();
            Book ks = new Book(123, "Ogniem i mieczem");
            Reader czyt = new Reader("Adam", "Telekomunista");

            int expected = baza.LiczbaWypozyczen + 1;
            baza.Add(new Borrow(ks, czyt));
            int result = baza.LiczbaWypozyczen;

            Assert.AreEqual(expected, result);
        }

        [TestMethod()]
        public void RemoveKsiazkaTest() {
            Baza baza = new Baza();
            Book ks = new Book(123, "Ogniem i mieczem");
            baza.Add(ks);
            int start = baza.LiczbaKsiazek;
            baza.Remove(ks);
            int end = baza.LiczbaKsiazek;
            Assert.AreEqual(end, start-1);
        }

        [TestMethod()]
        public void RemoveWypozyczenieTest() {
            Baza baza = new Baza();
            Book ks = new Book(123, "Ogniem i mieczem");
            Reader czyt = new Reader("Jan", "Nowak");
            Borrow wyp = new Borrow(ks, czyt);
            baza.Add(wyp);
            int start = baza.LiczbaWypozyczen;
            baza.Remove(wyp);
            int end = baza.LiczbaWypozyczen;
            Assert.AreEqual(end, start - 1);
        }

        [TestMethod()]
        public void RemoveCzytelnikTest() {
            Baza baza = new Baza();
            Reader cz = new Reader("Jan", "Kowalsky");
            baza.Add(cz);
            int start = baza.LiczbaCzytelnikow;
            baza.Remove(cz);
            int end = baza.LiczbaCzytelnikow;
            Assert.AreEqual(end, start - 1);
        }

        [TestMethod()]
        public void GetReaderByIdTest() {
            Baza b = new Baza(new RandomFiller(15));
            Reader czyt = new Reader("J", "K", 500);
            b.Add(czyt);

            Reader wyluskany = b.GetReaderById(500);
            Assert.AreEqual(czyt, wyluskany);
        }

        [TestMethod()]
        public void GetBookByIdTest() {
            Baza b = new Baza(new RandomFiller(15));
            var ks = new Book(43, "K");
            b.Add(ks);

            Book wyluskana = b.GetBookById(43);
            Assert.AreEqual(ks, wyluskana);
        }

        [TestMethod()]
        [ExpectedException(typeof(KeyNotFoundException))]
        public void RemoveBookWithIdTest() {
            Baza b = new Baza(new RandomFiller(15));
            var ks = new Book(43, "K");
            b.Add(ks);

            Book wyluskana = b.GetBookById(43);
            Assert.AreEqual(ks, wyluskana);

            b.RemoveBookWithId(43);
            Book wyluskana2 = b.GetBookById(43);
        }

        
        //
        [TestMethod()]
        [ExpectedException(typeof(KeyNotFoundException))]
        public void RemoveReaderWithIdTest() {
            Baza b = new Baza(new RandomFiller(15));
            Reader czyt = new Reader("J", "K", 1000);
            b.Add(czyt);

            Reader wyluskany = b.GetReaderById(1000);
            Assert.AreEqual(czyt, wyluskany);

            b.RemoveReaderWithId(1000);
            var wyluskany2 = b.GetReaderById(1000);
        }

        [TestMethod()]
        public void GetRentByNumberTest() {
            Baza b = new Baza();
            Book ks = new Book(123, "Ogniem i mieczem");
            Reader czyt1 = new Reader("Adam", "Telekomunista");
            Reader czyt2 = new Reader("Adam", "Technologik");
            Borrow wyp1 = new Borrow(ks, czyt1);
            Borrow wyp2 = new Borrow(ks, czyt2);
            b.Add(wyp1);
            b.Add(wyp2);
            Assert.AreEqual(b.GetRentByNumber(0), wyp1);
            Assert.AreNotEqual(b.GetRentByNumber(0), wyp2);
            Assert.AreEqual(b.GetRentByNumber(1), wyp2);
        }

        [TestMethod()]
        public void AnulujWypozyczenieNrTest() {
            Baza b = new Baza();
            Book ks = new Book(123, "Ogniem i mieczem");
            Reader czyt1 = new Reader("Adam", "Telekomunista");
            Reader czyt2 = new Reader("Adam", "Technologik");
            Borrow wyp1 = new Borrow(ks, czyt1);
            Borrow wyp2 = new Borrow(ks, czyt2);
            b.Add(wyp1);
            b.Add(wyp2);
            Assert.AreNotEqual(b.GetRentByNumber(0), wyp2);
            Assert.AreEqual(b.GetRentByNumber(1), wyp2);
            b.AnulujWypozyczenieNr(0);
            Assert.AreEqual(b.GetRentByNumber(0), wyp2); //Drugi element przesuniety na miejsce pierwszego, usunietego.
        }


        //ZADANIE 2
        [TestMethod()]
        public void GetBooksWithSpecifiedTitleTest() {
            Baza b = new Baza();
            Book one = new Book(1, "Ferdydurke", 2009);
            Book two = new Book(2, "Ferdydurke", 1998);
            b.Add(one);
            b.Add(two);
            b.Add(new Book(3, "Ogniem i mieczem", 1966));
            var list = b.GetBooksWithSpecifiedTitle("Ferdydurke");
            Assert.AreEqual(list.Count, 2);
            Assert.IsTrue(list.Contains(one));
            Assert.IsTrue(list.Contains(two));

            b.useLamb = true;
            list = b.GetBooksWithSpecifiedTitle("Ferdydurke");
            Assert.AreEqual(list.Count, 2);
            Assert.IsTrue(list.Contains(one));
            Assert.IsTrue(list.Contains(two));
        }

        [TestMethod()]
        public void GetBooksWithSpecifiedIssueYearTest() {
            Baza b = new Baza(new MassFiller(1000));
            bool isRight = true;
            int minYear = 1990;
            int maxYear = 2010;
            b.Add(new Book(100000, "Potop", 2000));
            var list = b.GetBooksWithSpecifiedIssueYear(minYear,maxYear);
            Assert.IsTrue(list.Count > 0);
            foreach(Book boo in list)
                if (boo.IssueYear < minYear || boo.IssueYear > maxYear) {
                    isRight = false;
                    break;
                }
            
            Assert.IsTrue(isRight);

            b.useLamb = true;
            isRight = true;
            list = b.GetBooksWithSpecifiedIssueYear(minYear, maxYear);
            Assert.IsTrue(list.Count > 0);
            foreach (Book boo in list)
                if (boo.IssueYear < minYear || boo.IssueYear > maxYear) {
                    isRight = false;
                    break;
                }

            Assert.IsTrue(isRight);
        }

        [TestMethod()]
        public void GetBooksWithSpecifiedIssueYearTest2() {
            Baza b = new Baza(new MassFiller(1000));
            bool isRight = true;
            int minYear = 1990;
            int maxYear = 2010;
            b.Add(new Book(100000, "Potop", 2000));
            var list = b.GetBooksWithSpecifiedIssueYear2(minYear, maxYear);
            Assert.IsTrue(list.Count > 0);
            foreach (SimpleClass boo in list)
                if (boo.intValue < minYear || boo.intValue > maxYear) {
                    isRight = false;
                    break;
                }

            Assert.IsTrue(isRight);

            b.useLamb = true;
            isRight = true;

            list = b.GetBooksWithSpecifiedIssueYear2(minYear, maxYear);
            Assert.IsTrue(list.Count > 0);
            foreach (SimpleClass boo in list)
                if (boo.intValue < minYear || boo.intValue > maxYear) {
                    isRight = false;
                    break;
                }

            Assert.IsTrue(isRight);
        }

        [TestMethod()]
        public void GetAllAuthorsTest() {
            Baza b = new Baza();
            string autor1 = "Witold Gombrowicz";
            string autor2 = "Julian Tuwim";
            string autor3 = "Juliusz Slowacki";
            b.Add(new Book(1, "Ferdydurke", 1990, autor1));
            b.Add(new Book(2, "Transatlantyk", 1991, autor1));
            b.Add(new Book(3, "Pan Tadeusz", 1991, autor2));
            b.Add(new Book(4, "Jakies wiersze", 1970, autor2));
            b.Add(new Book(5, "Dziady cz. 3", 1920, autor3));

            List<string> lst = b.GetAllAuthors();
            Assert.AreEqual(lst.Count, 3);
            Assert.IsTrue(lst.Contains(autor1));
            Assert.IsTrue(lst.Contains(autor2));
            Assert.IsTrue(lst.Contains(autor3));

            b.useLamb = true;

            lst = b.GetAllAuthors();
            Assert.AreEqual(lst.Count, 3);
            Assert.IsTrue(lst.Contains(autor1));
            Assert.IsTrue(lst.Contains(autor2));
            Assert.IsTrue(lst.Contains(autor3));
        }

        [TestMethod()]
        public void GetLatestBookTest() {
            Baza b = new Baza(new MassFiller(100));
            Book newest = new Book(333, "Lowca androidow", 4200, "Philip K. Dick");
            b.Add(newest);
            Assert.AreEqual(b.GetLatestBook(), newest);
            b.useLamb = true;
            Assert.AreEqual(b.GetLatestBook(), newest);
        }

        [TestMethod()]
        public void GetReadersWithBorrowsTest() {
            Baza b = new Baza(new RandomFiller(10));
            Reader reader = new Reader("Waclaw", "Waclaw");
            Reader noReader = new Reader("Janusz", "Janusz");
            b.Add(reader);
            b.Add(noReader);
            b.Add(new Borrow(b.GetLatestBook(), reader));

            var readingPeople = b.GetReadersWithBorrows();
            Assert.IsTrue(readingPeople.Contains(reader));
            Assert.IsFalse(readingPeople.Contains(noReader));

            b.useLamb = true;

            readingPeople = b.GetReadersWithBorrows();
            Assert.IsTrue(readingPeople.Contains(reader));
            Assert.IsFalse(readingPeople.Contains(noReader));
        }

        [TestMethod()]
        public void GetDistinctBorrowsTest() {
            Baza b = new Baza();
            Reader r1 = new Reader("Waclaw", "Waclaw");
            Reader r2 = new Reader("Janusz", "Janusz");
            Book b1 = new Book(2, "Transatlantyk", 1991, "Witold Gombrowicz");
            Book b2 = new Book(3, "Pan Tadeusz", 1991, "Adam Mickiewicz");
            b.Add(new Borrow(b1, r1));
            b.Add(new Borrow(b2, r2, new DateTime(100000)));
            Assert.AreEqual(b.GetDistinctBorrows().Count, 2);
            b.Add(new Borrow(b2, r2, new DateTime(100000)));
            Assert.AreEqual(b.GetDistinctBorrows().Count, 2);
            b.Add(new Borrow(b2, r2, new DateTime(25000)));
            Assert.AreEqual(b.GetDistinctBorrows().Count, 2);
            b.Add(new Borrow(b2, r1, new DateTime(25000)));
            Assert.AreEqual(b.GetDistinctBorrows().Count, 3);
        }

        [TestMethod()]
        public void GetDistinctBorrowsLambTest() {
            Baza b = new Baza();
            b.useLamb = true;
            Reader r1 = new Reader("Waclaw", "Waclaw");
            Reader r2 = new Reader("Janusz", "Janusz");
            Book b1 = new Book(2, "Transatlantyk", 1991, "Witold Gombrowicz");
            Book b2 = new Book(3, "Pan Tadeusz", 1991, "Adam Mickiewicz");
            b.Add(new Borrow(b1, r1));
            b.Add(new Borrow(b2, r2, new DateTime(100000)));
            Assert.AreEqual(b.GetDistinctBorrows().Count, 2);
            b.Add(new Borrow(b2, r2, new DateTime(100000)));
            Assert.AreEqual(b.GetDistinctBorrows().Count, 2);
            b.Add(new Borrow(b2, r2, new DateTime(25000)));
            Assert.AreEqual(b.GetDistinctBorrows().Count, 2);
            b.Add(new Borrow(b2, r1, new DateTime(25000)));
            Assert.AreEqual(b.GetDistinctBorrows().Count, 3);
        }

        
    }
}
