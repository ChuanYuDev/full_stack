var builder = WebApplication.CreateBuilder(args);

// Dependency injection
// Extension service helps to inject all of the services required by `MapControllerRoute`
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Add static file middleware
// Enable to access the static file in `wwwroot` folder
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
