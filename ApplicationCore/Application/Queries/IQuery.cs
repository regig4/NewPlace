using MediatR;

namespace ApplicationCore.Application.Queries
{
    public interface IQuery<T> : IRequest<T>
    {
    }
}
