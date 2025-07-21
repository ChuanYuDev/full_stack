using CoreBusiness;
using UseCases.DataStorePluginInterfaces;

namespace UseCases.ProductsUseCases
{
    public class ViewProductsUseCase : IViewProductsUseCase
    {
        private readonly IProductsRepository productsRepository;

        public ViewProductsUseCase(IProductsRepository productsRepository)
        {
            this.productsRepository = productsRepository;
        }
        public IEnumerable<Product> Execute(bool loadCategory = false)
        {
            return productsRepository.GetProducts(loadCategory);
        }
    }
}