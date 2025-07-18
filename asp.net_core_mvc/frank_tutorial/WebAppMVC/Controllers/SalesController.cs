using Microsoft.AspNetCore.Mvc;
using WebAppMVC.ViewModels;
using WebAppMVC.Models;

namespace WebAppMVC.Controllers
{
    public class SalesController : Controller
    {
        public IActionResult Index()
        {
            var salesViewModel = new SalesViewModel
            {
                Categories = CategoriesRepository.GetCategories()
            };

            return View(salesViewModel);
        }
    }
}