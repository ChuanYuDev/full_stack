using Plugins.DataStore.InMemory;
using UseCases.DataStorePluginInterfaces;
using UseCases.CategoriesUseCases;
using UseCases.ProductsUseCases;
using UseCases.TransactionsUseCases;

var builder = WebApplication.CreateBuilder(args);

// Dependency injection
// Extension service helps to inject all of the services required by `MapControllerRoute`
builder.Services.AddControllersWithViews();

// AddSingleton
//      For ICategoryRepository, there's only going to be one instance in the entire ASP.NET core application
//      Instance is created once
//      Every time you need to use this instance, it's always going to return back the same instance

builder.Services.AddSingleton<ICategoriesRepository, CategoriesInMemoryRepository>();
builder.Services.AddSingleton<IProductsRepository, ProductsInMemoryRepository>();
builder.Services.AddSingleton<ITransactionsRepository, TransactionsInMemoryRepository>();

// Create a mapping between interface and concrete implementation
//      Register in services collection that I have an implementation of IViewCategoriesUseCase
//
// When MVC create an instance of ViewCategoriesUseCase
//      It raises an exception that it is unable to resolve service for type ICategoryRepository
//      Because we didn't register the mapping in the service collection
//
// ASP.NET core is not a stateful application
//      That means it doesn't maintain the state and store anything in the session if you don't specially do so
//      Every time, you ask for a page, a new instance of the corresponding controller class will be created
//
// AddTransient mapping
//      The lifespan of the created object is going to live as long as the controller

builder.Services.AddTransient<IAddCategoryUseCase, AddCategoryUseCase>();
builder.Services.AddTransient<IDeleteCategoryUseCase, DeleteCategoryUseCase>();
builder.Services.AddTransient<IEditCategoryUseCase, EditCategoryUseCase>();
builder.Services.AddTransient<IViewCategoriesUseCase, ViewCategoriesUseCase>();
builder.Services.AddTransient<IViewSelectedCategoryUseCase, ViewSelectedCategoryUseCase>();

builder.Services.AddTransient<IAddProductUseCase, AddProductUseCase>();
builder.Services.AddTransient<IDeleteProductUseCase, DeleteProductUseCase>();
builder.Services.AddTransient<IEditProductUseCase, EditProductUseCase>();
builder.Services.AddTransient<ISellProductUseCase, SellProductUseCase>();
builder.Services.AddTransient<IViewProductsInCategoryUseCase, ViewProductsInCategoryUseCase>();
builder.Services.AddTransient<IViewProductsUseCase, ViewProductsUseCase>();
builder.Services.AddTransient<IViewSelectedProductUseCase, ViewSelectedProductUseCase>();

builder.Services.AddTransient<IGetTodayTransactionsUseCase, GetTodayTransactionsUseCase>();
builder.Services.AddTransient<IRecordTransactionUseCase, RecordTransactionUseCase>();
builder.Services.AddTransient<ISearchTransactionsUseCase, SearchTransactionsUseCase>();

var app = builder.Build();

// Add static file middleware
//      Enable to access the static file in `wwwroot` folder
//      http://localhost:5398/lib/bootstrap/css/bootstrap.css
//      No `wwwroot` in the URL
app.UseStaticFiles();

// Add routing middleware
app.UseRouting();

// Mapping
app.MapControllerRoute(
    name: "default",

    // Pattern of the request ->
    //      First part to the controller
    //      Second part to the action method
    //      Third part (optional) to the parameter of the action method
    //
    //      Default value
    //          controller: Home
    //          action: Index
    pattern: "{controller=Home}/{action=Index}/{id?}"
);

app.Run();
