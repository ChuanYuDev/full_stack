using UseCases.DataStorePluginInterfaces;

namespace UseCases.ProductsUseCases
{
    public class DeleteProductUseCase : IDeleteProductUseCase
    {
        private readonly IProductsRepository productsRepository;

        public DeleteProductUseCase(IProductsRepository productsRepository)
        {
            this.productsRepository = productsRepository;
        }
        public void Execute(int productId)
        {
            productsRepository.DeleteProduct(productId);
        }
    }
}