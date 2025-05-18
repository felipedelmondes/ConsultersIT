using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ConsultersIT.Infra.Data.Context;
using ConsultersIT.Infra.Interfaces;
using Dapper;

namespace ConsultersIT.Infra.Repositories
{
    public class TesteRepository : ITesteRepository
    {
        public readonly DBContext _dbContext;

        public TesteRepository(DBContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<string> GetTesteAsync()
        {
            var query = @"SELECT version()";
            try
            {
                using (var conn = _dbContext.CreateConnection())
                {
                    var result = await conn.QueryAsync<string>(query);
                    return result.FirstOrDefault();
                }
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}