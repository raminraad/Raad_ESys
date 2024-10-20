using MediatR;

namespace ESys.Application.Abstractions.CQRS;

public interface ICommandBase : IRequest
{
}

public interface ICommand : ICommandBase
{
    
}

public interface ICommand<TResponse> : ICommandBase
{
    
}