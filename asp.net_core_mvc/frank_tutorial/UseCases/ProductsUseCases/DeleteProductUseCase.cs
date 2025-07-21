using UseCases.DataStorePluginInterfaces;

namespace UseCases.ProductsUseCases
{
    public class DeleteProductUseCase
    {
        private readonly IProductRepository productRepository;

        public DeleteProductUseCase(IProductRepository productRepository)
        {
            this.productRepository = productRepository;
        }
        public void DeleteProduct(int productId)
        {
            productRepository.DeleteProduct(productId);
        }
    }
}