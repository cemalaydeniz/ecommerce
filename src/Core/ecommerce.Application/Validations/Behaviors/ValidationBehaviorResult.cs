namespace ecommerce.Application.Validations.Behaviors
{
    /// <summary>
    /// Contains either the response of the handler or the error list of the validation process
    /// </summary>
    public class ValidationBehaviorResult<TResponse> : IValidationBehaviorResult
    {
        /// <inheritdoc/>
        public bool IsSuccess { get; private set; }

        /// <inheritdoc/>
        public IEnumerable<string>? Errors { get; private set; }

        /// <summary>
        /// The response of the handler if <see cref="IsSuccess"/> is true
        /// </summary>
        public TResponse? Response { get; private set; }

        private ValidationBehaviorResult(bool isSuccess, TResponse? response, IEnumerable<string>? errors)
        {
            IsSuccess = isSuccess;
            Response = response;
            Errors = errors;
        }

        public static ValidationBehaviorResult<TResponse> Success(TResponse response) => new(true, response, null);
        public static ValidationBehaviorResult<TResponse> Fail(IEnumerable<string> errors) => new(false, default, errors);
        public static ValidationBehaviorResult<TResponse> Fail(List<string> errors) => new(false, default, errors);
        public static ValidationBehaviorResult<TResponse> Fail(string[] errors) => new(false, default, errors);
        public static ValidationBehaviorResult<TResponse> Fail(string error) => new(false, default, new string[] { error });

        public static implicit operator ValidationBehaviorResult<TResponse>(TResponse value) => new(true, value, null);
        public static implicit operator ValidationBehaviorResult<TResponse>(List<string> value) => new(false, default, value);
        public static implicit operator ValidationBehaviorResult<TResponse>(string[] value) => new(false, default, value);
    }
}
