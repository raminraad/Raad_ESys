using System.Data.SqlClient;
using System.Numerics;
using Dapper;
using ESys.Application.Abstractions.Persistence;
using ESys.Domain.Entities;
using ESys.Persistence.Static;
using Microsoft.Extensions.Configuration;

namespace ESys.Persistence.Dapper.Repositories;

public class BusinessInitialUiRepository : IBusinessInitialUiRepository
{
    private readonly IConfiguration _configuration;
    protected readonly string _connectionString = string.Empty;

    public BusinessInitialUiRepository(IConfiguration configuration)
    {
        _configuration = configuration;
        _connectionString =
            configuration.GetConnectionString(SqlServerStatics.ConnectionStrings.BusinessConnectionStringName) ??
            throw new ArgumentNullException();
    }

    public async Task<BusinessInitialUI> Add(BusinessInitialUI entity)
    {
        throw new NotImplementedException();
    }

    public async Task<int> Delete(BusinessInitialUI entity)
    {
        throw new NotImplementedException();
    }

    public async Task<BusinessInitialUI> BusinessInitialUiressionWithXmls(string BusinessInitialUiressionId)
    {
        throw new NotImplementedException();
    }

    public async Task<BusinessInitialUI> GetById(int id)
    {
        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            string queryStatement =
                $"SELECT TOP 1 *  FROM {SqlServerStatics.Tables.TblBusinessInitialUi.TableName} WHERE {SqlServerStatics.Tables.TblBusinessInitialUi.BusinessId} = '{id}'";
            return await connection.QueryFirstOrDefaultAsync<BusinessInitialUI>(queryStatement);
        }    }

    public async Task Update(BusinessInitialUI entity)
    {
        throw new NotImplementedException();
    }

    public async Task<IReadOnlyList<BusinessInitialUI>> ListAll()
    {
        throw new NotImplementedException();
    }
}