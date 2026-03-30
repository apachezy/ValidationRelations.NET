namespace ValidationRelations.Attributes.Comparison
{
    public sealed class GreaterOrEqualAttribute : PropertyComparisonAttribute
    {
        public GreaterOrEqualAttribute(string otherProperty)
            : base(otherProperty, ComparisonOperator.GreaterOrEqual)
        {
        }
    }
}
