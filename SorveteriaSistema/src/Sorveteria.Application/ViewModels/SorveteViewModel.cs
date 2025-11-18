using System.ComponentModel.DataAnnotations;
using Sorveteria.Application.Validators;

namespace Sorveteria.Application.ViewModels
{

    [DisponibilidadeCoerente(ErrorMessage = "A disponibilidade do sorvete não está coerente com o estoque")]
    public class SorveteViewModel : IValidatableObject
    {
        public int Id { get; set; }
        
        [Required(ErrorMessage = "O nome é obrigatório")]
        [StringLength(100, MinimumLength = 3, ErrorMessage = "O nome deve ter entre 3 e 100 caracteres")]
        [RegularExpression(@"^[a-zA-ZÀ-ÿ\s]+$", ErrorMessage = "O nome deve conter apenas letras e espaços")]
        [NomeSorveteValido(ErrorMessage = "Nome inválido para o sorvete")]
        [Display(Name = "Nome do Sorvete")]
        public string Nome { get; set; } = string.Empty;
        
        [Required(ErrorMessage = "O sabor é obrigatório")]
        [StringLength(100, MinimumLength = 2, ErrorMessage = "O sabor deve ter entre 2 e 100 caracteres")]
        [RegularExpression(@"^[a-zA-ZÀ-ÿ\s,]+$", ErrorMessage = "O sabor deve conter apenas letras, espaços e vírgulas")]
        [Display(Name = "Sabor Principal")]
        public string Sabor { get; set; } = string.Empty;
        
        [Required(ErrorMessage = "O preço é obrigatório")]
        [Range(0.01, 9999.99, ErrorMessage = "O preço deve estar entre R$ 0,01 e R$ 9.999,99")]
        [PrecoMinimoSorvete(5.00, ErrorMessage = "O preço mínimo para um sorvete é R$ 5,00")]
        [DataType(DataType.Currency)]
        [Display(Name = "Preço (R$)")]
        public decimal Preco { get; set; }
        
        [Required(ErrorMessage = "A quantidade em estoque é obrigatória")]
        [Range(0, 10000, ErrorMessage = "A quantidade deve estar entre 0 e 10.000")]
        [EstoqueMinimoSorvete(10, ErrorMessage = "O estoque mínimo recomendado é de 10 unidades")]
        [Display(Name = "Quantidade em Estoque")]
        public int QuantidadeEstoque { get; set; }
        
        [Required(ErrorMessage = "Os ingredientes são obrigatórios")]
        [StringLength(1000, MinimumLength = 10, ErrorMessage = "Os ingredientes devem ter entre 10 e 1000 caracteres")]
        [IngredientesValidos(ErrorMessage = "Os ingredientes devem conter pelo menos 3 itens separados por vírgula")]
        [Display(Name = "Ingredientes")]
        public string Ingredientes { get; set; } = string.Empty;
        
        [Required(ErrorMessage = "O campo disponibilidade é obrigatório")]
        [Display(Name = "Disponível para Venda")]
        public bool Disponivel { get; set; }
        
        [Display(Name = "Data de Cadastro")]
        public DateTime? DataCriacao { get; set; }
        
        [Required(ErrorMessage = "A categoria é obrigatória")]
        [Range(1, int.MaxValue, ErrorMessage = "Selecione uma categoria válida")]
        [Display(Name = "Categoria")]
        public int CategoriaId { get; set; }
        
        [Editable(false)]
        [Display(Name = "Nome da Categoria")]
        public string? CategoriaNome { get; set; }


        public IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
        {
            var results = new List<ValidationResult>();


            if (Preco > 50 && !Ingredientes.ToLower().Contains("premium") 
                && !Ingredientes.ToLower().Contains("importado")
                && !Ingredientes.ToLower().Contains("gourmet"))
            {
                results.Add(new ValidationResult(
                    "Sorvetes com preço acima de R$ 50,00 devem conter ingredientes premium, importados ou gourmet",
                    new[] { nameof(Ingredientes), nameof(Preco) }
                ));
            }


            if (!string.IsNullOrWhiteSpace(Nome) && !string.IsNullOrWhiteSpace(Sabor))
            {
                if (Nome.Trim().Equals(Sabor.Trim(), StringComparison.OrdinalIgnoreCase))
                {
                    results.Add(new ValidationResult(
                        "O nome e o sabor do sorvete não podem ser idênticos",
                        new[] { nameof(Nome), nameof(Sabor) }
                    ));
                }
            }


            if (Preco < 10 && QuantidadeEstoque < 20)
            {
                results.Add(new ValidationResult(
                    "Sorvetes econômicos (abaixo de R$ 10,00) devem ter estoque mínimo de 20 unidades",
                    new[] { nameof(QuantidadeEstoque) }
                ));
            }


            var ingredientesLower = Ingredientes?.ToLower() ?? "";
            var itemsLacteos = new[] { "leite", "creme", "nata", "iogurte", "queijo" };
            
            if (!itemsLacteos.Any(item => ingredientesLower.Contains(item)))
            {
                results.Add(new ValidationResult(
                    "Os ingredientes devem conter pelo menos um item lácteo (leite, creme, nata, iogurte ou queijo)",
                    new[] { nameof(Ingredientes) }
                ));
            }

            return results;
        }
    }
}