namespace ValidationRelations.Attributes.Aggregate
{
    /// <summary>
    /// 验证属性值大于其它多个属性之和。
    /// </summary>
    /// <example>
    /// // Budget 必须大于所有支出之和
    /// [GreaterThanSumOf(nameof(LaborCost), nameof(MaterialCost), nameof(TransportCost))]
    /// public decimal Budget { get; set; }
    /// </example>
    public sealed class GreaterThanSumOfAttribute : AggregateComparisonAttribute
    {
        public GreaterThanSumOfAttribute(params string[] otherProperties)
            : base(AggregateFunction.Sum, ComparisonOperator.GreaterThan, otherProperties)
        {
        }
    }
}
