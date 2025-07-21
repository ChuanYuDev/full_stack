using CoreBusiness;
using UseCases.DataStorePluginInterfaces;

namespace UseCases.CategoriesUseCases
{
    public class EditCategoryUseCase : IEditCategoryUseCase
    {
        private readonly ICategoriesRepository categoriesRepository;

        public EditCategoryUseCase(ICategoriesRepository categoriesRepository)
        {
            this.categoriesRepository = categoriesRepository;
        }
        public void Execute(int categoryId, Category category)
        {
            categoriesRepository.UpdateCategory(categoryId, category);
        }
    }
}