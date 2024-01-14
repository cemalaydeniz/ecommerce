namespace ecommerce.API.Utilities.Json
{
    public static partial class JsonUtility
    {
        public static object Success(string? message = null, int? code = null, object? metaData = null)
        {
            return new
            {
                success = true,
                code = code,
                message = message,
                metaData = metaData
            };
        }
    }
}
