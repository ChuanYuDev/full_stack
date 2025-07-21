using CoreBusiness;
using UseCases.DataStorePluginInterfaces;

namespace UseCases.CategoriesUseCases
{
    public class ViewCategoriesUseCase : IViewCategoriesUseCase
    {
        // readonly
        //      A readonly field can't be assigned after the constructor exits
        //      This rule has different implications for value types and reference types:
        private readonly ICategoriesRepository categoriesRepository;

        // Contructor based dependency injection
        //      Inject a future plugin based on an interface
        //
        // Use case driven
        //      I don't have anything else other than the model class itself
        //      Only focus on the intent of the business
        //
        // With an interface, we are able to implement our logic without have a concrete implementation of the interface
        //      That is the whole point of plugin based clean architecture
        public ViewCategoriesUseCase(ICategoriesRepository categoriesRepository)
        {
            this.categoriesRepository = categoriesRepository;
        }

        // Have only one public method called Execute in each use case class
        //      To represent the action of executing the use case
        //      It's very beneficial to have this one class that only servers one purpose
        //      That follows the single responsility principle
        //
        //      In real project, the execute method can contain a lot of logic
        public IEnumerable<Category> Execute()
        {
            return categoriesRepository.GetCategories();
        }
    }
}