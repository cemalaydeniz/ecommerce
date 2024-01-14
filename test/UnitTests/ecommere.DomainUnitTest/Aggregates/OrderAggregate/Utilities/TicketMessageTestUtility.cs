using ecommerce.Domain.Aggregates.OrderRepository.Entities;
using ecommerce.TestUtility;

namespace ecommerce.DomainUnitTest.Aggregates.OrderAggregate.Utilities
{
    public static class TicketMessageTestUtility
    {
        public static string ValidContent => "Message content";

        public static TicketMessage ValidTicketMessage => new TicketMessage(Guid.NewGuid(), ValidContent);

        #region Valid Values
        public static IEnumerable<object?[]> ValidContents()
        {
            yield return new object?[] { ValidContent };
        }

        public static IEnumerable<object?[]> ValidValues()
        {
            foreach (var values in ValueGenerator(ValidContents))
            {
                yield return values;
            }
        }
        #endregion

        #region Invalid Values
        public static IEnumerable<object?[]> InvalidContents()
        {
            yield return new object?[] { null };
            yield return new object?[] { string.Empty };
            yield return new object?[] { " " };
            yield return new object?[] { StringGenerator.GenerateRandomStringStrictLength(TicketMessage.ContentMinLength - 1) };
            yield return new object?[] { StringGenerator.GenerateRandomStringStrictLength(TicketMessage.ContentMaxLength + 1) };
        }

        public static IEnumerable<object?[]> InvalidValues()
        {
            foreach (var values in ValueGenerator(InvalidContents))
            {
                yield return values;
            }
        }
        #endregion

        private static IEnumerable<object?[]> ValueGenerator(Func<IEnumerable<object?[]>> contents)
        {
            foreach (var content in contents())
            {
                yield return new object?[]
                {
                    Guid.NewGuid(),
                    content[0]
                };
            }
        }
    }
}
