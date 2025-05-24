using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ConsultersIT.Infra.Data.Context;
using ConsultersIT.Infra.Interfaces;
using Dapper;

namespace ConsultersIT.Infra.Repositories
{
    public class TesteRepository :  BaseRepository, ITesteRepository
    {
        public TesteRepository(IDbConnectionFactory connectionFactory) 
            : base(connectionFactory) { }
        public async Task<string> GetTesteAsync()
        {
            var query = @"SELECT version()";
            try
            {
                //using (var conn = _dbContext.CreateConnection())
               // {
                //    var result = await conn.QueryAsync<string>(query);
                //    return result.FirstOrDefault();
               // }

               return await WithConnection(async conn =>
               {
                   const string sql = "SELECT version()";
                   return await conn.QueryFirstOrDefaultAsync<string>(sql);

               });

            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}