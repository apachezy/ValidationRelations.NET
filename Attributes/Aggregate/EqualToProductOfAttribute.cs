namespace ValidationRelations.Attributes.Aggregate
{
    /// <summary>
    /// 验证属性值等于其它多个属性之积。
    /// </summary>
    /// <example>
    /// // Area 必须等于 Width * Height
    /// [EqualToProductOf(nameof(Width), nameof(Height))]
    /// public decimal Area { get; set; }
    ///
    /// // Amount 必须等于 Price * Quantity
    /// [EqualToProductOf(nameof(Price), nameof(Quantity))]
    /// public decimal Amount { get; set; }
    /// </example>
    public sealed class EqualToProductOfAttribute : AggregateComparisonAttribute
    {
        public EqualToProductOfAttribute(params string[] otherProperties)
            : base(AggregateFunction.Product, ComparisonOperator.Equal, otherProperties)
        {
        }
    }
}
