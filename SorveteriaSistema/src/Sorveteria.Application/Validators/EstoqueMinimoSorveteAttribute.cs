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
            {
                return new ValidationResult("A quantidade em estoque não pode ser nula");
            }

            int quantidade = Convert.ToInt32(value);

            if (quantidade < _estoqueMinimo)
            {
                return new ValidationResult(
                    ErrorMessage ?? $"O estoque mínimo recomendado é de {_estoqueMinimo} unidades"
                );
            }

            return ValidationResult.Success;
        }
    }
}