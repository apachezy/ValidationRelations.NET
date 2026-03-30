namespace ValidationRelations.Attributes.ConstantComparison
{
    /// <summary>
    /// 限制被标注成员不能介于两个常量值之间。
    /// </summary>
    public sealed class BetweenValuesAttribute : DisallowComparisonAttribute
    {
        public BetweenValuesAttribute(object minValue, object maxValue)
            : base(ConstantComparisonOperator.Between, minValue, maxValue)
        {
        }
    }
}
