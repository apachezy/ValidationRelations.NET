namespace ValidationRelations.Attributes.Comparison
{
    public sealed class GreaterThanAttribute : PropertyComparisonAttribute
    {
        public GreaterThanAttribute(string otherProperty)
            : base(otherProperty, ComparisonOperator.GreaterThan)
        {
        }
    }
}
