using CoreBusiness;
using UseCases.DataStorePluginInterfaces;

namespace Plugins.DataStore.InMemory
{
    public class CategoriesInMemoryRepository : ICategoriesRepository
    {
        private List<Category> _categories = new List<Category>()
        {
            new Category { CategoryId = 1, Name = "Beverage", Description = "BeverageDesc" },
            new Category { CategoryId = 2, Name = "Bakery", Description = "BakeryDesc" },
            new Category { CategoryId = 3, Name = "Meat", Description = "MeatDesc" }
        };

        // Create
        public void AddCategory(Category category)
        {
            int maxId = 0;

            if (_categories.Count() > 0)
            {
                // =>: Lambda expression
                maxId = _categories.Max(x => x.CategoryId);
            }

            category.CategoryId = maxId + 1;
            _categories.Add(category);
        }

        // Read
        // Expression Body statements, introduced with C# 6, equvalent to
        // public static List<Category> GetCategories()
        // {
        //     return _categories;
        // }
        public IEnumerable<Category> GetCategories() => _categories;

        public Category? GetCategoryById(int categoryId)
        {
            var category = _categories.FirstOrDefault(x => x.CategoryId == categoryId);

            if (category != null)
            {
                // Mimic actual database
                //      Data is stored in the file that instance of data is never returned back to caller
                //      The caller always gets a copy
                //      Otherwise the caller may modify the data
                return new Category
                {
                    CategoryId = category.CategoryId,
                    Name = category.Name,
                    Description = category.Description,
                };
            }

            return null;
        }

        // Update
        public void UpdateCategory(int categoryId, Category category)
        {
            if (categoryId != category.CategoryId) return;

            // GetCategoryById return a copied instance
            //      Therefore the category object in the list will not be updated
            //      Use LINQ FirstOrDefault instead

            // var categoryToUpdate = GetCategoryById(categoryId);

            var categoryToUpdate = _categories.FirstOrDefault(x => x.CategoryId == categoryId);

            if (categoryToUpdate != null)
            {
                // If there are a lot of properties, can use automapper library to map the properties to properties
                categoryToUpdate.Name = category.Name;
                categoryToUpdate.Description = category.Description;
            }
        }

        // Delete
        public void DeleteCategory(int categoryId)
        {
            var category = _categories.FirstOrDefault(x => x.CategoryId == categoryId);

            if (category != null)
            {
                _categories.Remove(category);
            }
        }
    }
}