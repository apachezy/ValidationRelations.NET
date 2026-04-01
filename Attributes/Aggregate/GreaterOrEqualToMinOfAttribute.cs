namespace ValidationRelations.Attributes.Aggregate
{
    /// <summary>
    /// 验证属性值不低于其它多个属性中的最小值。
    /// </summary>
    /// <example>
    /// // MinGuarantee 不能低于各基准值中的最小值
    /// [GreaterOrEqualToMinOf(nameof(Baseline1), nameof(Baseline2), nameof(Baseline3))]
    /// public decimal MinGuarantee { get; set; }
    /// </example>
    public sealed class GreaterOrEqualToMinOfAttribute : AggregateComparisonAttribute
    {
        public GreaterOrEqualToMinOfAttribute(params string[] otherProperties)
            : base(AggregateFunction.Min, ComparisonOperator.GreaterOrEqual, otherProperties)
        {
        }
    }
}
