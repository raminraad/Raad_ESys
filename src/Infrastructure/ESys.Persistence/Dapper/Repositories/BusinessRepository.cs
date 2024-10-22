using System.Data.SqlClient;
using System.Numerics;
using Dapper;
using ESys.Application.Abstractions.Persistence;
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
        _connectionString =
            configuration.GetConnectionString(SqlServerStatics.ConnectionStrings.BusinessConnectionStringName) ??
            throw new ArgumentNullException();
    }

    public Task<Business> Add(Business entity)
    {
        throw new NotImplementedException();
    }

    public Task Delete(Business entity)
    {
        throw new NotImplementedException();
    }

    public Task<Business> GetBusinessWithXmls(string businessId)
    {
        throw new NotImplementedException();
    }

    public Task<Business> GetByIdAsync(BigInteger id)
    {
        throw new NotImplementedException();
    }

    public async Task<Business> GetById(int id)
    {
        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            string queryStatement = $"""
                                     SELECT TOP 1 *  FROM 
                                     {SqlServerStatics.Tables.TblBusiness.TableName} 
                                                     WHERE 
                                                     {SqlServerStatics.Tables.TblBusiness.BusinessId}='{id}'
                                     """;
            return await connection.QueryFirstOrDefaultAsync<Business>(queryStatement);
        }
    }

    public async Task<IReadOnlyList<Business>> ListAll()
    {
        using (var connection = new SqlConnection(_connectionString))
        {
            await connection.OpenAsync();
            var businesses =
                await connection.QueryAsync<Business>($"SELECT * FROM {SqlServerStatics.Tables.TblBusiness.TableName}");
            return businesses.ToList();
        }
    }

    public Task Update(Business entity)
    {
        throw new NotImplementedException();
    }
}