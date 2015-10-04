using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Linq;

namespace AdventureWorks {
    public class AdventureViewer {
        private OneDataContext db = new OneDataContext();

        public List<string> GetProductNamesByVendorName(string vendorName) {
            Table<ProductVendor> PV = db.GetTable<ProductVendor>();
            List<string> result =
                (from pv in PV
                 where pv.Vendor.Name == vendorName
                 select pv.Product.Name
                ).ToList();

            return result;
        }

        public List<Product> GetRecentlyReviewedProducts(int howManyReviews) {
            Table<ProductReview> productR = db.GetTable<ProductReview>();
            return (from pr in productR
                    orderby pr.ReviewDate descending
                    select pr.Product).Take(howManyReviews).ToList();
        }
        

        public List<Product> GetNProductsSortedByCategory(int N) {
            Table<Product> products = db.GetTable<Product>();
            return (from prod in products
                    select prod)
                    .OrderBy(prod => prod.ProductSubcategory.ProductCategory.Name)
                    .ThenBy(prod => prod.Name).Take(N).ToList();
        }

        public int GetTotalStandardCostByCategory(ProductCategory category) {
            Table<Product> products = db.GetTable<Product>();
            return (int)(from p in products
                    where p.ProductSubcategory.ProductCategory.ProductCategoryID == category.ProductCategoryID
                    select p.StandardCost).Sum();
        }

        //Metody spoza zadania
        public List<string> GetAllReviewedProductsNames() {
            Table<ProductReview> productR = db.GetTable<ProductReview>();
            return (from pr in productR
                    select pr.Product.Name).ToList();

        }

        public List<ProductCategory> GetAllCategories() {
            return db.GetTable<ProductCategory>().ToList();
        }

        public void WriteList<T>(List<T> writeIt) {
            foreach (T v in writeIt)
                Console.WriteLine(v);
        }

    }
}
