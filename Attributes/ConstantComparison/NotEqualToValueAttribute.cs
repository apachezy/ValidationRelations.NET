namespace ValidationRelations.Attributes.ConstantComparison
{
    /// <summary>
    /// 限制被标注成员不能不等于指定常量值。
    /// </summary>
    public sealed class NotEqualToValueAttribute : DisallowComparisonAttribute
    {
        public NotEqualToValueAttribute(object value)
            : base(ConstantComparisonOperator.NotEqual, value)
        {
        }
    }
}
