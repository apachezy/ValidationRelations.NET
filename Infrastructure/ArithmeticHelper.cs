using System;
using System.Globalization;

namespace ValidationRelations.Infrastructure
{
    /// <summary>
    /// 数值运算帮助类，用于多属性聚合计算。
    /// </summary>
    internal static class ArithmeticHelper
    {
        /// <summary>
        /// 尝试将值转换为 <see cref="decimal"/>。
        /// </summary>
        public static bool TryToDecimal(object? value, out decimal result)
        {
            result = 0m;

            if (value == null)
            {
                return false;
            }

            var type = value.GetType();
            type = Nullable.GetUnderlyingType(type) ?? type;

            if (!ConstantComparisonHelper.IsNumericType(type))
            {
                return false;
            }

            try
            {
                result = Convert.ToDecimal(value, CultureInfo.InvariantCulture);
                return true;
            }
            catch
            {
                return false;
            }
        }

        /// <summary>
        /// 对一组值执行指定的聚合运算。
        /// </summary>
        public static bool TryAggregate(object?[] values, AggregateFunction function, out decimal result)
        {
            result = 0m;

            if (values.Length == 0)
            {
                return false;
            }

            var decimals = new decimal[values.Length];

            for (var i = 0; i < values.Length; i++)
            {
                if (!TryToDecimal(values[i], out decimals[i]))
                {
                    return false;
                }
            }

            switch (function)
            {
                case AggregateFunction.Sum:
                    var sum = 0m;
                    foreach (var d in decimals) sum += d;
                    result = sum;
                    return true;

                case AggregateFunction.Product:
                    var product = 1m;
                    foreach (var d in decimals) product *= d;
                    result = product;
                    return true;

                case AggregateFunction.Min:
                    var min = decimals[0];
                    for (var i = 1; i < decimals.Length; i++)
                    {
                        if (decimals[i] < min) min = decimals[i];
                    }
                    result = min;
                    return true;

                case AggregateFunction.Max:
                    var max = decimals[0];
                    for (var i = 1; i < decimals.Length; i++)
                    {
                        if (decimals[i] > max) max = decimals[i];
                    }
                    result = max;
                    return true;

                case AggregateFunction.Average:
                    var total = 0m;
                    foreach (var d in decimals) total += d;
                    result = total / decimals.Length;
                    return true;

                default:
                    return false;
            }
        }
    }
}
