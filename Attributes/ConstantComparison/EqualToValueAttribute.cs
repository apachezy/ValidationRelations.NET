namespace ValidationRelations.Attributes.ConstantComparison
{
    /// <summary>
    /// 限制被标注成员不能等于指定常量值。
    /// </summary>
    public sealed class EqualToValueAttribute : DisallowComparisonAttribute
    {
        public EqualToValueAttribute(object value)
            : base(ConstantComparisonOperator.Equal, value)
        {
        }
    }
}
