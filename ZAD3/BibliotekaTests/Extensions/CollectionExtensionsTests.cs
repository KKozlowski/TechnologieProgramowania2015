using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Extensions;
using Biblioteka;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace Extensions.Tests {
    [TestClass()]
    public class CollectionExtensionsTests {
        [TestMethod()]
        public void GetMostActiveReadersTest() {
            Book b = new Book(50, "50 Twarzy Greya");
            List<Reader> list = new List<Reader>();
            for (int i = 0; i < 15; i++)
                list.Add(new Reader("Janina", "Pornijska", 1000000+i));

            for (int i = 0; i < 10; i++) {
                new Borrow(b, list[1]);
            }

            Assert.AreEqual(list[1].Borrows.Count, 10);

            for (int i = 0; i < 100; i++) {
                new Borrow(b, list[3]);
            }

            for (int i = 0; i < 112; i++) {
                new Borrow(b, list[10]);
            }

            List<Reader> active = list.GetMostActiveReaders(3);
            Assert.AreEqual(active.Count, 3);
            Assert.AreEqual(active[0].Borrows.Count, 112);
            Assert.AreEqual(active[0].ID, 1000010);
        }

        [TestMethod()]
        public void SplitTest() {
            List<Reader> list = new List<Reader>();
            for (int i = 0; i < 101; i++) {
                list.Add(new Reader("Jan", "Kowalski", 100000+i));
            }

            List<Reader>[] splitted = list.Split();
            Assert.AreEqual(splitted[0].Count, 10);
            Assert.AreEqual(splitted[10].Count, 1);
            Assert.AreEqual(splitted[0][5].ID, 100005);
        }

        [TestMethod()]
        public void GetNotBorrowedBooksTest() {
            Dictionary<int, Book> books = new Dictionary<int, Book>();
            Reader r1 = new Reader("Waclaw", "Waclaw");
            Book b1 = new Book(2, "Transatlantyk", 1991, "Witold Gombrowicz");
            Book b2 = new Book(3, "Pan Tadeusz", 1991, "Adam Mickiewicz");
            books.Add(0, b1);
            books.Add(1, b2);

            Assert.AreEqual(2, books.GetNotBorrowedBooks().Count);
            new Borrow(books[0], r1);
            Assert.AreEqual(1, books.GetNotBorrowedBooks().Count);
            new Borrow(books[1], r1);
            Assert.AreEqual(0, books.GetNotBorrowedBooks().Count);
        }
    }
}
