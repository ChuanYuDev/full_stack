using CoreBusiness;
using UseCases.DataStorePluginInterfaces;

namespace UseCases.ProductsUseCases
{
    public class ViewSelectedProductUseCase : IViewSelectedProductUseCase
    {
        private readonly IProductRepository productRepository;

        public ViewSelectedProductUseCase(IProductRepository productRepository)
        {
            this.productRepository = productRepository;
        }

        public Product? Execute(int productId)
        {
            return productRepository.GetProductById(productId);
        }
    }
}