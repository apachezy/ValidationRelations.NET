namespace ValidationRelations.Attributes.Comparison
{
    public sealed class LessOrEqualAttribute : PropertyComparisonAttribute
    {
        public LessOrEqualAttribute(string otherProperty)
            : base(otherProperty, ComparisonOperator.LessOrEqual)
        {
        }
    }
}
