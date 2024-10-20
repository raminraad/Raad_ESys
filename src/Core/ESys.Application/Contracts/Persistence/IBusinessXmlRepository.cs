using ESys.Domain.Entities;

namespace ESys.Application.Contracts.Persistence;
public interface IBusinessXmlRepository : IAsyncRepository<BusinessXml>
{
    public Dictionary<string, string> GetBusinessXmlAsDictionary(Business business,Dictionary<string, string> lookupDic);
    string GenerateQueryForLookup(Dictionary<string, string> lookupDic, BusinessXml businessXml);
}
