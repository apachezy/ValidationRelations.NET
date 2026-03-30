using System.ComponentModel.DataAnnotations;
using ValidationRelations.Infrastructure;

namespace ValidationRelations.Attributes.Conditional
{
    /// <summary>
    /// 只有 全部条件满足 才必填。
    /// </summary>
    /// <example>
    /// // 逻辑： StartDate &amp;&amp; EndDate 都存在 → Reason 必填
    /// [RequiredIfAll(nameof(StartDate), nameof(EndDate))]
    /// public string Reason { get; set; }
    /// </example>
    public sealed class RequiredIfAllAttribute : ValidationAttribute
    {
        public string[] Properties { get; }

        public RequiredIfAllAttribute(params string[] properties)
        {
            Properties = properties;
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext context)
        {
            foreach (var prop in Properties)
            {
                var v = PropertyValueResolver.GetValue(context.ObjectInstance, prop);

                if (!RequiredHelper.HasValue(v))
                {
                    return ValidationResult.Success;
                }
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
