using System.ComponentModel.DataAnnotations;
using Sorveteria.Application.Validators;

namespace Sorveteria.Application.ViewModels
{
    public class SorveteViewModel
    {
        public int Id { get; set; }
        
        [Required(ErrorMessage = "O nome é obrigatório")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "O nome deve ter entre 3 e 100 caracteres")]
        public string Nome { get; set; }
        
        [Required(ErrorMessage = "O sabor é obrigatório")]
        [StringLength(100, ErrorMessage = "O sabor deve ter no máximo 100 caracteres")]
        public string Sabor { get; set; }
        
        [Required(ErrorMessage = "O preço é obrigatório")]
        [Range(0.01, 9999.99, ErrorMessage = "O preço deve estar entre R$ 0,01 e R$ 9.999,99")]
        [PrecoMinimoSorvete(5.00, ErrorMessage = "O preço mínimo para um sorvete é R$ 5,00")]
        public decimal Preco { get; set; }
        
        [Required(ErrorMessage = "A quantidade em estoque é obrigatória")]
        [Range(0, 10000, ErrorMessage = "A quantidade deve estar entre 0 e 10.000")]
        [EstoqueMinimoSorvete(10, ErrorMessage = "O estoque mínimo recomendado é de 10 unidades")]
        public int QuantidadeEstoque { get; set; }
        
        [Required(ErrorMessage = "Os ingredientes são obrigatórios")]
        [StringLength(1000, ErrorMessage = "Os ingredientes devem ter no máximo 1000 caracteres")]
        public string Ingredientes { get; set; }
        
        public bool Disponivel { get; set; }
        
        // REMOVIDO [Required] - não é preenchido pelo formulário
        public DateTime? DataCriacao { get; set; }
        
        [Required(ErrorMessage = "A categoria é obrigatória")]
        [Range(1, int.MaxValue, ErrorMessage = "Selecione uma categoria válida")]
        public int CategoriaId { get; set; }
        
        // ADICIONADO [Editable(false)] para indicar que não deve ser validado no POST
        [Editable(false)]
        public string? CategoriaNome { get; set; }
    }
}