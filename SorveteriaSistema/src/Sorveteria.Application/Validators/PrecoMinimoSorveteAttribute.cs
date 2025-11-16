using System.ComponentModel.DataAnnotations;

namespace Sorveteria.Application.Validators
{
    public class PrecoMinimoSorveteAttribute : ValidationAttribute
    {
        private readonly double _precoMinimo;

        public PrecoMinimoSorveteAttribute(double precoMinimo)
        {
            _precoMinimo = precoMinimo;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null)
                return ValidationResult.Success;

            if (value is decimal preco)
            {
                if (preco < (decimal)_precoMinimo)
                {
                    return new ValidationResult(ErrorMessage ?? $"O preço deve ser no mínimo R$ {_precoMinimo:F2}");
                }
            }

            return ValidationResult.Success;
        }
    }
}