using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Sorveteria.Domain.Entities;

namespace Sorveteria.Infrastructure.Data.Configurations
{
    public class SorveteConfiguration : IEntityTypeConfiguration<Sorvete>
    {
        public void Configure(EntityTypeBuilder<Sorvete> builder)
        {
            builder.ToTable("Sorvetes");

            builder.HasKey(s => s.Id);

            builder.Property(s => s.Nome)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(s => s.Sabor)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(s => s.Preco)
                .IsRequired()
                .HasColumnType("decimal(10,2)");

            builder.Property(s => s.QuantidadeEstoque)
                .IsRequired();

            builder.Property(s => s.Ingredientes)
                .IsRequired()
                .HasMaxLength(1000);

            builder.Property(s => s.Disponivel)
                .IsRequired();

            builder.Property(s => s.DataCriacao)
                .IsRequired();

            // Chave estrangeira explícita
            builder.Property(s => s.CategoriaId)
                .IsRequired();

            // Relacionamento N:1
            builder.HasOne(s => s.Categoria)
                .WithMany(c => c.Sorvetes)
                .HasForeignKey(s => s.CategoriaId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}