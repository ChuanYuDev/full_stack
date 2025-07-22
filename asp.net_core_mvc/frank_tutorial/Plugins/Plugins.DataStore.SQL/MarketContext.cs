using CoreBusiness;
using Microsoft.EntityFrameworkCore;

namespace Plugins.DataStore.SQL
{
    // Represent in-memory representation of the database

    // DbContext is from EntityFrameworkCore Nuget package
    public class MarketContext : DbContext
    {
        // DbSet corresponds to a database table with generic parameter
        //
        // DbSet<Category>
        //      We are having a table and the definition of the table is the same as the definition of the category class
        //
        // Usually we use plural to represent the database inside the context
        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Transaction> transactions { get; set; }

        // Define relationship between the tables
        //
        // OnModelCreating
        //      When the in-memory representation of the database (Model) is being constructed, something happens
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // One category has many products
            // One product belongs to one category
            //
            // Fluent API
            //      Often, the type returned from the method call is the same instance as the one on which the method is called
            modelBuilder.Entity<Category>()
                .HasMany(c => c.Products)
                .WithOne(p => p.Category)
                .HasForeignKey(p => p.CategoryId);

            // Transaction table is an independent standalone table

            // Seeding some data
            modelBuilder.Entity<Category>().HasData(
                new Category { CategoryId = 1, Name = "Beverage", Description = "BeverageDesc" },
                new Category { CategoryId = 2, Name = "Bakery", Description = "BakeryDesc" },
                new Category { CategoryId = 3, Name = "Meat", Description = "MeatDesc" }
            );

            modelBuilder.Entity<Product>().HasData(
                new Product { ProductId = 1, CategoryId = 1, Name = "Iced Tea", Quantity = 100, Price = 1.99 },
                new Product { ProductId = 2, CategoryId = 1, Name = "Canada Dry", Quantity = 200, Price = 1.99 },
                new Product { ProductId = 3, CategoryId = 2, Name = "Whole Wheat Bread", Quantity = 300, Price = 1.50 },
                new Product { ProductId = 4, CategoryId = 2, Name = "White Bread", Quantity = 300, Price = 1.50 }
            );
        }
    }
}

