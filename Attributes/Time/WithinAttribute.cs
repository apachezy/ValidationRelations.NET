using ValidationRelations.Infrastructure;

namespace ValidationRelations.Attributes.Time
{
    /// <summary>
    /// 常用于时间区间
    /// </summary>
    /// <example>
    /// public class Booking
    /// {
    ///     public DateTime Start { get; set; }
    /// 
    ///     public DateTime End { get; set; }
    /// 
    ///     [Within(nameof(Start), nameof(End))]
    ///     public DateTime Time { get; set; }
    /// }
    /// </example>
    public class WithinAttribute : PropertyValidationAttribute
    {
        public string StartProperty { get; }
        public string EndProperty   { get; }

        public WithinAttribute(string startProperty, string endProperty)
        {
            StartProperty = startProperty;
            EndProperty   = endProperty;
        }

        protected override bool IsValidCore(object? value, object instance)
        {
            if (value == null)
            {
                return true;
            }

            var start = PropertyValueResolver.GetValue(instance, StartProperty);
            var end   = PropertyValueResolver.GetValue(instance, EndProperty);

            if (!ComparisonHelper.TryCompare(value, start, out var r1))
            {
                return true;
            }

            if (!ComparisonHelper.TryCompare(value, end, out var r2))
            {
                return true;
            }

            return r1 >= 0 && r2 <= 0;
        }
    }
}
