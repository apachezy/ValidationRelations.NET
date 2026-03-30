using System.ComponentModel.DataAnnotations;
using ValidationRelations.Infrastructure;

namespace ValidationRelations.Attributes
{
    // 核心Attribute
    // 具备的能力
    // ✔ 普通属性比较
    // ✔ 嵌套属性路径比较
    // ✔ null-safe访问
    // ✔ 跨数值类型比较
    // ✔ Enum比较
    // ✔ Nullable类型
    // ✔ Expression Getter Cache
    // ✔ WPF / ASP.NET Core / DataAnnotations 通用
    public class PropertyComparisonAttribute : ValidationAttribute
    {
        public string OtherProperty { get; }

        public ComparisonOperator Operator { get; }

        public PropertyComparisonAttribute(string otherProperty, ComparisonOperator op)
        {
            OtherProperty = otherProperty;
            Operator      = op;
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext context)
        {
            var getter = PropertyGetterCache.GetGetter(context.ObjectType, OtherProperty);

            var otherValue = getter(context.ObjectInstance);

            if (!ComparisonHelper.TryCompare(value, otherValue, out var compareResult))
            {
                return ValidationResult.Success;
            }

            if (!ComparisonHelper.Evaluate(compareResult, Operator))
            {
                var message = ErrorMessage ??
                              $"{context.MemberName} must be {Operator} {OtherProperty}.";

                return new ValidationResult(message);
            }

            return ValidationResult.Success;
        }
    }
}