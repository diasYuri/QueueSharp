using System.ComponentModel.Design;
using System.Data;
using System.Data.SqlClient;
using Dapper;
using Microsoft.Extensions.DependencyInjection;
using QueueSharp.Application.Adapters;
using QueueSharp.Application.Model;

namespace QueueSharp.SqlServerAdapter.Adapter;

public class SqlServerPersistenceAdapter : IPersistenceAdapter
{
    private readonly string? _connectionString;
    public SqlServerPersistenceAdapter(string? connectionString)
    {
        _connectionString = connectionString;
    }
    public async Task Enqueue(string topic, QModel model)
    {
        var data = new
        {
            Topic = topic,
            Status = model.Status,
            Payload = model.Payload
        };
        await ExecuteAsProcedure("PQueueSharp_enqueue", data);
    }

    public async Task<QModel?> Dequeue(string topic)
    {
        var data = new
        {
            Topic = topic
        };
        return await QueryAsProcedure<QModel?>("PQueueSharp_dequeue", data);
    }

    private async Task ExecuteAsProcedure(string procedureName, object data)
    {
        await using var connection = new SqlConnection(_connectionString);
        connection.Open();
        var result = await connection.ExecuteAsync(procedureName, data, commandType: CommandType.StoredProcedure);
    }
    
    private async Task<TR> QueryAsProcedure<TR>(string procedureName, object? data)
    {
        await using var connection = new SqlConnection(_connectionString);
        connection.Open();
        return await connection.QueryFirstOrDefaultAsync<TR>(procedureName, data, commandType: CommandType.StoredProcedure);
    }
}

public static class SqlServerExtensions
{
    public static IServiceCollection AddSqlServer(this IServiceCollection services, string? connectionString)
    {
        return services.AddSingleton<IPersistenceAdapter, SqlServerPersistenceAdapter>(s =>
        {
            return new SqlServerPersistenceAdapter(connectionString);
        });
    }
}