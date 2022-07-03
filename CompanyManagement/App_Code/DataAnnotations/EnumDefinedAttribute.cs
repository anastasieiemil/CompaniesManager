using System.ComponentModel.DataAnnotations;

namespace CompanyManagement.App_Code.DataAnnotations
{
    public class EnumDefinedAttribute : ValidationAttribute
    {
        private Type enumType;
        public EnumDefinedAttribute(Type enumType)
        {
            if (enumType is null)
                throw new ArgumentNullException($"Parameter {nameof(enumType)} is null");
            if (!enumType.IsEnum)
                throw new ArgumentException($"Parameter {nameof(enumType)} is not an enumeration");

            this.enumType = enumType;
        }

        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            return Enum.IsDefined(enumType, value) ? ValidationResult.Success : new ValidationResult($"Value {value} is not enum of type {enumType}");
        }
    }
}
