namespace ecommerce.API.Utilities.Json
{
    public static partial class JsonUtility
    {
        public static object Payload(object payload, string? message = null, int? code = null, object? metaData = null)
        {
            return new
            {
                success = true,
                code = code,
                message = message,
                payload = payload,
                metaData = metaData
            };
        }
    }
}
