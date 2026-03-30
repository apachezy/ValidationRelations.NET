using System;
using System.Collections.Concurrent;
using System.Reflection;

namespace ValidationRelations.Infrastructure
{
    [Obsolete("使用PropertyGetterCache")]
    internal static class PropertyAccessorCache
    {
        private static readonly ConcurrentDictionary<(Type, string), PropertyInfo?> Cache = new();

        public static PropertyInfo? GetProperty(Type type, string name)
        {
            return Cache.GetOrAdd(
                (type, name),
                key => key.Item1.GetProperty(
                    key.Item2,
                    BindingFlags.Instance |
                    BindingFlags.Public |
                    BindingFlags.NonPublic
                )
            );
        }
    }
}