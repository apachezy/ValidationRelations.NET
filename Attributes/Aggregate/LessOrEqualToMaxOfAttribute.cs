namespace ValidationRelations.Attributes.Aggregate
{
    /// <summary>
    /// 验证属性值不超过其它多个属性中的最大值。
    /// </summary>
    /// <example>
    /// // SelectedValue 不能超过各候选值中的最大值
    /// [LessOrEqualToMaxOf(nameof(Option1), nameof(Option2), nameof(Option3))]
    /// public decimal SelectedValue { get; set; }
    /// </example>
    public sealed class LessOrEqualToMaxOfAttribute : AggregateComparisonAttribute
    {
        public LessOrEqualToMaxOfAttribute(params string[] otherProperties)
            : base(AggregateFunction.Max, ComparisonOperator.LessOrEqual, otherProperties)
        {
        }
    }
}
