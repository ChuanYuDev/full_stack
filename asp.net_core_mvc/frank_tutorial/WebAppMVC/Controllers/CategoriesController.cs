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
            return View();
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
        public IActionResult Edit(int? id)
        {
            // // When type conversion failed, id will use default value
            // //      int? default value is null, id.HasValue is false
            // if (id.HasValue)
            //     // Initialize with object parameterless constructor

            //     // public class ContentResult : ActionResult, IStatusCodeActionResult
            //     return new ContentResult { Content = id.ToString() };
            // else
            //     return new ContentResult { Content = "null content" };

            var category = new Category { CategoryId = id.HasValue ? id.Value : 0 };

            // One of 4 View signitures
            //      public virtual ViewResult View(object? model)
            //      We can pass model to the View and display model using
            //      <h3>Category: @Model.CategoryId</h3> in the view (Edit.cshtml)
            return View(category);
        }
    }
}
