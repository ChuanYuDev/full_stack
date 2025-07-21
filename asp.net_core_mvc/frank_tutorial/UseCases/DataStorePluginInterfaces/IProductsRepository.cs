using CoreBusiness;

namespace UseCases.DataStorePluginInterfaces
{
    public interface IProductsRepository
    {
        void AddProduct(Product product);
        void DeleteProduct(int productId);
        Product? GetProductById(int productId, bool loadCategory = false);
        IEnumerable<Product> GetProducts(bool loadCategory = false);
        IEnumerable<Product> GetProductsByCategoryId(int categoryId);
        void UpdateProduct(int productId, Product product);
    }
}