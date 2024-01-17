namespace ecommerce.Domain.Common.Exceptions
{
    public class CharLengthOutofRangeException : Exception
    {
        public CharLengthOutofRangeException() : base() { }
        public CharLengthOutofRangeException(string message) : base(message) { }
        public CharLengthOutofRangeException(string nameofVariable, int maxLength) :
            base($"{nameofVariable} cannot be longer than {maxLength} characters")
        { }
        public CharLengthOutofRangeException(string nameofVariable, int minLength, int maxLength) :
            base($"{nameofVariable} must be between {minLength} and {maxLength} characters")
        { }
    }
}
