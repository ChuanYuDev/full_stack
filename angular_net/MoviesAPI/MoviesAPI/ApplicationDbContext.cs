using Microsoft.EntityFrameworkCore;

namespace MoviesAPI;

public class ApplicationDbContext: DbContext
{
    public ApplicationDbContext(DbContextOptions options): base(options)
    {
        
    }
}