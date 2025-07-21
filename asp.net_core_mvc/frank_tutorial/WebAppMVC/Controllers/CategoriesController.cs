using Microsoft.AspNetCore.Mvc;
using UseCases.CategoriesUseCases;
using CoreBusiness;

namespace WebAppMVC.Controllers
{
    // Categories:
    //      Name of the controller
    //      Prefer plural form of English word
    public class CategoriesController : Controller
    {
        private readonly IAddCategoryUseCase addCategoryUseCase;
        private readonly IDeleteCategoryUseCase deleteCategoryUseCase;
        private readonly IEditCategoryUseCase editCategoryUseCase;
        private readonly IViewCategoriesUseCase viewCategoriesUseCase;
        private readonly IViewSelectedCategoryUseCase viewSelectedCategoryUseCse;

        // When MVC framework tries to instantiate the instance of CategoriesController class
        //      It's going to see that this controller constructor requires IViewCategoriesUseCase
        //      Then it's going to try to create an instance of the implementation of this interface
        //      Go to service collection and check whether there's anything registered against this interface
        //      Create the instance of ViewCategoriesUseCase
        //      Feed the instance into CategoriesController constructor
        //
        // How does MVC know where is the implementation of the interface
        //      That's when we need to actually register the use case
        //      We call it service class
        //
        public CategoriesController(
            IAddCategoryUseCase addCategoryUseCase,
            IDeleteCategoryUseCase deleteCategoryUseCase,
            IEditCategoryUseCase editCategoryUseCase,
            IViewCategoriesUseCase viewCategoriesUseCase,
            IViewSelectedCategoryUseCase viewSelectedCategoryUseCse
        )
        {
            this.addCategoryUseCase = addCategoryUseCase;
            this.deleteCategoryUseCase = deleteCategoryUseCase;
            this.editCategoryUseCase = editCategoryUseCase;
            this.viewCategoriesUseCase = viewCategoriesUseCase;
            this.viewSelectedCategoryUseCse = viewSelectedCategoryUseCse;
        }
        public IActionResult Index()
        {
            // Load static repository data
            // var categories = CategoriesRepository.GetCategories();

            // Use view categories use case instance instead
            var categories = viewCategoriesUseCase.Execute();


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
        //      Then value will be automatically assigned to `Edit` action method `id` parameter
        //
        // The way to transfer data to the action method using `id` is called model binding
        // 
        // HttpGet
        //      If we don't specify what attrobute the action method has, it has to the HttpGet attribute
        //      So by default, an action method without the attribute is the one that handles Http get
        // 
        // Model binding
        //      We can specify where the data come from
        //      There are five data source
        //          [FromQuery] - Gets values from the query string.
        //          [FromRoute] - Gets values from route data.
        //          [FromForm] - Gets values from posted form fields.
        //          [FromBody] - Gets values from the request body.
        //          [FromHeader] - Gets values from HTTP headers
        //      If we don't specify the data source, asp.net core is going to go through all of five different places to locate the data  
        //          If it can't locate the data, it will report error or provide default value for the parameter
        //
        //      FromRoute attribute: the data come from the route
        //          Syntax: public IActionResult Edit([FromRoute] int? id)
        //              Such as /categories/edit/1
        //          Then /categories/edit?id=555, passing the data from query string no long works
        //
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

            // We can provide information and pass the information to the partial view
            ViewBag.Action = "edit";

            // var category = new Category { CategoryId = id.HasValue ? id.Value : 0 };
            // Load the category from the data store
            // var category = CategoriesRepository.GetCategoryById(id.HasValue ? id.Value : 0);

            var category = viewSelectedCategoryUseCse.Execute(id.HasValue ? id.Value : 0);

            // One of 4 View signitures
            //      public virtual ViewResult View(object? model)
            //      We can pass model to the View and display model using
            //      <h3>Category: @Model.CategoryId</h3> in the view (Edit.cshtml)
            return View(category);
        }

        // Http form sends post request, use another HttpPost attribute decorated Edit action method to handle it

        // We can add [FromForm] attribute to specify category data from posted form fields
        //      [HttpPost]
        //      public IActionResult Edit([FromForm] Category category)
        [HttpPost]
        public IActionResult Edit(Category category)
        {
            // Only when ModelState indicates valid, the category will be updated
            if (ModelState.IsValid)
            {
                // CategoriesRepository.UpdateCategory(category.CategoryId, category);
                editCategoryUseCase.Execute(category.CategoryId, category);

                // Redirect user to the categories/index page
                //      If don't specify the Controller which indicates redirecting to the same controller
                //      Redirect to same controller and action method which name is Index
                return RedirectToAction(nameof(Index));
            }

            // Set ViewBag again
            //      Otherwise if we don't pass the validation, input the form again, the form will not be passed to Edit action method
            ViewBag.Action = "Edit";

            // If ModelState is invalid, we need user to see the same category page 
            // On that category page, we need user to see error message so that the user has the opportunity to correct his or her mistake 
            return View(category);
        }

        public IActionResult Add()
        {
            // Wrong
            //      Because even we don't pass category into the View, in the Razor view, we still have the @model Category
            //      The empty @Model and tag helper will render HTML label and leave input blank
            //      The model binding still works when we submit the form

            // var category = new Category();
            // return View(category);

            ViewBag.Action = "add";

            return View();
        }

        [HttpPost]
        public IActionResult Add(Category category)
        {
            if (ModelState.IsValid)
            {
                // CategoriesRepository.AddCategory(category);

                addCategoryUseCase.Execute(category);
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Action = "Add";

            return View(category);
        }

        // In Index.cshtml, tag helper generated URL is `/categories/delete?categoryid=1`
        //      So if we use FromRoute attribute
        //      public IActionResult Delete([FromRoute] int categoryId)
        //      Model binding failed
        //
        //      If we use FromQuery
        //      public IActionResult Delete([FromQuery] int categoryId)
        //      Model binding succeeded
        public IActionResult Delete(int categoryId)
        {
            // CategoriesRepository.DeleteCategory(categoryId);

            deleteCategoryUseCase.Execute(categoryId);

            return RedirectToAction(nameof(Index));
        }
    }
}
