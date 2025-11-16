using Microsoft.EntityFrameworkCore;
using Sorveteria.Domain.Entities;
using Sorveteria.Infrastructure.Data.Configurations;

namespace Sorveteria.Infrastructure.Data
{
    public class SorveteriaDbContext : DbContext
    {
        public SorveteriaDbContext(DbContextOptions<SorveteriaDbContext> options) : base(options)
        {
        }

        public DbSet<Categoria> Categorias { get; set; }
        public DbSet<Sorvete> Sorvetes { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfiguration(new CategoriaConfiguration());
            modelBuilder.ApplyConfiguration(new SorveteConfiguration());
        }
    }
}