namespace ecommerce.Application.Utilities.Constants
{
    public static partial class ConstantsUtility
    {
        public static class OrderValidation
        {
            public static readonly string MessageContentRequired = "Message content is required";
            public static readonly string MessageContentLength_MinMax = $"Message content must be between {Domain.Aggregates.OrderAggregate.Entities.TicketMessage.ContentMinLength} and {Domain.Aggregates.OrderAggregate.Entities.TicketMessage.ContentMaxLength} characters";
        }
    }
}
