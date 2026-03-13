using CoreBusiness.DTOs;
using Microsoft.EntityFrameworkCore;
using MoviesAPI.Utilities;
using Plugins.DataStore.InMemory;
using Plugins.DataStore.SQL;
using Plugins.FileStorage;
using UseCases.DataStoreInterfaces;
using UseCases.FileStorageInterfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddOutputCache(options =>
{
    options.DefaultExpirationTimeSpan = TimeSpan.FromSeconds(60);
});

var allowedOrigins = builder.Configuration.GetValue<string>("AllowedOrigins")?.Split(",") ?? [];

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.WithOrigins(allowedOrigins).AllowAnyMethod().AllowAnyHeader().WithExposedHeaders("total-records-count");
    });
});

if (builder.Environment.IsEnvironment("QA"))
{
    builder.Services.AddSingleton<IGenresRepository, GenresInMemoryRepository>();
}
else
{
    var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Default connection string not found.");
    builder.Services.AddDbContext<ApplicationDbContext>(options =>
    {
        options.UseSqlServer(connectionString);
    });

    builder.Services.AddTransient<IRepository<GenreCreationDto, GenreDto>, GenresSqlRepository>();
    builder.Services.AddTransient<IRepository<ActorCreationDto, ActorDto>, ActorsSqlRepository>();
}

var autoMapperLicenseKey = builder.Configuration.GetValue<string>("AutoMapperLicenseKey");

if (autoMapperLicenseKey is null)
{
    builder.Services.AddAutoMapper(cfg => {}, typeof(AutoMapperProfiles));
}
else
{
    builder.Services.AddAutoMapper(cfg =>
    {
        cfg.LicenseKey = autoMapperLicenseKey;
    }, typeof(AutoMapperProfiles));
}

builder.Services.AddTransient<IFileStorage, AzureFileStorage>();

builder.Services.AddHttpContextAccessor();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    // app.UseSwagger();
    // app.UseSwaggerUI();
}

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseCors();

app.UseOutputCache();

app.UseAuthorization();

app.MapControllers();

app.Run();