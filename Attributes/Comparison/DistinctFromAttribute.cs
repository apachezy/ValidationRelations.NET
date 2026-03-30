using ValidationRelations.Infrastructure;

namespace ValidationRelations.Attributes.Comparison
{
    /// <summary>
    /// 语义比 NotEqualTo 更清晰。
    /// </summary>
    /// <example>
    /// [DistinctFrom(nameof(OwnerId))]
    /// public int ApproverId { get; set; }
    /// </example>
    public sealed class DistinctFromAttribute : PropertyValidationAttribute
    {
        public string OtherProperty { get; }

        public DistinctFromAttribute(string otherProperty)
        {
            OtherProperty = otherProperty;
        }

        protected override bool IsValidCore(object? value, object instance)
        {
            var other = PropertyValueResolver.GetValue(instance, OtherProperty);

            if (value == null && other == null)
            {
                return false;
            }

            return !Equals(value, other);
        }
    }
}
