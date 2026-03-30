using System;
using System.Collections.Concurrent;
using System.Linq.Expressions;
using System.Reflection;

namespace ValidationRelations.Infrastructure
{
    // 已编译表达式缓存
    // 特点：
    // 只在第一次访问属性时编译表达式
    // 后续访问直接调用委托
    // 无反射开销
    //
    // 支持：
    // 单属性 StartTime
    // 嵌套属性 Order.CreateTime
    // 任意层级 A.B.C.D
    internal static class PropertyGetterCache
    {
        private static readonly ConcurrentDictionary<(Type, string), Func<object, object?>> Cache = new();

        public static Func<object, object?> GetGetter(Type type, string propertyPath)
        {
            return Cache.GetOrAdd((type, propertyPath), key =>
            {
                return CreateGetter(key.Item1, key.Item2);
            });
        }

        private static Func<object, object?> CreateGetter(Type rootType, string propertyPath)
        {
            var parameter = Expression.Parameter(typeof(object), "instance");

            Expression current = Expression.Convert(parameter, rootType);
            var currentType = rootType;

            var parts = propertyPath.Split('.');

            foreach (var part in parts)
            {
                var property = currentType.GetProperty(
                    part,
                    BindingFlags.Instance |
                    BindingFlags.Public |
                    BindingFlags.NonPublic);

                if (property == null)
                {
                    throw new InvalidOperationException(
                        $"Property '{part}' not found on type '{currentType.Name}'.");
                }

                current = BuildSafePropertyAccess(current, property);

                currentType = property.PropertyType;
            }

            var convertResult = Expression.Convert(current, typeof(object));

            var lambda = Expression.Lambda<Func<object, object?>>(convertResult, parameter);

            return lambda.Compile();
        }

        private static Expression BuildSafePropertyAccess(Expression instance, PropertyInfo property)
        {
            var propertyAccess = Expression.Property(instance, property);

            if (!IsNullable(instance.Type))
            {
                return propertyAccess;
            }

            var nullConstant = Expression.Constant(null, propertyAccess.Type);

            return Expression.Condition(
                Expression.Equal(instance, Expression.Constant(null, instance.Type)),
                nullConstant,
                propertyAccess);
        }

        private static bool IsNullable(Type type)
        {
            if (!type.IsValueType)
            {
                return true;
            }

            return Nullable.GetUnderlyingType(type) != null;
        }
    }
}