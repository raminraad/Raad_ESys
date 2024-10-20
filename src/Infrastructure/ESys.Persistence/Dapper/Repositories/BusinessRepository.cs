using System.Data.SqlClient;
using System.Numerics;
using Dapper;
using ESys.Application.Contracts.Persistence;
using ESys.Domain.Entities;
using ESys.Persistence.Static;
using Microsoft.Extensions.Configuration;

namespace ESys.Persistence.Dapper.Repositories;
public class BusinessRepository : IBusinessRepository
{

    private readonly IConfiguration _configuration;
    protected readonly string _connectionString = string.Empty;

    public BusinessRepository(IConfiguration configuration)
    {
        _configuration = configuration;
        _connectionString = configuration.GetConnectionString("EsysSqlServerConnectionString") ?? throw new ArgumentNullException();
    }
    public Task<Business> AddAsync(Business entity)
    {
        throw new NotImplementedException();
    }

    public Task DeleteAsync(Business entity)
    {
        throw new NotImplementedException();
    }

    public Task<Business> GetBusinessWithXmls(string businessId)
    {
        throw new NotImplementedException();
    }

    public Task<Business> GetByIdAsync(string id)
    {
        var idAsBigInteger = BigInteger.Parse(id);
        return GetByIdAsync(idAsBigInteger);
    }

    public async Task<Business> GetByIdAsync(BigInteger id)
    {
        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            string queryStatement = $"SELECT TOP 1 *  FROM {SqlServerStatics.BusinessTable} WHERE BizId = '{id}'";
            return await connection.QueryFirstOrDefaultAsync<Business>(queryStatement);
        }
    }

    public async Task<IReadOnlyList<Business>> ListAllAsync()
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();
            var businesses = await connection.QueryAsync<Business>($"SELECT * FROM {SqlServerStatics.BusinessTable}");
            return businesses.ToList();
        }
    }

    public Task UpdateAsync(Business entity)
    {
        throw new NotImplementedException();
    }
}
