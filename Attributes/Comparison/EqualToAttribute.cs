namespace ValidationRelations.Attributes.Comparison
{
    public sealed class EqualToAttribute : PropertyComparisonAttribute
    {
        public EqualToAttribute(string otherProperty)
            : base(otherProperty, ComparisonOperator.Equal)
        {
        }
    }
}
