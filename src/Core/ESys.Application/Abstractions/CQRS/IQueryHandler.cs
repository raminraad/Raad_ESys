using CSharpFunctionalExtensions;

namespace ESys.Application.Abstractions.CQRS;

public interface IQueryHandler<in TQuery,TResponse> where TQuery : IQuery<TResponse>
{
    Task<Result<TResponse>> Handle(TQuery query, CancellationToken cancellationToken);
}