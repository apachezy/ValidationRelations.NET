using System;
using System.Collections.Concurrent;
using System.Linq.Expressions;

namespace ValidationRelations.Infrastructure
{
    internal static class ComparisonDelegateCache
    {
        private static readonly ConcurrentDictionary<(Type, Type), Func<object, object, int>?> Cache = new();

        public static Func<object, object, int>? GetComparer(Type leftType, Type rightType)
        {
            leftType  = Nullable.GetUnderlyingType(leftType) ?? leftType;
            rightType = Nullable.GetUnderlyingType(rightType) ?? rightType;

            return Cache.GetOrAdd((leftType, rightType), key => BuildComparer(key.Item1, key.Item2));
        }

        private static Func<object, object, int>? BuildComparer(Type leftType, Type rightType)
        {
            // 同类型且实现 IComparable
            if (leftType == rightType && typeof(IComparable).IsAssignableFrom(leftType))
            {
                var left  = Expression.Parameter(typeof(object), "l");
                var right = Expression.Parameter(typeof(object), "r");

                var lCast = Expression.Convert(left,  leftType);
                var rCast = Expression.Convert(right, rightType);

                var compareCall = Expression.Call(
                    Expression.Convert(lCast, typeof(IComparable)),
                    nameof(IComparable.CompareTo),
                    null,
                    Expression.Convert(rCast, typeof(object)));

                var lambda = Expression.Lambda<Func<object, object, int>>(compareCall, left, right);
                return lambda.Compile();
            }

            // 数值类型
            if (IsNumeric(leftType) && IsNumeric(rightType))
            {
                var left  = Expression.Parameter(typeof(object), "l");
                var right = Expression.Parameter(typeof(object), "r");

                var lCast = Expression.Convert(
                    Expression.Call(typeof(Convert), nameof(Convert.ToDecimal), null, left),
                    typeof(decimal));

                var rCast = Expression.Convert(
                    Expression.Call(typeof(Convert), nameof(Convert.ToDecimal), null, right),
                    typeof(decimal));

                var compareCall = Expression.Call(lCast, nameof(decimal.CompareTo), null, rCast);

                var lambda = Expression.Lambda<Func<object, object, int>>(compareCall, left, right);
                return lambda.Compile();
            }

            // Enum
            if (leftType.IsEnum && rightType.IsEnum)
            {
                var left  = Expression.Parameter(typeof(object), "l");
                var right = Expression.Parameter(typeof(object), "r");

                var lCast = Expression.Call(typeof(Convert), nameof(Convert.ToInt64), null, left);
                var rCast = Expression.Call(typeof(Convert), nameof(Convert.ToInt64), null, right);

                var compareCall = Expression.Call(lCast, nameof(long.CompareTo), null, rCast);

                var lambda = Expression.Lambda<Func<object, object, int>>(compareCall, left, right);
                return lambda.Compile();
            }

            return null;
        }

        private static bool IsNumeric(Type type)
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
    }
}