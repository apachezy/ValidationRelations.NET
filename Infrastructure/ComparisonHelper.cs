namespace ValidationRelations.Infrastructure
{
    // 类型比较帮助类
    // 这个 helper 解决几个坑：
    // 
    // Nullable<int> vs int
    //     DateTime vs DateTime
    // Enum
    // decimal / double
    // 
    // 跨类型数值比较：
    // int    vs long
    // int    vs decimal
    // double vs decimal
    //
    // Enum 比较：
    // Status.Active > Status.Disabled
    //
    // Nullable 自动拆箱：
    // int? vs int
    // DateTime? vs DateTime
    internal static class ComparisonHelper
    {
        public static bool TryCompare(object? left, object? right, out int result)
        {
            result = 0;

            if (left == null || right == null)
            {
                return false;
            }

            var comparer = ComparisonDelegateCache.GetComparer(left.GetType(), right.GetType());

            if (comparer == null)
            {
                return false;
            }

            result = comparer(left, right);

            return true;
        }

        public static bool Evaluate(int compareResult, ComparisonOperator op)
        {
            return op switch
            {
                ComparisonOperator.Equal          => compareResult == 0,
                ComparisonOperator.NotEqual       => compareResult != 0,
                ComparisonOperator.GreaterThan    => compareResult > 0,
                ComparisonOperator.GreaterOrEqual => compareResult >= 0,
                ComparisonOperator.LessThan       => compareResult < 0,
                ComparisonOperator.LessOrEqual    => compareResult <= 0,
                _                                 => false
            };
        }
    }
}