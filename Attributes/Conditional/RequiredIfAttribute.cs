using System.ComponentModel.DataAnnotations;
using ValidationRelations.Infrastructure;

namespace ValidationRelations.Attributes.Conditional
{
    /// <summary>
    /// </summary>
    /// <example>
    /// // 逻辑：Type == Special → SpecialCode 必填
    /// [RequiredIf(nameof(Type), OrderType.Special)]
    /// public string SpecialCode { get; set; }
    /// </example>
    public sealed class RequiredIfAttribute : ValidationAttribute
    {
        public string  OtherProperty { get; }
        public object? ExpectedValue { get; }

        public RequiredIfAttribute(string otherProperty, object? expectedValue)
        {
            OtherProperty = otherProperty;
            ExpectedValue = expectedValue;
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext context)
        {
            var other = PropertyValueResolver.GetValue(context.ObjectInstance, OtherProperty);

            if (!Equals(other, ExpectedValue))
            {
                return ValidationResult.Success;
            }

            if (value == null)
            {
                return new ValidationResult(ErrorMessage ?? $"{context.MemberName} is required.");
            }

            if (value is string s && string.IsNullOrWhiteSpace(s))
            {
                return new ValidationResult(ErrorMessage ?? $"{context.MemberName} is required.");
            }

            return ValidationResult.Success;
        }
    }
}
