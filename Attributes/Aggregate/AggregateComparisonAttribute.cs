using ValidationRelations.Infrastructure;

namespace ValidationRelations.Attributes.Aggregate
{
    /// <summary>
    /// 聚合比较验证基类。将当前属性值与其它多个属性的聚合运算结果进行比较。
    /// </summary>
    /// <example>
    /// // 验证 Total 等于 SubTotal + Tax + Shipping 之和
    /// [AggregateComparison(AggregateFunction.Sum, ComparisonOperator.Equal,
    ///     nameof(SubTotal), nameof(Tax), nameof(Shipping))]
    /// public decimal Total { get; set; }
    /// </example>
    public class AggregateComparisonAttribute : PropertyValidationAttribute
    {
        /// <summary>
        /// 参与运算的其它属性名。
        /// </summary>
        public string[] OtherProperties { get; }

        /// <summary>
        /// 聚合运算类型。
        /// </summary>
        public AggregateFunction Function { get; }

        /// <summary>
        /// 比较运算符。
        /// </summary>
        public ComparisonOperator Operator { get; }

        public AggregateComparisonAttribute(
            AggregateFunction function,
            ComparisonOperator op,
            params string[] otherProperties)
        {
            Function        = function;
            Operator        = op;
            OtherProperties = otherProperties;
        }

        protected override bool IsValidCore(object? value, object instance)
        {
            if (value == null)
            {
                return true;
            }

            if (!ArithmeticHelper.TryToDecimal(value, out var decimalValue))
            {
                return true;
            }

            var otherValues = new object?[OtherProperties.Length];

            for (var i = 0; i < OtherProperties.Length; i++)
            {
                otherValues[i] = PropertyValueResolver.GetValue(instance, OtherProperties[i]);
            }

            if (!ArithmeticHelper.TryAggregate(otherValues, Function, out var aggregateResult))
            {
                return true;
            }

            var compareResult = ComparisonHelper.CompareDecimalTolerant(decimalValue, aggregateResult);

            return ComparisonHelper.Evaluate(compareResult, Operator);
        }
    }
}
