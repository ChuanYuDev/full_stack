using Microsoft.AspNetCore.Mvc;
using WebAppMVC.Models;

namespace WebAppMVC.Controllers
{
    public class ProductsController : Controller
    {
        public IActionResult Index()
        {
            var products = ProductsRepository.GetProducts();
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

            return View();
        }

        [HttpPost]
        public IActionResult Add(Product product)
        {
            if (ModelState.IsValid)
            {
                ProductsRepository.AddProduct(product);
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Action = "Add";

            return View(product);
        }

        public IActionResult Delete(int? productId)
        {
            ProductsRepository.DeleteProduct(productId.HasValue ? productId.Value : 0);

            return RedirectToAction(nameof(Index));
        }
    }
}