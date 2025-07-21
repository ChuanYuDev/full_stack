using CoreBusiness;
using UseCases.DataStorePluginInterfaces;

namespace UseCases.ProductsUseCases
{
    public class ViewProductsInCategoryUseCase : IViewProductsInCategoryUseCase
    {
        private readonly IProductsRepository productsRepository;

        public ViewProductsInCategoryUseCase(IProductsRepository productsRepository)
        {
            this.productsRepository = productsRepository;
        }
        public IEnumerable<Product> Execute(int categoryId)
        {
            return productsRepository.GetProductsByCategoryId(categoryId);
        }
    }
}