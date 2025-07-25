using System;
using System.Data;
using Npgsql;

namespace Movies.Application.Database;

public interface IDbConnectionFactory
{
    Task<IDbConnection> CreateConnectionAsync(CancellationToken token = default );
}

public class NpgsqlConnectionFactory : IDbConnectionFactory
{
    private readonly string _connectionString;
    public NpgsqlConnectionFactory(string connectionString)
    {
        _connectionString = connectionString;
    }
    public async Task<IDbConnection> CreateConnectionAsync(CancellationToken token = default)
    {
        var connection = new NpgsqlConnection(_connectionString);
        await connection.OpenAsync();
        return connection;
    }
}
