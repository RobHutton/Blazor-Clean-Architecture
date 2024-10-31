using MediatR;

namespace BlazorCleanArchitecture.Application.Abstractions.RequestHandler
{
    // CQRS QUERY PATTERN
    // Query Handler responsible for handling queries of type IQuery<TResponse>
    // Extends from IRequestHandler
    // Responsible for processing a query (TQuery) and returning the corresponding result (Result<TResponse>)
    // where TQuery : IQuery<TResponse> ensures that TQuery is a valid query (i.e., it must implement IQuery<TResponse>)
    public interface IQueryHandler<TQuery, TResponse> : IRequestHandler<TQuery, Result<TResponse>>
        where TQuery : IQuery<TResponse>
    {
    }
}
