using MediatR;

namespace API.Queries
{
    public interface IQuery<T> : IRequest<T>
    {
    }
}
