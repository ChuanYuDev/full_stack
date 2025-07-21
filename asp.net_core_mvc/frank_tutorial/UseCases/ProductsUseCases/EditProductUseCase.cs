using CoreBusiness;
using UseCases.DataStorePluginInterfaces;

namespace UseCases.ProductsUseCases
{
    public class EditProductUseCase : IEditProductUseCase
    {
        private readonly IProductsRepository productsRepository;

        public EditProductUseCase(IProductsRepository productsRepository)
        {
            this.productsRepository = productsRepository;
        }
        public void Execute(int productId, Product product)
        {
            productsRepository.UpdateProduct(productId, product);
        }
    }
}