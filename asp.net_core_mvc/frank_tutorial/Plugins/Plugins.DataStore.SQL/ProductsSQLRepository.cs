using CoreBusiness;
using Microsoft.EntityFrameworkCore;
using UseCases.DataStorePluginInterfaces;

namespace Plugins.DataStore.SQL
{
    public class ProductsSQLRepository : IProductsRepository
    {
        private readonly MarketContext db;

        public ProductsSQLRepository(MarketContext db)
        {
            this.db = db;
        }
        public void AddProduct(Product product)
        {
            db.Products.Add(product);
            db.SaveChanges();
        }

        public void DeleteProduct(int productId)
        {
            var product = db.Products.Find(productId);

            if (product == null) return;

            db.Products.Remove(product);
            db.SaveChanges();
        }

        public Product? GetProductById(int productId, bool loadCategory = false)
        {
            if (loadCategory)
                // Include
                //      Category property inside the product is a navigation property
                //      Use Include method to populate the Category
                return db.Products.Include(x => x.Category).FirstOrDefault(x => x.ProductId == productId);

            else
                // In this case, the Category property will be null
                return db.Products.FirstOrDefault(x => x.ProductId == productId);
        }

        public IEnumerable<Product> GetProducts(bool loadCategory = false)
        {
            if (loadCategory)
                // Order the Products by CategoryId
                return db.Products.Include(x => x.Category).OrderBy(x => x.CategoryId).ToList();

            else
                return db.Products.OrderBy(x => x.CategoryId).ToList();
        }

        public IEnumerable<Product> GetProductsByCategoryId(int categoryId)
        {
            return db.Products.Where(x => x.CategoryId == categoryId).ToList();
        }

        public void UpdateProduct(int productId, Product product)
        {
            if (productId != product.ProductId) return;

            var productToUpdate = db.Products.Find(productId);

            if (productToUpdate == null) return;

            productToUpdate.CategoryId = product.CategoryId;
            productToUpdate.Name = product.Name;
            productToUpdate.Quantity = product.Quantity;
            productToUpdate.Price = product.Price;
            db.SaveChanges();
        }
    }
}