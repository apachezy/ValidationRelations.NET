using System;

namespace ValidationRelations.Attributes.Time
{
    /// <summary>
    /// 
    /// </summary>
    /// <example>
    /// [LessThanNow]
    /// public DateTime CreatedTime { get; set; }
    /// </example>
    public sealed class LessThanNowAttribute : PropertyValidationAttribute
    {
        protected override bool IsValidCore(object? value, object instance)
        {
            if (value == null)
            {
                return true;
            }

            if (value is DateTime dt)
            {
                return dt < DateTime.Now;
            }

            if (value is DateTimeOffset dto)
            {
                return dto < DateTimeOffset.Now;
            }

            return true;
        }
    }
}
