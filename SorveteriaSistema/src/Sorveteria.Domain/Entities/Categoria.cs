namespace Sorveteria.Domain.Entities
{
    public class Categoria
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public bool Ativo { get; set; }
        public DateTime DataCriacao { get; set; }
        
   
        public virtual ICollection<Sorvete> Sorvetes { get; set; } = new List<Sorvete>();
    }
}