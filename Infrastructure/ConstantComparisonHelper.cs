using System;
using System.Globalization;

namespace ValidationRelations.Infrastructure
{
    /// <summary>
    /// 常量值数值比较帮助类。
    /// </summary>
    internal static class ConstantComparisonHelper
    {
        public static bool TryCompare(object? left, object? right, out int result)
        {
            result = 0;

            if (left == null || right == null)
            {
                return false;
            }

            var leftType  = Nullable.GetUnderlyingType(left.GetType()) ?? left.GetType();
            var rightType = Nullable.GetUnderlyingType(right.GetType()) ?? right.GetType();

            if (!IsNumericType(leftType) || !IsNumericType(rightType))
            {
                return false;
            }

            try
            {
                result = CompareCore(left, right, leftType, rightType);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static bool Evaluate(int compareResult, ConstantComparisonOperator op)
        {
            return op switch
            {
                ConstantComparisonOperator.Equal       => compareResult == 0,
                ConstantComparisonOperator.NotEqual    => compareResult != 0,
                ConstantComparisonOperator.GreaterThan => compareResult > 0,
                ConstantComparisonOperator.LessThan    => compareResult < 0,
                _                                      => false
            };
        }

        public static bool IsNumericType(Type type)
        {
            return type == typeof(byte)
                || type == typeof(sbyte)
                || type == typeof(short)
                || type == typeof(ushort)
                || type == typeof(int)
                || type == typeof(uint)
                || type == typeof(long)
                || type == typeof(ulong)
                || type == typeof(float)
                || type == typeof(double)
                || type == typeof(decimal);
        }

        private static int CompareCore(object left, object right, Type leftType, Type rightType)
        {
            if (leftType == typeof(decimal) || rightType == typeof(decimal))
            {
                var leftValue  = Convert.ToDecimal(left,  CultureInfo.InvariantCulture);
                var rightValue = Convert.ToDecimal(right, CultureInfo.InvariantCulture);
                return leftValue.CompareTo(rightValue);
            }

            if (IsFloatingPointType(leftType) || IsFloatingPointType(rightType))
            {
                var leftValue  = Convert.ToDouble(left,  CultureInfo.InvariantCulture);
                var rightValue = Convert.ToDouble(right, CultureInfo.InvariantCulture);
                return leftValue.CompareTo(rightValue);
            }

            var leftInteger  = Convert.ToDecimal(left,  CultureInfo.InvariantCulture);
            var rightInteger = Convert.ToDecimal(right, CultureInfo.InvariantCulture);
            return leftInteger.CompareTo(rightInteger);
        }

        private static bool IsFloatingPointType(Type type)
        {
            return type == typeof(float) || type == typeof(double);
        }
    }
}