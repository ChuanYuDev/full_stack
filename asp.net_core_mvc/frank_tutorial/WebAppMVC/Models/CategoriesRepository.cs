namespace WebAppMVC.Models
{
    // In-memory static repository

    // static class: can't be instantiated
    public static class CategoriesRepository
    {
        private static List<Category> _categories = new List<Category>()
        {
            new Category { CategoryId = 1, Name = "Beverage", Description = "BeverageDesc" },
            new Category { CategoryId = 2, Name = "Bakery", Description = "BakeryDesc" },
            new Category { CategoryId = 3, Name = "Meat", Description = "MeatDesc" }
        };

        // Create
        public static void AddCategory(Category category)
        {
            int maxId;

            if (_categories.Count() == 0)
            {
                maxId = 0;
            }
            else
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
        public static List<Category> GetCategories() => _categories;

        public static Category? GetCategoryById(int categoryId)
        {
            // FirstOrDefault()
            //      Belongs to LINQ (language integrated query)
            //
            //      This method returns the first element of a sequence that satisfies a specified condition
            //      Or a default value if no such element is found
            //      The default value for reference types is null
            //      And for value types, it’s the default value of the type (e.g., 0 for int)
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
        public static void UpdateCategory(int categoryId, Category category)
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
        public static void DeleteCategory(int categoryId)
        {
            var category = _categories.FirstOrDefault(x => x.CategoryId == categoryId);

            if (category != null)
            {
                _categories.Remove(category);
            }
        }
    }
}