using CoreBusiness;
using UseCases.DataStorePluginInterfaces;

namespace UseCases.CategoriesUseCases
{
    public class AddCategoryUseCase : IAddCategoryUseCase
    {
        private readonly ICategoriesRepository categoriesRepository;

        public AddCategoryUseCase(ICategoriesRepository categoriesRepository)
        {
            this.categoriesRepository = categoriesRepository;
        }

        public void Execute(Category category)
        {
            categoriesRepository.AddCategory(category);
        }
    }
}