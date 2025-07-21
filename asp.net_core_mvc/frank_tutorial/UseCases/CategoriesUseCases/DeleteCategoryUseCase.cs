using UseCases.DataStorePluginInterfaces;

namespace UseCases.CategoriesUseCases
{
    public class DeleteCategoryUseCase : IDeleteCategoryUseCase
    {
        private readonly ICategoriesRepository categoriesRepository;
        public DeleteCategoryUseCase(ICategoriesRepository categoriesRepository)
        {
            this.categoriesRepository = categoriesRepository;
        }
        public void Execute(int categoryId)
        {
            categoriesRepository.DeleteCategory(categoryId);
        }
    }
}