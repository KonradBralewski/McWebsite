using ErrorOr;
using FluentValidation;
using MediatR;

namespace McWebsite.Application.Common.Validation
{
    internal sealed class ValidateBehavior<TRequest, TResponse> :
        IPipelineBehavior<TRequest, TResponse> where TRequest : IRequest<TResponse>

    {
        private readonly IValidator<TRequest>? _validator;

        public ValidateBehavior(IValidator<TRequest>? validator = null)
        {
            _validator = validator;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            if (_validator is null)
            {
                return await next();
            }

            var validationResult = await _validator.ValidateAsync(request, cancellationToken);

            if (validationResult.IsValid)
            {
                return await next();
            }

            var errorList = validationResult.Errors.Select(valFailure => Error.Validation(valFailure.PropertyName, valFailure.ErrorMessage))
                .ToList();

            return (dynamic)errorList; // dynamic cast is dangerous but it will always come down to List<Error> object, no runtime exception should be thrown.
        }
    }
}
