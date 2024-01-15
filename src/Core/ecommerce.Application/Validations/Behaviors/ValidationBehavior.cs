using FluentValidation;
using MediatR;

namespace ecommerce.Application.Validations.Behaviors
{
    public class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse>
        where TRequest : IRequest<TResponse>
        where TResponse : IValidationBehaviorResult
    {
        private readonly IEnumerable<IValidator<TRequest>>? _validators;

        public ValidationBehavior(IEnumerable<IValidator<TRequest>>? validators = null)
        {
            _validators = validators;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            if (_validators != null && _validators.Any())
            {
                var errors = _validators.Select(x => x.Validate(request))
                    .SelectMany(x => x.Errors)
                    .Where(x => x != null)
                    .ToList()
                    .ConvertAll(x => x.ErrorMessage);

                if (errors.Count != 0)
                    return (dynamic)errors;
            }

            return await next();
        }
    }
}
