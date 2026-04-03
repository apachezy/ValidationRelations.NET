using ValidationRelations.Infrastructure;

namespace ValidationRelations.Attributes.Comparison
{
    /// <summary>
    /// 验证属性值必须是另一个属性值的整数倍。
    /// </summary>
    /// <example>
    /// public class Order
    /// {
    ///     public int PackSize { get; set; }
    ///
    ///     [MultipleOf(nameof(PackSize))]
    ///     public int Quantity { get; set; }
    /// }
    /// </example>
    public sealed class MultipleOfAttribute : PropertyValidationAttribute
    {
        /// <summary>
        /// 作为除数的属性名称。
        /// </summary>
        public string OtherProperty { get; }

        /// <summary>
        /// 初始化 <see cref="MultipleOfAttribute"/>。
        /// </summary>
        /// <param name="otherProperty">作为除数的属性名称，支持嵌套属性路径。</param>
        public MultipleOfAttribute(string otherProperty)
        {
            OtherProperty = otherProperty;
        }

        protected override bool IsValidCore(object? value, object instance)
        {
            if (value == null)
            {
                return true;
            }

            var otherValue = PropertyValueResolver.GetValue(instance, OtherProperty);

            if (!ArithmeticHelper.TryToDecimal(value, out var dividend))
            {
                return true;
            }

            if (!ArithmeticHelper.TryToDecimal(otherValue, out var divisor))
            {
                return true;
            }

            if (divisor == 0m)
            {
                return true;
            }

            return dividend % divisor == 0m;
        }
    }
}
