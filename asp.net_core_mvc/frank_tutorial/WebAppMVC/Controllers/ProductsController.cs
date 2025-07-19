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

            var productViewModel = new ProductViewModel
            {
                Categories = CategoriesRepository.GetCategories(),
                Product = ProductsRepository.GetProductById(productId.HasValue ? productId.Value : 0) ?? new Product()
            };

            return View(productViewModel);
        }

        [HttpPost]
        public IActionResult Edit(ProductViewModel productViewModel)
        {
            if (ModelState.IsValid)
            {
                ProductsRepository.UpdateProduct(productViewModel.Product.ProductId, productViewModel.Product);

                return RedirectToAction(nameof(Index));
            }

            ViewBag.Action = "edit";

            // We need to repopulate categories
            //      Because the Edit form didn't send the categories back to the controller
            //      When ModelState is not valid, the Category dropdown menu will be empty except for Please Select option
            productViewModel.Categories = CategoriesRepository.GetCategories();

            return View(productViewModel);
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

        // Partial
        //      Returns a partial view result containing the product list to help us render the partial view on the page
        public IActionResult ProductsByCategoryPartial(int categoryId)
        {
            // Test-driven development (TDD)
            //      Write a test case first and make it fail
            //      Make a test case work later
            var products = ProductsRepository.GetProductsByCategoryId(categoryId);

            // _Products: partial view name
            return PartialView("_Products", products);
        }
    }
}