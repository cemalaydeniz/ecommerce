#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

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

        #region Behaviors
        private TicketMessage() { }

        /// <summary>
        /// Creates a ticket message locally
        /// </summary>
        /// <param name="userId">The user who sends the message</param>
        /// <param name="content">The content of the message</param>
        /// <exception cref="ArgumentNullException"></exception>
        /// <exception cref="CharLengthOutofRangeException"></exception>
        public TicketMessage(Guid userId,
            string content)
        {
            ValidateContent(content);

            Id = Guid.NewGuid();
            Content = content;

            UserId = userId;
        }
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
