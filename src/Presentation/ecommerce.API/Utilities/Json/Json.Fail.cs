namespace ecommerce.API.Utilities.Json
{
    public static partial class JsonUtility
    {
        public static object Fail(object? errorDetails, int? code, object? metaData = null)
        {
            return new
            {
                success = false,
                code = code,
                error = errorDetails,
                metaData = metaData
            };
        }
    }
}
