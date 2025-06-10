using System.Data;
using ConsultersIT.Infra.Interfaces;
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace ConsultersIT.Infra.Data.Context;

public class NpgsqlConnectionFactory : IDbConnectionFactory
{
    private readonly string _connectionString;
    
    public NpgsqlConnectionFactory(IConfiguration config)
    {
        _connectionString = config.GetConnectionString("PostgresDb");
    }
    
    public IDbConnection CreateConnection() => new NpgsqlConnection(_connectionString);
}
