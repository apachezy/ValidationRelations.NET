namespace ValidationRelations.Attributes.ConstantComparison
{
    /// <summary>
    /// 限制被标注成员不能不介于两个常量值之间。
    /// </summary>
    public sealed class NotBetweenValuesAttribute : DisallowComparisonAttribute
    {
        public NotBetweenValuesAttribute(object minValue, object maxValue)
            : base(ConstantComparisonOperator.NotBetween, minValue, maxValue)
        {
        }
    }
}
