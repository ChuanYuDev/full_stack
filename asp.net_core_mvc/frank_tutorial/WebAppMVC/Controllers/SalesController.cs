using Microsoft.AspNetCore.Mvc;

namespace WebAppMVC.Controllers
{
    public class SalesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}