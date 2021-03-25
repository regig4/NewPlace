using MediatR;

namespace CatalogService.Application.Queries
{
    public interface IQuery<T> : IRequest<T>
    {
    }
}
