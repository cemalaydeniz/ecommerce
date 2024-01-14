using ecommerce.Domain.Aggregates.UserAggregate.Exceptions;
using ecommerce.Domain.SeedWork;

namespace ecommerce.Domain.Aggregates.UserAggregate.ValueObjects
{
    public class Token : ValueObject
    {
        public string? Value { get; private set; }
        public DateTime? ExpiresAt { get; private set; }

        private Token() { }

        /// <summary>
        /// Creates a new token locally
        /// </summary>
        /// <param name="value">The value of the token</param>
        /// <param name="expiresAt">The expiration date of the token</param>
        /// <exception cref="TokenExpiresInPastException"></exception>
        public Token(string? value, DateTime? expiresAt)
        {
            ValidateExpirationDate(expiresAt);

            Value = value;
            ExpiresAt = expiresAt;
        }

        public override IEnumerable<object> GetEqualityComponents()
        {
            yield return Value ?? string.Empty;
        }

        #region Validations
        private void ValidateExpirationDate(DateTime? expiresAt)
        {
            if (expiresAt == null)
                return;

            if (expiresAt <= DateTime.UtcNow)
                throw new TokenExpiresInPastException("The date in the past according to UTC-0");
        }
        #endregion
    }
}
