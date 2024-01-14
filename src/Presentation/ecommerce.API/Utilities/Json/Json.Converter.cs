using System.Text.Json;

namespace ecommerce.API.Utilities.Json
{
    public static partial class JsonUtility
    {
        public static string Stringify(object json)
        {
            if (json == null)
                return string.Empty;

            return JsonSerializer.Serialize(json);
        }
    }
}
