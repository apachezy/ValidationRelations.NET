using ValidationRelations.Infrastructure;

namespace ValidationRelations.Attributes.Comparison
{
    /// <summary>
    /// 
    /// </summary>
    /// <example>
    /// public class Range
    /// {
    ///     public int Min { get; set; }
    ///
    ///     public int Max { get; set; }
    ///
    ///     [Between(nameof(Min), nameof(Max))]
    ///     public int Value { get; set; }
    /// }
    /// </example>
    public class BetweenAttribute : PropertyValidationAttribute
    {
        public string MinProperty { get; }
        public string MaxProperty { get; }

        public BetweenAttribute(string minProperty, string maxProperty)
        {
            MinProperty = minProperty;
            MaxProperty = maxProperty;
        }

        protected override bool IsValidCore(object? value, object instance)
        {
            if (value == null)
            {
                return true;
            }

            var min = PropertyValueResolver.GetValue(instance, MinProperty);
            var max = PropertyValueResolver.GetValue(instance, MaxProperty);

            if (!ComparisonHelper.TryCompare(value, min, out var r1))
            {
                return true;
            }

            if (!ComparisonHelper.TryCompare(value, max, out var r2))
            {
                return true;
            }

            return r1 >= 0 && r2 <= 0;
        }
    }
}