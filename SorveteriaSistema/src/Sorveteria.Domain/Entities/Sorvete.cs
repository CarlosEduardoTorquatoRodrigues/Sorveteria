namespace Sorveteria.Domain.Entities
{
    public class Sorvete
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Sabor { get; set; }
        public decimal Preco { get; set; }
        public int QuantidadeEstoque { get; set; }
        public string Ingredientes { get; set; }
        public bool Disponivel { get; set; }
        public DateTime DataCriacao { get; set; }
        
        // Chave estrangeira explícita
        public int CategoriaId { get; set; }
        public virtual Categoria Categoria { get; set; }
    }
}