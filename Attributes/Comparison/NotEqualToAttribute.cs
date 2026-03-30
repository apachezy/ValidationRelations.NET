namespace ValidationRelations.Attributes.Comparison
{
    public sealed class NotEqualToAttribute : PropertyComparisonAttribute
    {
        public NotEqualToAttribute(string otherProperty)
            : base(otherProperty, ComparisonOperator.NotEqual)
        {
        }
    }
}
