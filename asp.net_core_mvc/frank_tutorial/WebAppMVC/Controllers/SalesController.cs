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
            var product = ProductsRepository.GetProductById(salesViewModel.SelectedProductId);

            if (ModelState.IsValid)
            {
                // Sell the product    
                if (product != null)
                {
                    TransactionsRepository.Add(
                        productId: salesViewModel.SelectedProductId,
                        productName: product.Name,
                        price: product.Price.HasValue ? product.Price.Value : 0,
                        beforeQty: product.Quantity.HasValue ? product.Quantity.Value : 0,
                        soldQty: salesViewModel.QuantityToSell,
                        cashierName: "Cashier1"
                    );

                    product.Quantity -= salesViewModel.QuantityToSell;
                    ProductsRepository.UpdateProduct(salesViewModel.SelectedProductId, product);
                }
            }

            // Get SelectedCategoryId by SelectProductId
            salesViewModel.SelectedCategoryId = (product?.CategoryId) ?? 0;

            salesViewModel.Categories = CategoriesRepository.GetCategories();
            return View("Index", salesViewModel);
        }
    }
}