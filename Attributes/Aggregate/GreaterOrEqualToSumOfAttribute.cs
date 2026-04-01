namespace ValidationRelations.Attributes.Aggregate
{
    /// <summary>
    /// 验证属性值大于等于其它多个属性之和。
    /// </summary>
    /// <example>
    /// // Capacity 必须大于等于所有货物重量之和
    /// [GreaterOrEqualToSumOf(nameof(CargoWeight1), nameof(CargoWeight2), nameof(CargoWeight3))]
    /// public decimal Capacity { get; set; }
    /// </example>
    public sealed class GreaterOrEqualToSumOfAttribute : AggregateComparisonAttribute
    {
        public GreaterOrEqualToSumOfAttribute(params string[] otherProperties)
            : base(AggregateFunction.Sum, ComparisonOperator.GreaterOrEqual, otherProperties)
        {
        }
    }
}
