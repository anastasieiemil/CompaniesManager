using System.ComponentModel.DataAnnotations;

namespace CompanyManagement.App_Code.DataAnnotations
{
    public class MaxSizeAttribute : ValidationAttribute
    {
        private readonly int maxSize;
        public MaxSizeAttribute(int maxSize)
        {
            this.maxSize = maxSize;
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value is IFormFile file)
            {
                if (file.Length > maxSize)
                {
                    return new ValidationResult(ErrorMessage = $"Maximum size is {maxSize} Bytes");
                }
                else
                {
                    return ValidationResult.Success;
                }
            }

            return new ValidationResult(ErrorMessage = $"Invalid type.");

        }
    }
}
