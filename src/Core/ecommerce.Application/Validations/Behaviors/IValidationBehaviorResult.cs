namespace ecommerce.Application.Validations.Behaviors
{
    /// <summary>
    /// Declares that the class is a result of a validation
    /// </summary>
    public interface IValidationBehaviorResult
    {
        /// <summary>
        /// Whether the validation is successfull or not
        /// </summary>
        public bool IsSuccess { get; }

        /// <summary>
        /// The error list of the validation process if <see cref="IsSuccess"/> is false
        /// </summary>
        public IEnumerable<string>? Errors { get; }
    }
}
