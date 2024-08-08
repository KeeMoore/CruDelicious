using Microsoft.EntityFrameworkCore;
using CruDelicious.Models;

namespace CruDelicious.Context
{
    public class ApplicationContext : DbContext
    {
        public DbSet<Dish> Dishes { get; set; } = null!;

        public ApplicationContext(DbContextOptions options) : base(options) { }
    }
}
