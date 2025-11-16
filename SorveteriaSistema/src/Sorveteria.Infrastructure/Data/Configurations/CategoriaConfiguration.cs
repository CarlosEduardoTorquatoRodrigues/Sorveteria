using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sorveteria.Domain.Entities;

namespace Sorveteria.Infrastructure.Data.Configurations
{
    public class CategoriaConfiguration : IEntityTypeConfiguration<Categoria>
    {
        public void Configure(EntityTypeBuilder<Categoria> builder)
        {
            builder.ToTable("Categorias");

            builder.HasKey(c => c.Id);

            builder.Property(c => c.Nome)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(c => c.Descricao)
                .IsRequired()
                .HasMaxLength(500);

            builder.Property(c => c.Ativo)
                .IsRequired();

            builder.Property(c => c.DataCriacao)
                .IsRequired();

            // Relacionamento 1:N
            builder.HasMany(c => c.Sorvetes)
                .WithOne(s => s.Categoria)
                .HasForeignKey(s => s.CategoriaId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}