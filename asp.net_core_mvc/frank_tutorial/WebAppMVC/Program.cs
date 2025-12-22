using Plugins.DataStore.InMemory;
using Plugins.DataStore.SQL;
using UseCases.DataStorePluginInterfaces;
using UseCases.CategoriesUseCases;
using UseCases.ProductsUseCases;
using UseCases.TransactionsUseCases;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using WebAppMVC.Data;

var builder = WebApplication.CreateBuilder(args);

// Use dependency injection to provide all of the services that entity framework core actually needs
builder.Services.AddDbContext<MarketContext>(options =>
{
    // UseSqlServer
    //      Configure the EF core to use SQL server
    //
    // Provide connection string
    //      Pull from appsettings.Development.json because we're using development environment
    //      It will be fed to the MarketContext in Plugins.DataStore.SQL
    options.UseSqlServer(builder.Configuration.GetConnectionString("MarketManagement"));
});

builder.Services.AddDbContext<AccountContext>(options =>
{
    // Can choose different database for identity
    //      This course uses the same database
    options.UseSqlServer(builder.Configuration.GetConnectionString("MarketManagement"));
});

// Scaffold for identity adds automatically
builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true).AddEntityFrameworkStores<AccountContext>();

// Once ASP.NET core finds the user hasn't logged in and tries to access one of `Identity/Pages`, it's going to redirect the user to the login page
//      The login page is Razor pages
//      Our application is only set up for accessing ASP.NET core MVC
//      In order to run the application, we need to add support for Razor pages
builder.Services.AddRazorPages();

// Dependency injection
//      Extension service helps to inject all of the services required by `MapControllerRoute`
builder.Services.AddControllersWithViews();

// Configure authorization
builder.Services.AddAuthorization(options =>
{
    // Policy-based authorization
    //      Use policy to limit access to our pages
    //
    // AddPolicy
    //      Policy name: "Inventory"
    //      Claim is a key value pair that carries the user information
    options.AddPolicy("Inventory", p => p.RequireClaim("Position", "Inventory"));
    options.AddPolicy("Cashier", p => p.RequireClaim("Position", "Cashier"));
});

// Implement a logic for dependency injection
//      Sometimes, when QA wants to test, QA will go through hundreds of thousands of test cases
//      Talking to actual database is going to be really slow
//      It's possible that QA wants to use in-memory repositories
if (builder.Environment.IsEnvironment("QA"))
{
    // AddSingleton
    //      For ICategoryRepository, there's only going to be one instance in the entire ASP.NET core application
    //      Instance is created once
    //      Every time you need to use this instance, it's always going to return back the same instance
    builder.Services.AddSingleton<ICategoriesRepository, CategoriesInMemoryRepository>();
    builder.Services.AddSingleton<IProductsRepository, ProductsInMemoryRepository>();
    builder.Services.AddSingleton<ITransactionsRepository, TransactionsInMemoryRepository>();
}
else
{
    // As soon as we switch the concrete implementation to its SQL server counterpoarts
    //      We will be able to talk to the database
    //
    // Entity Framework Core will control the life span itself
    //      We will use AddTransient instead
    builder.Services.AddTransient<ICategoriesRepository, CategoriesSQLRepository>();
    builder.Services.AddTransient<IProductsRepository, ProductsSQLRepository>();
    builder.Services.AddTransient<ITransactionsRepository, TransactionsSQLRepository>();
}

// When MVC create an instance of ViewCategoriesUseCase
//      It raises an exception that it is unable to resolve service for type ICategoryRepository
//      Because we didn't register the mapping in the service collection
//
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

// For every web request that goes to this application, it's going to
//      Check the identity to know who the user is 
//      And then verify the user's permission with the authorization middleware
app.UseAuthentication();
app.UseAuthorization();

// Map Razor pages
app.MapRazorPages();
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
