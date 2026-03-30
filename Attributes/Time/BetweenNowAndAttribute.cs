using System;
using ValidationRelations.Infrastructure;

namespace ValidationRelations.Attributes.Time
{
    /// <summary>
    /// 用于：Now &lt;= Value &lt;= SomeProperty
    /// </summary>
    /// <example>
    /// [BetweenNowAnd(nameof(ExpireTime))]
    /// public DateTime ExecuteTime { get; set; }
    /// </example>
    public sealed class BetweenNowAndAttribute : PropertyValidationAttribute
    {
        public string EndProperty { get; }

        public BetweenNowAndAttribute(string endProperty)
        {
            EndProperty = endProperty;
        }

        protected override bool IsValidCore(object? value, object instance)
        {
            if (value == null)
            {
                return true;
            }

            var end = PropertyValueResolver.GetValue(instance, EndProperty);

            if (!ComparisonHelper.TryCompare(value, DateTime.Now, out var r1))
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
