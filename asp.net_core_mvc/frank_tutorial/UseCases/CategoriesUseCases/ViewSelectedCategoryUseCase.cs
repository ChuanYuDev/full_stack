
using CoreBusiness;
using UseCases.DataStorePluginInterfaces;

namespace UseCases.CategoriesUseCases
{
    public class ViewSelectedCategoryUseCase : IViewSelectedCategoryUseCase
    {
        private readonly ICategoriesRepository categoriesRepository;

        public ViewSelectedCategoryUseCase(ICategoriesRepository categoriesRepository)
        {
            this.categoriesRepository = categoriesRepository;
        }

        public Category? Execute(int categoryId)
        {
            return categoriesRepository.GetCategoryById(categoryId);
        }
    }
}