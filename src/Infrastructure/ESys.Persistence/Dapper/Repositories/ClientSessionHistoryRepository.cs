using System.Data.SqlClient;
using Dapper;
using ESys.Application.Abstractions.Persistence;
using ESys.Domain.Entities;
using ESys.Domain.Exceptions;
using ESys.Persistence.Static;
using Microsoft.Extensions.Configuration;

namespace ESys.Persistence.Dapper.Repositories;

public class ClientSessionHistoryRepository : IClientSessionHistoryRepository
{
    protected readonly string _connectionString = string.Empty;

    public ClientSessionHistoryRepository(IConfiguration configuration)
    {
        _connectionString =
            configuration.GetConnectionString(SqlServerStatics.ConnectionStrings.BusinessConnectionStringName) ??
            throw new ArgumentNullException();
    }

    public Task<ClientSessionHistory> GetById(int id)
    {
        throw new NotImplementedException();
    }

    public async Task<ClientSessionHistory?> GetByIdAsync(uint id)
    {
        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            string queryStatement = $"""
                                     SELECT TOP 1 *  FROM 
                                     {SqlServerStatics.Tables.TblClientSessionHistory.TableName} 
                                                     WHERE 
                                                     {SqlServerStatics.Tables.TblClientSessionHistory.ClientSessionHistoryId}={id}
                                     """;
            return await connection.QueryFirstOrDefaultAsync<ClientSessionHistory>(queryStatement);
        }
    }
    
    public async Task<ClientSessionHistory?> GetByTempRoute(Guid tempRoute)
    {
        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            string queryStatement = $"""
                                     SELECT TOP 1 *  FROM 
                                     {SqlServerStatics.Tables.TblClientSessionHistory.TableName} 
                                                     WHERE 
                                                     {SqlServerStatics.Tables.TblClientSessionHistory.TempRoute}='{tempRoute}'
                                     """;
            return await connection.QueryFirstOrDefaultAsync<ClientSessionHistory>(queryStatement);
        }
    }

    public Task<IReadOnlyList<ClientSessionHistory>> ListAll()
    {
        throw new NotImplementedException();
    }

    public async Task<ClientSessionHistory> Add(ClientSessionHistory entity)
    {
        try
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                await connection.OpenAsync();

                var newInsertedId = connection.QuerySingle<int>(
                    $"""
                     INSERT INTO {SqlServerStatics.Tables.TblClientSessionHistory.TableName} 
                         ({SqlServerStatics.Tables.TblClientSessionHistory.BusinessId},
                          {SqlServerStatics.Tables.TblClientSessionHistory.ClientToken},
                          {SqlServerStatics.Tables.TblClientSessionHistory.TempRoute},
                          {SqlServerStatics.Tables.TblClientSessionHistory.SubmissionInput},
                          {SqlServerStatics.Tables.TblClientSessionHistory.SubmissionOutput})
                     VALUES 
                         (@{SqlServerStatics.Tables.TblClientSessionHistory.BusinessId},
                          @{SqlServerStatics.Tables.TblClientSessionHistory.ClientToken},
                          @{SqlServerStatics.Tables.TblClientSessionHistory.TempRoute},
                          @{SqlServerStatics.Tables.TblClientSessionHistory.SubmissionInput},
                          @{SqlServerStatics.Tables.TblClientSessionHistory.SubmissionOutput})
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

    public Task Update(ClientSessionHistory entity)
    {
        throw new NotImplementedException();
    }

    public async Task<int> Delete(ClientSessionHistory entity)
    {
        using (SqlConnection connection = new SqlConnection(_connectionString))
        {
            string queryStatement = $"""
                                     DELETE  FROM 
                                     {SqlServerStatics.Tables.TblClientSessionHistory.TableName} 
                                                     WHERE 
                                                     {SqlServerStatics.Tables.TblClientSessionHistory.ClientSessionHistoryId}={entity.ClientSessionHistoryId}
                                     """;
            return await connection.ExecuteAsync(queryStatement);
        }
    }
}