namespace ValidationRelations.Attributes.Aggregate
{
    /// <summary>
    /// 验证属性值小于等于其它多个属性之和。
    /// </summary>
    /// <example>
    /// // TotalShipped 不能超过各批次发货量之和
    /// [LessOrEqualToSumOf(nameof(Batch1), nameof(Batch2), nameof(Batch3))]
    /// public decimal TotalShipped { get; set; }
    /// </example>
    public sealed class LessOrEqualToSumOfAttribute : AggregateComparisonAttribute
    {
        public LessOrEqualToSumOfAttribute(params string[] otherProperties)
            : base(AggregateFunction.Sum, ComparisonOperator.LessOrEqual, otherProperties)
        {
        }
    }
}
