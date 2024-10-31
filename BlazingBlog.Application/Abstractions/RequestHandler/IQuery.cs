using MediatR;

namespace BlazorCleanArchitecture.Application.Abstractions.RequestHandler
{
    // CQRS QUERY PATTERN
    // The IQuery interface inherits from the IRequest interface, meaning it can be processed by a mediator.
    // Returns Result<TResponse> which wraps both success and failure outcomes for the query.
    public interface IQuery<TResponse> : IRequest<Result<TResponse>>
    {
    }
}
