namespace ValidationRelations.Infrastructure
{
    internal static class PropertyValueResolver
    {
        public static object? GetValue(object instance, string propertyPath)
        {
            var getter = PropertyGetterCache.GetGetter(instance.GetType(), propertyPath);
            return getter(instance);
        }
    }
}
