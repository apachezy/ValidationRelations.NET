using System.ComponentModel.DataAnnotations;
using ValidationRelations.Infrastructure;

namespace ValidationRelations.Attributes.Conditional
{
    /// <summary>
    /// 条件 不满足时必填。
    /// 逻辑：Status != Draft → Approver 必填
    /// </summary>
    /// <example>
    /// [RequiredIfNot(nameof(Status), OrderStatus.Draft)]
    /// public string Approver { get; set; }
    /// </example>
    public sealed class RequiredIfNotAttribute : ValidationAttribute
    {
        public string  OtherProperty { get; }
        public object? ExpectedValue { get; }

        public RequiredIfNotAttribute(string otherProperty, object? expectedValue)
        {
            OtherProperty = otherProperty;
            ExpectedValue = expectedValue;
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext context)
        {
            var other = PropertyValueResolver.GetValue(context.ObjectInstance, OtherProperty);

            if (Equals(other, ExpectedValue))
            {
                return ValidationResult.Success;
            }

            if (!RequiredHelper.HasValue(value))
            {
                return new ValidationResult(
                    ErrorMessage ?? $"{context.MemberName} is required.");
            }

            return ValidationResult.Success;
        }
    }
}
