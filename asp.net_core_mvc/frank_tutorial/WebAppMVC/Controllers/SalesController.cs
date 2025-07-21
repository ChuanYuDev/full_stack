using Microsoft.AspNetCore.Mvc;
using UseCases.CategoriesUseCases;
using UseCases.ProductsUseCases;
using WebAppMVC.ViewModels;

namespace WebAppMVC.Controllers
{
    public class SalesController : Controller
    {
        private readonly ISellProductUseCase sellProductUseCase;
        private readonly IViewCategoriesUseCase viewCategoriesUseCase;
        private readonly IViewProductsInCategoryUseCase viewProductsInCategoryUseCase;
        private readonly IViewSelectedProductUseCase viewSelectedProductUseCase;

        public SalesController(
            ISellProductUseCase sellProductUseCase,
            IViewCategoriesUseCase viewCategoriesUseCase,
            IViewProductsInCategoryUseCase viewProductsInCategoryUseCase,
            IViewSelectedProductUseCase viewSelectedProductUseCase
        )
        {
            this.sellProductUseCase = sellProductUseCase;
            this.viewCategoriesUseCase = viewCategoriesUseCase;
            this.viewProductsInCategoryUseCase = viewProductsInCategoryUseCase;
            this.viewSelectedProductUseCase = viewSelectedProductUseCase;
        }

        public IActionResult Index()
        {
            var salesViewModel = new SalesViewModel
            {
                // Categories = CategoriesRepository.GetCategories()
                Categories = viewCategoriesUseCase.Execute()
            };

            return View(salesViewModel);
        }

        // Partial
        //      Returns a partial view result containing the product list to help us render the partial view on the page
        public IActionResult ProductsByCategoryPartial(int categoryId)
        {
            // Test-driven development (TDD)
            //      Write a test case first and make it fail
            //      Make a test case work later

            // var products = ProductsRepository.GetProductsByCategoryId(categoryId);
            var products = viewProductsInCategoryUseCase.Execute(categoryId);

            // _Products: partial view name
            return PartialView("_Products", products);
        }

        public IActionResult SellProductPartial(int productId)
        {
            // var product = ProductsRepository.GetProductById(productId);
            var product = viewSelectedProductUseCase.Execute(productId);

            return PartialView("_SellProduct", product);
        }

        public IActionResult Sell(SalesViewModel salesViewModel)
        {
            // var product = ProductsRepository.GetProductById(salesViewModel.SelectedProductId);

            if (ModelState.IsValid)
            {
                // Sell the product    
                // if (product != null)
                // {
                //     TransactionsRepository.Add(
                //         productId: salesViewModel.SelectedProductId,
                //         productName: product.Name,
                //         price: product.Price.HasValue ? product.Price.Value : 0,
                //         beforeQty: product.Quantity.HasValue ? product.Quantity.Value : 0,
                //         soldQty: salesViewModel.QuantityToSell,
                //         cashierName: "Cashier1"
                //     );

                //     product.Quantity -= salesViewModel.QuantityToSell;
                //     ProductsRepository.UpdateProduct(salesViewModel.SelectedProductId, product);
                // }

                sellProductUseCase.Execute("Cashier1", salesViewModel.SelectedProductId, salesViewModel.QuantityToSell);

            }

            var product = viewSelectedProductUseCase.Execute(salesViewModel.SelectedProductId);

            // Get SelectedCategoryId by SelectProductId
            salesViewModel.SelectedCategoryId = (product?.CategoryId) ?? 0;

            // salesViewModel.Categories = CategoriesRepository.GetCategories();
            salesViewModel.Categories = viewCategoriesUseCase.Execute();

            return View("Index", salesViewModel);
        }
    }
}