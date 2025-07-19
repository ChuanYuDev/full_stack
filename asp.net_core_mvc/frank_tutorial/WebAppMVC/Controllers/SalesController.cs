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

        public IActionResult SellProductPartial(int productId)
        {
            var product = ProductsRepository.GetProductById(productId);

            return PartialView("_SellProduct", product);
        }

        public IActionResult Sell(SalesViewModel salesViewModel)
        {
            if (ModelState.IsValid)
            {
                
            }

            salesViewModel.Categories = CategoriesRepository.GetCategories();
            return View("Index", salesViewModel);
        }
    }
}