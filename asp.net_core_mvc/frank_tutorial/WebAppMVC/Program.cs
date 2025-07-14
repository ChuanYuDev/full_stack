var builder = WebApplication.CreateBuilder(args);

// Dependency injection
// extension service helps to inject all of the services required by `MapControllerRoute`
builder.Services.AddControllersWithViews();

var app = builder.Build();

app.UseRouting();

// map `pattern`
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}"
);

app.Run();
