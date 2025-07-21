using Microsoft.AspNetCore.Mvc;
using CoreBusiness;
using UseCases.CategoriesUseCases;
using UseCases.ProductsUseCases;
using WebAppMVC.ViewModels;

namespace WebAppMVC.Controllers
{
    public class ProductsController : Controller
    {
        private readonly IAddProductUseCase addProductUseCase;
        private readonly IDeleteProductUseCase deleteProductUseCase;
        private readonly IEditProductUseCase editProductUseCase;
        private readonly IViewCategoriesUseCase viewCategoriesUseCase;
        private readonly IViewProductsUseCase viewProductsUseCase;
        private readonly IViewSelectedProductUseCase viewSelectedProductUseCase;

        public ProductsController(
            IAddProductUseCase addProductUseCase,
            IDeleteProductUseCase deleteProductUseCase,
            IEditProductUseCase editProductUseCase,
            IViewCategoriesUseCase viewCategoriesUseCase,
            IViewProductsUseCase viewProductsUseCase,
            IViewSelectedProductUseCase viewSelectedProductUseCase
        )
        {
            this.addProductUseCase = addProductUseCase;
            this.deleteProductUseCase = deleteProductUseCase;
            this.editProductUseCase = editProductUseCase;
            this.viewCategoriesUseCase = viewCategoriesUseCase;
            this.viewProductsUseCase = viewProductsUseCase;
            this.viewSelectedProductUseCase = viewSelectedProductUseCase;
        }

        public IActionResult Index()
        {
            // var products = ProductsRepository.GetProducts(loadCategory: true);
            var products = viewProductsUseCase.Execute(loadCategory: true);

            return View(products);
        }

        public IActionResult Edit(int? productId)
        {
            ViewBag.Action = "edit";

            var productViewModel = new ProductViewModel
            {
                // Categories = CategoriesRepository.GetCategories(),
                // Product = ProductsRepository.GetProductById(productId.HasValue ? productId.Value : 0) ?? new Product()

                Categories = viewCategoriesUseCase.Execute(),
                Product = viewSelectedProductUseCase.Execute(productId.HasValue ? productId.Value : 0) ?? new Product()
            };

            return View(productViewModel);
        }

        [HttpPost]
        public IActionResult Edit(ProductViewModel productViewModel)
        {
            if (ModelState.IsValid)
            {
                // ProductsRepository.UpdateProduct(productViewModel.Product.ProductId, productViewModel.Product);
                editProductUseCase.Execute(productViewModel.Product.ProductId, productViewModel.Product);

                return RedirectToAction(nameof(Index));
            }

            ViewBag.Action = "edit";

            // We need to repopulate categories
            //      Because the Edit form didn't send the categories back to the controller
            //      When ModelState is not valid, the Category dropdown menu will be empty except for Please Select option

            // productViewModel.Categories = CategoriesRepository.GetCategories();
            productViewModel.Categories = viewCategoriesUseCase.Execute();

            return View(productViewModel);
        }

        public IActionResult Add()
        {
            ViewBag.Action = "add";

            // In order to create dropdown categories selection
            var productViewModel = new ProductViewModel
            {
                // Categories = CategoriesRepository.GetCategories()
                Categories = viewCategoriesUseCase.Execute()
            };

            return View(productViewModel);
        }

        [HttpPost]
        public IActionResult Add(ProductViewModel productViewModel)
        {
            if (ModelState.IsValid)
            {
                // ProductsRepository.AddProduct(productViewModel.Product);
                addProductUseCase.Execute(productViewModel.Product);
                return RedirectToAction(nameof(Index));
            }

            ViewBag.Action = "Add";

            // productViewModel.Categories = CategoriesRepository.GetCategories();
            productViewModel.Categories = viewCategoriesUseCase.Execute();

            return View(productViewModel);
        }

        public IActionResult Delete(int? productId)
        {
            // ProductsRepository.DeleteProduct(productId.HasValue ? productId.Value : 0);
            deleteProductUseCase.Execute(productId.HasValue ? productId.Value : 0);

            return RedirectToAction(nameof(Index));
        }
    }
}