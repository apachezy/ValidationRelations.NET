using System;

namespace ValidationRelations.Attributes.Comparison
{
    /// <summary>
    /// 时间规则
    /// </summary>
    /// <example>
    /// </example>
    public class GreaterThanNowAttribute : PropertyValidationAttribute
    {
        protected override bool IsValidCore(object? value, object instance)
        {
            if (value == null)
            {
                return true;
            }

            if (value is DateTime dt)
            {
                return dt > DateTime.Now;
            }

            if (value is DateTimeOffset dto)
            {
                return dto > DateTimeOffset.Now;
            }

            return true;
        }
    }
}
