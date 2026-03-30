namespace ValidationRelations.Attributes.Comparison
{
    public class NotBetweenAttribute : BetweenAttribute
    {
        public NotBetweenAttribute(string minProperty, string maxProperty)
            : base(minProperty, maxProperty)
        {
        }

        protected override bool IsValidCore(object? value, object instance)
        {
            return !base.IsValidCore(value, instance);
        }
    }
}
