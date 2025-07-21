using CoreBusiness;
using UseCases.DataStorePluginInterfaces;

namespace UseCases.ProductsUseCases
{
    public class EditProductUseCase
    {
        private readonly IProductRepository productRepository;

        public EditProductUseCase(IProductRepository productRepository)
        {
            this.productRepository = productRepository;
        }
        public void Execute(int productId, Product product)
        {
            productRepository.UpdateProduct(productId, product);
        }
    }
}