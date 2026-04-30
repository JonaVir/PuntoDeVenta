using FluentValidation;
using MediatR;

namespace BospOne.PuntoDeVenta.Application.Core
{
    public sealed class ValidationBehavior<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : ICommandBase
    {
        #region Fields and properties
        private readonly IEnumerable<IValidator<TRequest>> Validators;
        #endregion

        #region Builders
        public ValidationBehavior(IEnumerable<IValidator<TRequest>> validators)
        {
            Validators = validators;
        }
        #endregion

        #region Methods
        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            var context = new ValidationContext<TRequest>(request);

            var validationFailures = await Task.WhenAll(Validators.Select(validator => validator.ValidateAsync(context)));

            var errors = validationFailures
                .Where(validation => !validation.IsValid)
                .SelectMany(validation => validation.Errors)
                .Select(validation => new ValidationError(
                    validation.PropertyName,
                    validation.ErrorMessage
                )).ToList();

            if (errors.Any())
            {
                throw new ValidationException(errors);
            }

            return await next();
        }
        #endregion
    }
}
