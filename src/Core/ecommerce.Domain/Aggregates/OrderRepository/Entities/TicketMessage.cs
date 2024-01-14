using ecommerce.Domain.Common.Exceptions;
using ecommerce.Domain.SeedWork;

namespace ecommerce.Domain.Aggregates.OrderRepository.Entities
{
    public class TicketMessage : BaseEntity<Guid>
    {
        #region Properties
        public string Content { get; private set; }
        public DateTime CreatedAt { get; private set; }

        // Navigations
        public Guid UserId { get; private set; }
        #endregion

        #region Validations
        public static readonly int ContentMinLength = 10;
        public static readonly int ContentMaxLength = 300;

        private void ValidateContent(string content)
        {
            if (string.IsNullOrWhiteSpace(content))
                throw new ArgumentNullException(nameof(content));
            if (content.Length < ContentMinLength || content.Length > ContentMaxLength)
                throw new CharLengthOutofRangeException(nameof(content), ContentMinLength, ContentMaxLength);
        }
        #endregion
    }
}
