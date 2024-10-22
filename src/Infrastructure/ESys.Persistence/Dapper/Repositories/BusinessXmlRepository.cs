using System.Data.SqlClient;
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
        _connectionString =
            configuration.GetConnectionString(SqlServerStatics.ConnectionStrings.BusinessConnectionStringName) ??
            throw new ArgumentNullException();
    }

    public Dictionary<string, string> GetBusinessXmlAsDictionary(Business business,
        Dictionary<string, string> lookupStr)
    {
        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            BusinessXml? businessXml;
            string tableQuery =
                $"SELECT * FROM {SqlServerStatics.Tables.TblBusinessXml.TableName} WHERE {SqlServerStatics.Tables.TblBusinessXml.BusinessId} = '{business.BusinessId}' and {SqlServerStatics.Tables.TblBusinessXml.TName} = '{lookupStr["table"]}'";
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
        string query =
            $"SELECT * FROM ( SELECT{businessXml.XmlTitles} FROM(select * FROM {SqlServerStatics.Tables.TblBusinessXml.TableName} WHERE {SqlServerStatics.Tables.TblBusinessXml.BusinessId} = '{businessXml.BusinessId}' AND {SqlServerStatics.Tables.TblBusinessXml.TName} = '{lookupDic["table"]}') e OUTER APPLY e.xml.nodes('{businessXml.XmlTags}') as X(Y) )T ";

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


    public Task<BusinessXml> GetById(int id)
    {
        throw new NotImplementedException();
    }

    public Task<IReadOnlyList<BusinessXml>> ListAll()
    {
        throw new NotImplementedException();
    }

    public Task<BusinessXml> Add(BusinessXml entity)
    {
        throw new NotImplementedException();
    }

    public Task Update(BusinessXml entity)
    {
        throw new NotImplementedException();
    }

    public Task Delete(BusinessXml entity)
    {
        throw new NotImplementedException();
    }
}