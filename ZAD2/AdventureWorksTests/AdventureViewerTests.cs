using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AdventureWorks;
using Microsoft.VisualStudio.TestTools.UnitTesting;
namespace AdventureWorks.Tests {
    [TestClass()]
    public class AdventureViewerTests {
        [TestMethod()]
        public void GetRecentlyReviewedProductsTest() {
            AdventureViewer av = new AdventureViewer();
            var list = av.GetRecentlyReviewedProducts(3);
            Assert.AreEqual(list[0].Name, "HL Mountain Pedal");
            Assert.AreEqual(list[1].Name, "Road-550-W Yellow, 40");
            Assert.AreEqual(list[2].Name, "HL Mountain Pedal");
        }

        [TestMethod()]
        public void GetProductNamesByVendorNameTest() {
            AdventureViewer av = new AdventureViewer();
            var list = av.GetProductNamesByVendorName("Mountain Works");
            Assert.IsTrue(list.Contains("Hex Nut 5"));
            Assert.IsTrue(list.Contains("Hex Nut 8"));
            Assert.IsFalse(list.Contains("Hex Nut 1"));
        }

        [TestMethod()]
        public void GetNProductsSortedByCategoryTest() {
            AdventureViewer av = new AdventureViewer();
            var list = av.GetNProductsSortedByCategory(500);
            Assert.AreEqual(list[0].Name, "Adjustable Race");
            Assert.AreEqual(list[100].Name, "Lock Nut 12");
            Assert.AreEqual(list[300].Name, "Road-650 Black, 58");
            Assert.AreEqual(list[450].Name, "LL Road Handlebars");
        }

        [TestMethod()]
        public void GetTotalStandardCostByCategoryTest() {
            AdventureViewer av = new AdventureViewer();
            var list = av.GetAllCategories();
            var lsttt = av.GetAllCategories();
            Assert.AreEqual(av.GetTotalStandardCostByCategory(lsttt[0]), 92092);
            Assert.AreEqual(av.GetTotalStandardCostByCategory(lsttt[1]), 35930);
        }
    }
}
