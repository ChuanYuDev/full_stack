using CoreBusiness;
using UseCases.DataStorePluginInterfaces;

namespace UseCases.ProductsUseCases
{
    public class AddProductUseCase : IAddProductUseCase
    {
        private readonly IProductsRepository productsRepository;
        public AddProductUseCase(IProductsRepository productsRepository)
        {
            this.productsRepository = productsRepository;
        }
        public void Execute(Product product)
        {
            productsRepository.AddProduct(product);
        }
    }
}