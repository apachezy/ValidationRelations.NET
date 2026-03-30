using System.ComponentModel.DataAnnotations;
using ValidationRelations.Infrastructure;

namespace ValidationRelations.Attributes.Conditional
{
    /// <summary>
    /// 只要 任意条件满足 就必填。
    /// </summary>
    /// <example>
    /// // 逻辑：Email 或 Phone 任意存在 → ContactName 必填
    /// [RequiredIfAny(nameof(Email), nameof(Phone))]
    /// public string ContactName { get; set; }
    /// </example>
    public sealed class RequiredIfAnyAttribute : ValidationAttribute
    {
        public string[] Properties { get; }

        public RequiredIfAnyAttribute(params string[] properties)
        {
            Properties = properties;
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext context)
        {
            foreach (var prop in Properties)
            {
                var v = PropertyValueResolver.GetValue(context.ObjectInstance, prop);

                if (RequiredHelper.HasValue(v))
                {
                    if (!RequiredHelper.HasValue(value))
                    {
                        return new ValidationResult(
                            ErrorMessage ?? $"{context.MemberName} is required.");
                    }

                    return ValidationResult.Success;
                }
            }

            return ValidationResult.Success;
        }
    }
}
