using CoreBusiness;
using UseCases.DataStorePluginInterfaces;

namespace UseCases.ProductsUseCases
{
    public class ViewSelectedProductUseCase : IViewSelectedProductUseCase
    {
        private readonly IProductsRepository productsRepository;

        public ViewSelectedProductUseCase(IProductsRepository productsRepository)
        {
            this.productsRepository = productsRepository;
        }

        public Product? Execute(int productId)
        {
            return productsRepository.GetProductById(productId);
        }
    }
}