using Microsoft.Extensions.Configuration;
using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsultersIT.Infra.Data.Context
{
    public class DBContext
    {
        private readonly IConfiguration _configuration;

        private readonly string connectionStrig;

        public DBContext(IConfiguration configuration)
        {
            _configuration = configuration;
            this.connectionStrig = this._configuration.GetConnectionString("PostgresDb");
        }
        public IDbConnection CreateConnection() => new NpgsqlConnection(connectionStrig);
    }
}