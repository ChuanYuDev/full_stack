using CoreBusiness;
using Microsoft.EntityFrameworkCore;

namespace Plugins.DataStore.SQL;

public class ApplicationDbContext: DbContext
{
    public ApplicationDbContext(DbContextOptions options): base(options)
    {
    }
    
    public DbSet<Genre> Genres { get; set; }
}