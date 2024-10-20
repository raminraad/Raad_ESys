using ESys.Domain.Entities;

namespace ESys.Application.Contracts.Persistence;
public interface IBusinessRepository : IAsyncRepository<Business>
{
    Task<Business> GetBusinessWithXmls(string businessId);
}
