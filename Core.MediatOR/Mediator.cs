using Core.MediatOR.Contracts;
using Microsoft.Extensions.DependencyInjection;

namespace Core.MediatOR
{
    public class Mediator : IMediator
    {
        private readonly IServiceProvider _provider;

        public Mediator(IServiceProvider provider)
        {
            _provider = provider;
        }


        public async Task<TResponse> Send<TResponse>(IRequest<TResponse> request, CancellationToken cancellationToken)
        {
            if (request is null)
                throw new ArgumentNullException(nameof(request));

            var handlerType = typeof(IRequestHandler<,>).MakeGenericType(request.GetType(), typeof(TResponse));
            dynamic handler = _provider.GetRequiredService(handlerType);

            if(handler is null)
            {
                throw new InvalidOperationException($"No se encontro el handler para {request.GetType().Name}");
            }

            return await handler.Handle((dynamic)request, cancellationToken);
        }
    }
}
