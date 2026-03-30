namespace ValidationRelations.Attributes.ConstantComparison
{
    /// <summary>
    /// 限制被标注成员不能大于指定常量值。
    /// </summary>
    public sealed class GreaterThanValueAttribute : DisallowComparisonAttribute
    {
        public GreaterThanValueAttribute(object value)
            : base(ConstantComparisonOperator.GreaterThan, value)
        {
        }
    }
}
