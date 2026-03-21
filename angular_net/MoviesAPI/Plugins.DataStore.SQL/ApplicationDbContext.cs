using CoreBusiness;
using Microsoft.EntityFrameworkCore;

namespace Plugins.DataStore.SQL;

public class ApplicationDbContext: DbContext
{
    public ApplicationDbContext(DbContextOptions options): base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<MovieGenre>().HasKey(entity => new { entity.GenreId, entity.MovieId });
        modelBuilder.Entity<MovieTheater>().HasKey(entity => new { entity.TheaterId, entity.MovieId });
        modelBuilder.Entity<MovieActor>().HasKey(entity => new { entity.ActorId, entity.MovieId });
    }

    public DbSet<Genre> Genres { get; set; }
    public DbSet<Actor> Actors { get; set; }
    public DbSet<Theater> Theaters { get; set; }
    public DbSet<Movie> Movies { get; set; }
    public DbSet<MovieGenre> MoviesGenres { get; set; }
    public DbSet<MovieTheater> MoviesTheaters { get; set; }
    public DbSet<MovieActor> MoviesActors { get; set; }
}