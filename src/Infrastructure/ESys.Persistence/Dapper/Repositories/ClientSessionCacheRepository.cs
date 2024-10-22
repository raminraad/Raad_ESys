using System.Data.SqlClient;
using System.Numerics;
using Dapper;
using ESys.Application.Abstractions.Persistence;
using ESys.Domain.Entities;
using ESys.Domain.Exceptions;
using ESys.Persistence.Static;
using Microsoft.Extensions.Configuration;

namespace ESys.Persistence.Dapper.Repositories;

public class ClientSessionCacheRepository : IClientSessionCacheRepository
{
    protected readonly string _connectionString = string.Empty;

    public ClientSessionCacheRepository(IConfiguration configuration)
    {
        _connectionString =
            configuration.GetConnectionString(SqlServerStatics.ConnectionStrings.BusinessConnectionStringName) ??
            throw new ArgumentNullException();
    }

    public Task<ClientSessionCache> GetById(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<ClientSessionCache?> GetByIdAsync(uint id)
    {
        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            string queryStatement = $"""
                                     SELECT TOP 1 *  FROM 
                                     {SqlServerStatics.Tables.TblClientSessionCache.TableName} 
                                                     WHERE 
                                                     {SqlServerStatics.Tables.TblClientSessionCache.ClientSessionCacheId}={id}
                                     """;
            return await connection.QueryFirstOrDefaultAsync<ClientSessionCache>(queryStatement);
        }
    }
    
    public async Task<ClientSessionCache?> GetByTempRoute(Guid tempRoute)
    {
        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            string queryStatement = $"""
                                     SELECT TOP 1 *  FROM 
                                     {SqlServerStatics.Tables.TblClientSessionCache.TableName} 
                                                     WHERE 
                                                     {SqlServerStatics.Tables.TblClientSessionCache.TempRoute}='{tempRoute}'
                                     """;
            return await connection.QueryFirstOrDefaultAsync<ClientSessionCache>(queryStatement);
        }
    }

    public Task<IReadOnlyList<ClientSessionCache>> ListAll()
    {
        throw new NotImplementedException();
    }

    public async Task<ClientSessionCache> Add(ClientSessionCache entity)
    {
        try
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                var newInsertedId = connection.QuerySingle<int>(
                    $"""
                     INSERT INTO {SqlServerStatics.Tables.TblClientSessionCache.TableName} 
                         ({SqlServerStatics.Tables.TblClientSessionCache.BusinessId},
                          {SqlServerStatics.Tables.TblClientSessionCache.ClientToken},
                          {SqlServerStatics.Tables.TblClientSessionCache.TempRoute})
                     VALUES 
                         (@{SqlServerStatics.Tables.TblClientSessionCache.BusinessId},
                          @{SqlServerStatics.Tables.TblClientSessionCache.ClientToken},
                          @{SqlServerStatics.Tables.TblClientSessionCache.TempRoute})
                     SELECT CAST(SCOPE_IDENTITY() AS INT);
                     """,
                    entity
                );
                return entity;
            }
        }
        catch (Exception e)
        {
            throw new DbException(e.Message);
        }
    }

    public Task Update(ClientSessionCache entity)
    {
        throw new NotImplementedException();
    }

    public Task Delete(ClientSessionCache entity)
    {
        throw new NotImplementedException();
    }
}