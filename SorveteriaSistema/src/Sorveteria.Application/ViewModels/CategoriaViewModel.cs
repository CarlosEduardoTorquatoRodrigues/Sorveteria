using System.ComponentModel.DataAnnotations;

namespace Sorveteria.Application.ViewModels
{
    public class CategoriaViewModel
    {
        public int Id { get; set; }
        
        [Required(ErrorMessage = "O nome é obrigatório")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "O nome deve ter entre 3 e 100 caracteres")]
        public string Nome { get; set; }
        
        [Required(ErrorMessage = "A descrição é obrigatória")]
        [StringLength(500, ErrorMessage = "A descrição deve ter no máximo 500 caracteres")]
        public string Descricao { get; set; }
        
        public bool Ativo { get; set; }
        public DateTime DataCriacao { get; set; }
        
        public int QuantidadeSorvetes { get; set; }
    }
}