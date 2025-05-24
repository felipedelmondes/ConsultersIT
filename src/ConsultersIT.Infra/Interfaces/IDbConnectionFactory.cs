using System.Data;

namespace ConsultersIT.Infra.Interfaces;

public interface IDbConnectionFactory
{
    IDbConnection CreateConnection();
}