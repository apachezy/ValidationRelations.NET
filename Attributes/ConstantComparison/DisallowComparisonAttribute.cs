using System;
using System.ComponentModel.DataAnnotations;
using ValidationRelations.Infrastructure;

namespace ValidationRelations.Attributes.ConstantComparison
{
    /// <summary>
    /// 限制被标注成员不能与指定常量值满足某种比较关系。
    /// </summary>
    /// <example>
    /// public class Demo
    /// {
    ///     [DisallowComparison(ConstantComparisonOperator.Equal, 0)]
    ///     public int Status { get; set; }
    ///
    ///     [DisallowComparison(ConstantComparisonOperator.Between, 1.5, 3.5)]
    ///     public decimal Weight { get; set; }
    /// }
    /// </example>
    [AttributeUsage(AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter, AllowMultiple = true)]
    public class DisallowComparisonAttribute : ValidationAttribute
    {
        /// <summary>
        /// 不允许满足的比较操作。
        /// </summary>
        public ConstantComparisonOperator Operator { get; }

        /// <summary>
        /// 第一个比较值。
        /// </summary>
        public object Value { get; }

        /// <summary>
        /// 第二个比较值，仅在区间比较时使用。
        /// </summary>
        public object? SecondValue { get; }

        /// <summary>
        /// 初始化常量比较限制特性。
        /// </summary>
        /// <param name="operator">不允许满足的比较操作。</param>
        /// <param name="value">第一个比较值。</param>
        public DisallowComparisonAttribute(ConstantComparisonOperator @operator, object value)
        {
            Operator = @operator;
            Value    = value;
        }

        /// <summary>
        /// 初始化区间常量比较限制特性。
        /// </summary>
        /// <param name="operator">不允许满足的比较操作。</param>
        /// <param name="value">第一个比较值。</param>
        /// <param name="secondValue">第二个比较值。</param>
        public DisallowComparisonAttribute(ConstantComparisonOperator @operator, object value, object secondValue)
            : this(@operator, value)
        {
            SecondValue = secondValue;
        }

        protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
        {
            if (value == null)
            {
                return ValidationResult.Success;
            }

            var actualType = Nullable.GetUnderlyingType(value.GetType()) ?? value.GetType();

            if (!ConstantComparisonHelper.IsNumericType(actualType))
            {
                throw new InvalidOperationException($"{nameof(DisallowComparisonAttribute)} 仅支持数值类型成员。当前类型: {actualType.FullName}");
            }

            var isInvalid = IsRangeOperator(Operator)
                ? MatchesRange(value)
                : MatchesSingleValue(value);

            if (!isInvalid)
            {
                return ValidationResult.Success;
            }

            var message = ErrorMessage ?? $"{validationContext.DisplayName} 的值不能{GetDescription()}。";
            return new ValidationResult(message);
        }

        private bool MatchesSingleValue(object value)
        {
            return ConstantComparisonHelper.TryCompare(value, Value, out var result)
                && ConstantComparisonHelper.Evaluate(result, Operator);
        }

        private bool MatchesRange(object value)
        {
            if (SecondValue == null)
            {
                throw new InvalidOperationException(
                    $"{nameof(ConstantComparisonOperator.Between)} 和 {nameof(ConstantComparisonOperator.NotBetween)} 必须同时提供两个比较值。");
            }

            if (!ConstantComparisonHelper.TryCompare(Value, SecondValue, out var boundResult))
            {
                throw new InvalidOperationException("无法比较区间边界值。请确认比较值都是数值类型。");
            }

            var minValue = boundResult <= 0 ? Value : SecondValue;
            var maxValue = boundResult <= 0 ? SecondValue : Value;

            if (!ConstantComparisonHelper.TryCompare(value, minValue, out var minResult)
             || !ConstantComparisonHelper.TryCompare(value, maxValue, out var maxResult))
            {
                throw new InvalidOperationException("无法执行区间比较。请确认被校验值和比较值都是兼容的数值类型。");
            }

            var isBetween = minResult >= 0 && maxResult <= 0;
            return Operator == ConstantComparisonOperator.Between ? isBetween : !isBetween;
        }

        private static bool IsRangeOperator(ConstantComparisonOperator @operator)
        {
            return @operator == ConstantComparisonOperator.Between || @operator == ConstantComparisonOperator.NotBetween;
        }

        private string GetDescription()
        {
            return Operator switch
            {
                ConstantComparisonOperator.Equal       => $"等于 {Value}",
                ConstantComparisonOperator.NotEqual    => $"不等于 {Value}",
                ConstantComparisonOperator.GreaterThan => $"大于 {Value}",
                ConstantComparisonOperator.LessThan    => $"小于 {Value}",
                ConstantComparisonOperator.Between     => $"介于 {Value} 和 {SecondValue} 之间",
                ConstantComparisonOperator.NotBetween  => $"不介于 {Value} 和 {SecondValue} 之间",
                _                                      => Operator.ToString()
            };
        }
    }
}