using System.ComponentModel.DataAnnotations;

namespace Sorveteria.Application.Validators
{

    public class DisponibilidadeCoerente : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            var sorvete = validationContext.ObjectInstance;
            var quantidadeProperty = sorvete.GetType().GetProperty("QuantidadeEstoque");
            var disponivelProperty = sorvete.GetType().GetProperty("Disponivel");

            if (quantidadeProperty == null || disponivelProperty == null)
            {
                return ValidationResult.Success;
            }

            int quantidade = (int)quantidadeProperty.GetValue(sorvete);
            bool disponivel = (bool)disponivelProperty.GetValue(sorvete);


            if (quantidade > 0 && !disponivel)
            {
                return new ValidationResult(
                    "Sorvete com estoque disponível não deveria estar marcado como indisponível"
                );
            }


            if (quantidade == 0 && disponivel)
            {
                return new ValidationResult(
                    "Sorvete sem estoque não pode estar disponível para venda"
                );
            }

            return ValidationResult.Success;
        }
    }
}