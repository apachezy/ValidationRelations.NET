using System;
using System.Collections.Generic;
using System.Linq;

namespace ValidationRelations.Attributes.ConstantComparison
{
    /// <summary>
    /// 限制被标注成员的值必须等于指定值集合中的任意一个。
    /// </summary>
    public sealed class AllowedValuesAttribute : PropertyValidationAttribute
    {
        public IReadOnlyList<object?> AllowedValues { get; }

        public AllowedValuesAttribute(params object?[] allowedValues)
        {
            AllowedValues = allowedValues ?? throw new ArgumentNullException(nameof(allowedValues));
        }

        protected override bool IsValidCore(object? value, object instance)
        {
            if (value == null)
            {
                return true;
            }

            return AllowedValues.Any(allowedValue => Equals(value, allowedValue));
        }
    }
}