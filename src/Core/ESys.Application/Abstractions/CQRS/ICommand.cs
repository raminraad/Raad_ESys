namespace ESys.Application.Abstractions.CQRS;

public interface ICommandBase
{
}

public interface ICommand : ICommandBase
{
    
}

public interface ICommand<TResponse> : ICommandBase
{
    
}