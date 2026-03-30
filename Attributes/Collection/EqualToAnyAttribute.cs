using ValidationRelations.Infrastructure;

namespace ValidationRelations.Attributes.Collection
{
    /// <summary>
    /// 允许值等于多个属性之一。
    /// </summary>
    /// <example>
    /// [EqualToAny(nameof(Start), nameof(End), nameof(Default))]
    /// public int Value { get; set; }
    /// </example>
    public sealed class EqualToAnyAttribute : PropertyValidationAttribute
    {
        public string[] OtherProperties { get; }

        public EqualToAnyAttribute(params string[] otherProperties)
        {
            OtherProperties = otherProperties;
        }

        protected override bool IsValidCore(object? value, object instance)
        {
            if (value == null)
            {
                return true;
            }

            foreach (var prop in OtherProperties)
            {
                var other = PropertyValueResolver.GetValue(instance, prop);

                if (Equals(value, other))
                {
                    return true;
                }
            }

            return false;
        }
    }
}
