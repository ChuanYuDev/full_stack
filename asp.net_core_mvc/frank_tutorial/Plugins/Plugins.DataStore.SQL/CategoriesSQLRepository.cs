using CoreBusiness;
using UseCases.DataStorePluginInterfaces;

namespace Plugins.DataStore.SQL
{
    public class CategoriesSQLRepository : ICategoriesRepository
    {
        private readonly MarketContext db;

        public CategoriesSQLRepository(MarketContext db)
        {
            this.db = db;
        }
        public void AddCategory(Category category)
        {
            db.Categories.Add(category);

            db.SaveChanges();
        }

        public void DeleteCategory(int categoryId)
        {
            // Category? Microsoft.EntityFrameworkCore.DbSet<Category>.Find (params object? (]? keyValues)
            //      Finds an entity with the given primary key values
            //
            //      If an entity with the given primary key values is being tracked by the context, then it is returned immediately without making a request to the database
            //      Otherwise, a query is made to the database for an entity with the given primary key values
            //          This entity, if found, is attached to the context and returned
            //          If no entity is found, then null is returned
            var category = db.Categories.Find(categoryId);

            if (category == null) return;

            db.Categories.Remove(category);
            db.SaveChanges();
        }

        public IEnumerable<Category> GetCategories()
        {
            return db.Categories.ToList();
        }

        public Category? GetCategoryById(int categoryId)
        {
            return db.Categories.Find(categoryId);
        }

        public void UpdateCategory(int categoryId, Category category)
        {
            if (categoryId != category.CategoryId) return;

            var categoryToUpdate = db.Categories.Find(categoryId);

            if (categoryToUpdate == null) return;

            categoryToUpdate.Name = category.Name;
            categoryToUpdate.Description = category.Description;
            db.SaveChanges();
        }
    }
}