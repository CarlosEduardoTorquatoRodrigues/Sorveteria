using System.ComponentModel.DataAnnotations;

namespace Sorveteria.Application.Validators
{
    public class EstoqueMinimoSorveteAttribute : ValidationAttribute
    {
        private readonly int _estoqueMinimo;

        public EstoqueMinimoSorveteAttribute(int estoqueMinimo)
        {
            _estoqueMinimo = estoqueMinimo;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null)
                return ValidationResult.Success;

            if (value is int quantidade)
            {
                if (quantidade < _estoqueMinimo)
                {
                    return new ValidationResult(ErrorMessage ?? $"Atenção: o estoque está abaixo do mínimo recomendado de {_estoqueMinimo} unidades");
                }
            }

            return ValidationResult.Success;
        }
    }
}