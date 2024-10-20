using System.Data.SqlClient;
using System.Numerics;
using Dapper;
using ESys.Application.Contracts.Persistence;
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
        _connectionString = configuration.GetConnectionString("EsysSqlServerConnectionString") ?? throw new ArgumentNullException();
    }
    public async Task<BusinessInitialUI> AddAsync(BusinessInitialUI entity)
    {
        throw new NotImplementedException();
    }

    public async Task DeleteAsync(BusinessInitialUI entity)
    {
        throw new NotImplementedException();
    }

    public async Task<BusinessInitialUI> BusinessInitialUiressionWithXmls(string BusinessInitialUiressionId)
    {
        throw new NotImplementedException();
    }

    public async Task<BusinessInitialUI> GetByIdAsync(string id)
    {
        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            string queryStatement = "SELECT TOP 1 *  FROM " + SqlServerStatics.BusinessInitialUiTable + " WHERE BizId = '" + id + "'";
            return await connection.QueryFirstOrDefaultAsync<BusinessInitialUI>(queryStatement);
        }
    }

    public async Task<BusinessInitialUI> GetByIdAsync(BigInteger id)
    {
        throw new NotImplementedException();
    }

    public async Task UpdateAsync(BusinessInitialUI entity)
    {
        throw new NotImplementedException();
    }

    public async Task<IReadOnlyList<BusinessInitialUI>> ListAllAsync()
    {
        throw new NotImplementedException();
    }
}
