using UseCases.DataStorePluginInterfaces;

namespace UseCases.ProductsUseCases
{
    public class SellProductUseCase
    {
        private readonly IProductRepository productRepository;

        public SellProductUseCase(
            IProductRepository productRepository,
            
        )
        {
            this.productRepository = productRepository;
        }
        public void Execute(string cashierName, int productId, int qtyToSell)
        {
            var product = productRepository.GetProductById(productId);

            if (product == null)
                return;
            
            
        }
    }
}