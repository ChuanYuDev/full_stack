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
        public string Index()
        {
            return "Hello world from action method Index.";
        }

        public string Error()
        {
            return "I have an error here.";
        }
    }
}
