using Microsoft.AspNetCore.Mvc;
using WebAppMVC.Models;

namespace WebAppMVC.Controllers
{
    // Categories:
    //      Name of the controller
    //      Prefer plural form of English word
    public class CategoriesController : Controller
    {
        public IActionResult Index()
        {
            // Load static repository data
            var categories = CategoriesRepository.GetCategories();
            return View(categories);
        }

        // When mapping /categories/edit/abc
        //      "abc" maps to integer failed
        //      No exception is throwed, use default value
        //      0: default value of int
        //      null: defalut value of int? which is nullable type
        //
        // Another way to pass parameter
        //      /categories/edit?id=555
        //      Make sure id=555 name matches with action method parameter name `id`
        //      Then value will be automatically assigned to `Edit` action method `id` paramater
        //
        // The way to transfer data to the action method using `id` is called model binding
        // 
        // If we don't specify what the action method is, it corresponds to the HttpGet action method
        //      HttpGet: default of action method
        // 
        // We can specify where the data come from
        //      There are five data source
        //      FromRoute attribute: the data come from the route
        //          Syntax: public IActionResult Edit([FromRoute] int? id)
        //          Then /categories/edit?id=555, passing the data from query string no long works
        //
        //      If we don't specify the data source, asp.net core is going to go through all of five different places to locate the data  
        //          If it can't locate the data, it will report error or provide default value for the parameter
        //
        // Model binding can bind complex data structure such as Category
        //      public IActionResult Edit(Category? category)
        //      If it's the case
        //          /categories/edit/1 will fail to bind the data
        //
        //      However, if we use
        //          /categories/edit?categoryId=1&Name=testBeverage
        //          We can use query string to initialize category object
        public IActionResult Edit(int? id)
        {
            // // When type conversion failed, id will use default value
            // //      int? default value is null, id.HasValue is false
            // if (id.HasValue)

            //     // Initialize ContentResult class with object parameterless constructor
            //     // public class ContentResult : ActionResult, IStatusCodeActionResult
            //     return new ContentResult { Content = id.ToString() };
            // else
            //     return new ContentResult { Content = "null content" };

            // var category = new Category { CategoryId = id.HasValue ? id.Value : 0 };

            // Load the category from the data store
            var category = CategoriesRepository.GetCategoryById(id.HasValue ? id.Value : 0);

            // One of 4 View signitures
            //      public virtual ViewResult View(object? model)
            //      We can pass model to the View and display model using
            //      <h3>Category: @Model.CategoryId</h3> in the view (Edit.cshtml)
            return View(category);
        }

        // Http form sends post request, use another HttpPost attribute decorated Edit action method to handle it
        [HttpPost]
        public IActionResult Edit(Category category)
        {
            // Only when ModelState indicates valid, the category will be updated
            if (ModelState.IsValid)
            {
                CategoriesRepository.UpdateCategory(category.CategoryId, category);

                // Redirect user to the categories/index page
                //      If don't specify the Controller which indicates redirecting to the same controller
                //      Redirect to same controller and action method which name is Index
                return RedirectToAction(nameof(Index));
            }

            // If ModelState is invalid, we need user to see the same category page 
            // On that category page, we need user to see error message so that the user has the opportunity to correct his or her mistake 
            return View(category);
        }

        public IActionResult Add()
        {
            var category = new Category();

            return View(category);
        }

        [HttpPost]
        public IActionResult Add(Category category)
        {
            if (ModelState.IsValid)
            {
                CategoriesRepository.AddCategory(category);
                return RedirectToAction(nameof(Index));
            }

            return View(category);
        }
    }
}
