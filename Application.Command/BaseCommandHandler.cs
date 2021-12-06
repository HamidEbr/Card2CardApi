using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Command
{
    public abstract class BaseCommand<TResponse> : IRequest<TResponse>
    {

    }

    public abstract class BaseCommandHandler<TRequest, TResponse>
        : IRequestHandler<TRequest, TResponse> where TRequest : IRequest<TResponse>
    {
        public abstract Task<TResponse> Handle(TRequest request, CancellationToken cancellationToken);
    }
}
