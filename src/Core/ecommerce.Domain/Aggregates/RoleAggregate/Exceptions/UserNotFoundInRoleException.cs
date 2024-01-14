namespace ecommerce.Domain.Aggregates.RoleAggregate.Exceptions
{
    public class UserNotFoundInRoleException : Exception
    {
        public UserNotFoundInRoleException() : base() { }
        public UserNotFoundInRoleException(string message) : base(message) { }
    }
}
