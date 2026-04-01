namespace ValidationRelations.Attributes.Aggregate
{
    /// <summary>
    /// 验证属性值小于其它多个属性之和。
    /// </summary>
    /// <example>
    /// // ActualUsage 必须小于各配额之和
    /// [LessThanSumOf(nameof(Quota1), nameof(Quota2), nameof(Quota3))]
    /// public decimal ActualUsage { get; set; }
    /// </example>
    public sealed class LessThanSumOfAttribute : AggregateComparisonAttribute
    {
        public LessThanSumOfAttribute(params string[] otherProperties)
            : base(AggregateFunction.Sum, ComparisonOperator.LessThan, otherProperties)
        {
        }
    }
}
