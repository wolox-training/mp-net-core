using Microsoft.EntityFrameworkCore;
using training_net.Models;

namespace training_net.Repositories.Database
{
  public class DataBaseContext : DbContext
    {
        public DataBaseContext(DbContextOptions<DataBaseContext> options) : base(options) {}

        public DbSet<Movie> Movies { get; set; }
        
        protected override void OnModelCreating(ModelBuilder modelBuilder) => base.OnModelCreating(modelBuilder);
    }   
}
