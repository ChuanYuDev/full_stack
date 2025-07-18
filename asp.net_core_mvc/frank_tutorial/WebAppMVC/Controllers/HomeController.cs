using Microsoft.AspNetCore.Mvc;

namespace WebAppMVC.Controllers
{
    // Home: name of the controller
    public class HomeController : Controller
    {
        // Public method in the controller is called action method
        // Every action method is used to handle request 
        //
        // Index: Name of the action method
        public IActionResult Index()
        {
            // Return Index.cshtml view by convention
            //      Or we can specify view name to Index.cshtml 
            //      return View("Index");

            // IActionResult
            //      All possible return types of action method
            //      public class ViewResult : ActionResult, IStatusCodeActionResult
            //      public abstract class ActionResult : IActionResult
            return View();
        }
    }
}
