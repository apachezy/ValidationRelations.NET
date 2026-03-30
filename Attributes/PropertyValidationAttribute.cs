using System.ComponentModel.DataAnnotations;

namespace ValidationRelations.Attributes
{
    public abstract class PropertyValidationAttribute : ValidationAttribute
    {
        protected abstract bool IsValidCore(object? value, object instance);

        protected override ValidationResult? IsValid(object? value, ValidationContext context)
        {
            var instance = context.ObjectInstance;

            if (IsValidCore(value, instance))
            {
                return ValidationResult.Success;
            }

            var message = ErrorMessage ?? $"{context.MemberName} validation failed.";

            return new ValidationResult(message);
        }
    }
}