using System.Data;
using ConsultersIT.Infra.Interfaces;

namespace ConsultersIT.Infra.Data.Context;

public abstract class BaseRepository
{
    private readonly IDbConnectionFactory _connectionFactory;
    
    protected BaseRepository(IDbConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }
    
    protected async Task<T> WithConnection<T>(Func<IDbConnection, Task<T>> func)
    {
        using var connection = _connectionFactory.CreateConnection();
        return await func(connection);
    }
}