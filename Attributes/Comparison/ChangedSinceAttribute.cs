using ValidationRelations.Infrastructure;

namespace ValidationRelations.Attributes.Comparison
{
    /// <summary>
    /// 状态变化检测
    /// </summary>
    /// <example>
    /// public class UserProfile
    /// {
    ///     public string OriginalEmail { get; set; }
    /// 
    ///     [ChangedSince(nameof(OriginalEmail))]
    ///     public string Email { get; set; }
    /// }
    /// </example>
    public class ChangedSinceAttribute : PropertyValidationAttribute
    {
        public string OriginalProperty { get; }

        public ChangedSinceAttribute(string originalProperty)
        {
            OriginalProperty = originalProperty;
        }

        protected override bool IsValidCore(object? value, object instance)
        {
            var original = PropertyValueResolver.GetValue(instance, OriginalProperty);

            if (value == null && original == null)
            {
                return false;
            }

            return !Equals(value, original);
        }
    }
}
