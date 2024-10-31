using MediatR;

namespace BlazorCleanArchitecture.Application.Abstractions.RequestHandler
{
    // CQRS Pattern
    // Command Handler for commands that do not return any specific result other than success or failure
    public interface ICommandHandler<TCommand> : IRequestHandler<TCommand, Result>
        where TCommand : ICommand
    {
    }
    // CQRS Pattern
    // Command Handler for commands that do return a specific response type
    public interface ICommandHandler<TCommand, TResponse> : IRequestHandler<TCommand, Result<TResponse>>
        where TCommand : ICommand<TResponse>
    {
    }
}
