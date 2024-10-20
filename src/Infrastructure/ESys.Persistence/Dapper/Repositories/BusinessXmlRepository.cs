using System.Data.SqlClient;
using System.Numerics;
using Dapper;
using ESys.Application.Abstractions.Persistence;
using ESys.Domain.Entities;
using ESys.Persistence.Static;
using Microsoft.Extensions.Configuration;

namespace ESys.Persistence.Dapper.Repositories;

public class BusinessXmlRepository : IBusinessXmlRepository
{
    private readonly IConfiguration _configuration;
    protected readonly string _connectionString = string.Empty;

    public BusinessXmlRepository(IConfiguration configuration)
    {
        //todo: move cs to AsyncRepository
        _configuration = configuration;
        _connectionString = configuration.GetConnectionString("EsysSqlServerConnectionString") ??
                            throw new ArgumentNullException();
    }

    public Dictionary<string, string> RequestDBforXML(string bizId)
    {
        throw new NotImplementedException();
    }

    public Task<Exp> AddAsync(Exp entity)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(Exp entity)
    {
        throw new NotImplementedException();
    }

    public Task<Exp> GetExpressionWithXmls(string ExpressionId)
    {
        throw new NotImplementedException();
    }

    public Task<Exp> GetByIdAsync(string id)
    {
        throw new NotImplementedException();
    }

    public Task<Exp> GetByIdAsync(BigInteger id)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(Exp entity)
    {
        throw new NotImplementedException();
    }

    public Task<IReadOnlyList<Exp>> ListAllAsync()
    {
        throw new NotImplementedException();
    }

    Task<BusinessXml> IAsyncRepository<BusinessXml>.GetByIdAsync(string id)
    {
        throw new NotImplementedException();
    }

    Task<BusinessXml> IAsyncRepository<BusinessXml>.GetByIdAsync(BigInteger id)
    {
        throw new NotImplementedException();
    }

    Task<IReadOnlyList<BusinessXml>> IAsyncRepository<BusinessXml>.ListAllAsync()
    {
        throw new NotImplementedException();
    }

    public Task<BusinessXml> AddAsync(BusinessXml entity)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(BusinessXml entity)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(BusinessXml entity)
    {
        throw new NotImplementedException();
    }

    public Dictionary<string, string> GetBusinessXmlAsDictionary(Business business, Dictionary<string, string> lookupStr)
    {
        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            BusinessXml? businessXml;
            string tableQuery =
                $"SELECT * FROM {SqlServerStatics.BusinessXmlTable} WHERE BizId = '{business.BizId}' and tname = '{lookupStr["table"]}'"; 
            businessXml = connection.QueryFirstOrDefault<BusinessXml>(tableQuery);

            //Q? This line was in original code. Ask what it is for.
            //string xmlColCount = "1";

            string innerQuery = GenerateQueryForLookup(lookupStr, businessXml);
            var innerQueryResult = connection.Query(innerQuery).FirstOrDefault();
            Dictionary<string, string> finalResult = new();
            if (innerQueryResult is not null)
            {
                foreach (var property in innerQueryResult)
                {
                    finalResult.Add(property.Key, property.Value.ToString());
                }
            }

            return finalResult;
        }
    }

    public string GenerateQueryForLookup(Dictionary<string, string> lookupDic, BusinessXml businessXml)
    {
        string query = @"SELECT * FROM ( SELECT" + businessXml.XmlTitles + " FROM(select * FROM " +
                       SqlServerStatics.BusinessXmlTable + " WHERE BizId = '" + businessXml.BizId + "' AND tname = '" +
                       lookupDic["table"] + "') e OUTER APPLY e.xml.nodes('" + businessXml.XmlTags + "') as X(Y) )T ";

        lookupDic.Remove("table");

        if (lookupDic.Count() > 0)
        {
            if (businessXml.WhereClause != null)
            {
                string tmpWhere = businessXml.WhereClause;
                foreach (var item in lookupDic)
                {
                    tmpWhere = tmpWhere.Replace("__" + item.Key, item.Value);
                }

                query += tmpWhere;
            }
        }

        return query;
    }
}