namespace ValidationRelations.Infrastructure
{
    internal static class RequiredHelper
    {
        public static bool HasValue(object? value)
        {
            if (value == null)
            {
                return false;
            }

            if (value is string s)
            {
                return !string.IsNullOrWhiteSpace(s);
            }

            return true;
        }
    }
}
