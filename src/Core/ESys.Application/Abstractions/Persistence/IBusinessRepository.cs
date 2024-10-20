using ESys.Domain.Entities;

namespace ESys.Application.Abstractions.Persistence;
public interface IBusinessRepository : IAsyncRepository<Business>
{
    Task<Business> GetBusinessWithXmls(string businessId);
}
