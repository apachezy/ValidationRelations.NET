namespace ValidationRelations.Attributes.Aggregate
{
    /// <summary>
    /// 验证属性值等于其它多个属性之和。
    /// </summary>
    /// <example>
    /// // Total 必须等于 SubTotal + Tax + Shipping
    /// [EqualToSumOf(nameof(SubTotal), nameof(Tax), nameof(Shipping))]
    /// public decimal Total { get; set; }
    /// </example>
    public sealed class EqualToSumOfAttribute : AggregateComparisonAttribute
    {
        public EqualToSumOfAttribute(params string[] otherProperties)
            : base(AggregateFunction.Sum, ComparisonOperator.Equal, otherProperties)
        {
        }
    }
}
