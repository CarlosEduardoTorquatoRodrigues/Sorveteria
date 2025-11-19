using System.ComponentModel.DataAnnotations;

namespace Sorveteria.Application.ViewModels
{
    public class CategoriaViewModel
    {
        public int Id { get; set; }
        
        [Required(ErrorMessage = "O nome é obrigatório")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "O nome deve ter entre 3 e 100 caracteres")]
        [Display(Name = "Nome da Categoria")]
        public string Nome { get; set; } = string.Empty;
        
        [Required(ErrorMessage = "A descrição é obrigatória")]
        [StringLength(500, MinimumLength = 10, ErrorMessage = "A descrição deve ter entre 10 e 500 caracteres")]
        [Display(Name = "Descrição")]
        public string Descricao { get; set; } = string.Empty;
        
        [Display(Name = "Ativo")]
        public bool Ativo { get; set; }
        
        [Display(Name = "Data de Criação")]
        public DateTime DataCriacao { get; set; }
        
        [Display(Name = "Quantidade de Sorvetes")]
        public int QuantidadeSorvetes { get; set; }
    }
}