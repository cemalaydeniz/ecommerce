namespace ecommerce.API.Utilities
{
    public static class CookieUtility
    {
        /// <summary>
        /// Adds new key-value pair to the cookie
        /// </summary>
        /// <param name="response">The response object of the request</param>
        /// <param name="key">The key of the pair</param>
        /// <param name="value">The value of the pair</param>
        /// <param name="expirationDate">The expiration date of the cookie</param>
        /// <param name="isHttpOnly">If the cookie is Http only</param>
        /// <exception cref="ArgumentNullException"></exception>
        public static void AddToCookie(HttpResponse response, string key, string value, DateTime expirationDate, bool isHttpOnly)
        {
            if (response == null)
                throw new ArgumentNullException(nameof(response));

            response.Cookies.Append(key, value, new CookieOptions()
            {
                HttpOnly = isHttpOnly,
                Expires = expirationDate
            });
        }
    }
}
