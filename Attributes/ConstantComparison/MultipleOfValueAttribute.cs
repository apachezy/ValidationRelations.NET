using System;
using System.ComponentModel.DataAnnotations;
using ValidationRelations.Infrastructure;

namespace ValidationRelations.Attributes.ConstantComparison
{
    /// <summary>
    /// 验证属性值必须是指定常量的整数倍。
    /// </summary>
    /// <example>
    /// public class Product
    /// {
    ///     [MultipleOfValue(0.5)]
    ///     public decimal Weight { get; set; }
    ///
    ///     [MultipleOfValue(12)]
    ///     public int Quantity { get; set; }
    /// }
    /// </example>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter)]
    public sealed class MultipleOfValueAttribute : ValidationAttribute
    {
        /// <summary>
        /// 作为除数的常量值。
        /// </summary>
        public object Divisor { get; }

        /// <summary>
        /// 初始化 <see cref="MultipleOfValueAttribute"/>。
        /// </summary>
        /// <param name="divisor">作为除数的常量值。</param>
        public MultipleOfValueAttribute(object divisor)
        {
            Divisor = divisor;
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext context)
        {
            if (value == null)
            {
                return ValidationResult.Success;
            }

            if (!ArithmeticHelper.TryToDecimal(value, out var dividend))
            {
                return ValidationResult.Success;
            }

            if (!ArithmeticHelper.TryToDecimal(Divisor, out var divisor))
            {
                return ValidationResult.Success;
            }

            if (divisor == 0m)
            {
                return ValidationResult.Success;
            }

            if (dividend % divisor == 0m)
            {
                return ValidationResult.Success;
            }

            var message = ErrorMessage ??
                          $"{context.MemberName} must be a multiple of {Divisor}.";

            return new ValidationResult(message);
        }
    }
}
