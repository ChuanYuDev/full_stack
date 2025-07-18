using Microsoft.AspNetCore.Mvc;
using WebAppMVC.Models;
using WebAppMVC.ViewModels;

namespace WebAppMVC.Controllers
{
    public class ProductsController : Controller
    {
        public IActionResult Index()
        {
            var products = ProductsRepository.GetProducts(loadCategory: true);
            return View(products);
        }

        public IActionResult Edit(int? productId)
        {
            ViewBag.Action = "edit";

            var product = ProductsRepository.GetProductById(productId.HasValue ? productId.Value : 0);
            return View(product);
        }

        [HttpPost]
        public IActionResult Edit(Product product)
        {
            if (ModelState.IsValid)
            {
                ProductsRepository.UpdateProduct(product.ProductId, product);

                return RedirectToAction(nameof(Index));
            }

            ViewBag.Action = "edit";

            return View(product);
        }

        public IActionResult Add()
        {
            ViewBag.Action = "add";

            // In order to create dropdown categories selection
            var productViewModel = new ProductViewModel
            {
                Categories = CategoriesRepository.GetCategories()
            };

            return View(productViewModel);
        }

        [HttpPost]
        public IActionResult Add(ProductViewModel productViewModel)
        {
            if (ModelState.IsValid)
            {
                ProductsRepository.AddProduct(productViewModel.Product);
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Action = "Add";
            productViewModel.Categories = CategoriesRepository.GetCategories();

            return View(productViewModel);
        }

        public IActionResult Delete(int? productId)
        {
            ProductsRepository.DeleteProduct(productId.HasValue ? productId.Value : 0);

            return RedirectToAction(nameof(Index));
        }
    }
}