using System.ComponentModel.DataAnnotations;

namespace Sorveteria.Application.Validators
{

    public class PrecoMinimoSorveteAttribute : ValidationAttribute
    {
        private readonly decimal _precoMinimo;

        public PrecoMinimoSorveteAttribute(double precoMinimo)
        {
            _precoMinimo = (decimal)precoMinimo;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null)
            {
                return new ValidationResult("O preço não pode ser nulo");
            }

            decimal preco = Convert.ToDecimal(value);

            if (preco < _precoMinimo)
            {
                return new ValidationResult(
                    ErrorMessage ?? $"O preço deve ser no mínimo R$ {_precoMinimo:F2}"
                );
            }

            return ValidationResult.Success;
        }
    }
}