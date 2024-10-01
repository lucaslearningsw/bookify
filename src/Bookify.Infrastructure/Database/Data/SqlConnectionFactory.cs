using Bookify.Application.Abstractions.Data;
using Microsoft.Data.SqlClient;
using System.Data;

namespace Bookify.Infrastructure.Database.Data;

internal class SqlConnectionFactory : ISqlConnectionFactory
{
    private readonly string _connectionString;

    public SqlConnectionFactory(string connectionString)
    {
        _connectionString = connectionString;
    }
    public IDbConnection CreateConnection()
    {
        var connection = new SqlConnection(_connectionString);
        connection.Open();

        return connection;
    }
}
