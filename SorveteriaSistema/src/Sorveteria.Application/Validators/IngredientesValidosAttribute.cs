using System.ComponentModel.DataAnnotations;

namespace Sorveteria.Application.Validators
{

    public class IngredientesValidosAttribute : ValidationAttribute
    {
        private const int MinimosIngredientes = 3;

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null || string.IsNullOrWhiteSpace(value.ToString()))
            {
                return new ValidationResult("Os ingredientes não podem ser vazios");
            }

            string ingredientes = value.ToString().Trim();


            var listaIngredientes = ingredientes
                .Split(',')
                .Select(i => i.Trim())
                .Where(i => !string.IsNullOrWhiteSpace(i))
                .ToList();

            if (listaIngredientes.Count < MinimosIngredientes)
            {
                return new ValidationResult(
                    ErrorMessage ?? $"Deve conter pelo menos {MinimosIngredientes} ingredientes separados por vírgula"
                );
            }


            foreach (var ingrediente in listaIngredientes)
            {
                if (ingrediente.Length < 2)
                {
                    return new ValidationResult(
                        "Cada ingrediente deve ter pelo menos 2 caracteres"
                    );
                }
            }

            return ValidationResult.Success;
        }
    }
}