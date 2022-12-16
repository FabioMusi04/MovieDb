using MoviesDb.Models;
using Microsoft.EntityFrameworkCore;

namespace MoviesDb.Database
{
    public class AppDbContext:DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {

        }

        public DbSet<Movie> Movies { get; set; }
    }
}
