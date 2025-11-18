using System.ComponentModel.DataAnnotations;

namespace Sorveteria.Application.Validators
{

    public class NomeSorveteValidoAttribute : ValidationAttribute
    {
        private static readonly string[] PalavrasProibidas = 
        { 
            "teste", 
            "test", 
            "xxx", 
            "fake",
            "dummy",
            "sample",
            "exemplo"
        };

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            if (value == null || string.IsNullOrWhiteSpace(value.ToString()))
            {
                return ValidationResult.Success; 
            }

            string nome = value.ToString().ToLower().Trim();

            foreach (var palavra in PalavrasProibidas)
            {
                if (nome.Contains(palavra))
                {
                    return new ValidationResult(
                        $"O nome não pode conter a palavra '{palavra}'"
                    );
                }
            }


            if (nome.Any(char.IsDigit))
            {
                return new ValidationResult("O nome não pode conter números");
            }

            return ValidationResult.Success;
        }
    }
}