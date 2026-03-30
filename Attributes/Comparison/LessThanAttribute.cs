namespace ValidationRelations.Attributes.Comparison
{
    public sealed class LessThanAttribute : PropertyComparisonAttribute
    {
        public LessThanAttribute(string otherProperty)
            : base(otherProperty, ComparisonOperator.LessThan)
        {
        }
    }
}
