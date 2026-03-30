namespace ValidationRelations.Attributes.ConstantComparison
{
    /// <summary>
    /// 限制被标注成员不能小于指定常量值。
    /// </summary>
    public sealed class LessThanValueAttribute : DisallowComparisonAttribute
    {
        public LessThanValueAttribute(object value)
            : base(ConstantComparisonOperator.LessThan, value)
        {
        }
    }
}
