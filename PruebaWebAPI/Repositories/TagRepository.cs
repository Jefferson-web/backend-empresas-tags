using Dapper;
using Microsoft.Extensions.Configuration;
using PruebaWebAPI.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace PruebaWebAPI.Repositories
{
    public class TagRepository : ITagRepository
    {
        private readonly IConfiguration _configuration;

        public TagRepository(IConfiguration configuration)
        {
            this._configuration = configuration;
        }
        private IDbConnection Connection
        {
            get
            {
                var db = new SqlConnection(_configuration.GetConnectionString("default"));
                db.Open();
                return db;
            }
        }

        public async Task<int> CreateTagAsync(Tag tag) {
            using var db = Connection;
            string query = "insert into tags(nombre) values(@nombre);" +
                "select cast(scope_identity() as int)";
            var param = new { nombre = tag.nombre };
            try
            {
                return await db.QuerySingleAsync<int>(query, param);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task CreateAsync(EmpresaTag empresaTag)
        {
            using var db = Connection;
            string sql = "insert into empresatag(empresaId, tagId) values(@empresaId, @tagId);" +
                "select cast(scope_identity() as int)";
            var param = new { empresaId = empresaTag.empresaId, tagId = empresaTag.tagId };
            await db.QueryAsync(sql, param);
        }

        public async Task<Tag> FindByIdAsync(int id)
        {
            using var db = Connection;
            string query = "select * from tags where tagId=@tagId";
            var param = new { tagId = id };
            try
            {
                return await db.QuerySingleAsync<Tag>(query, param);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<IEnumerable<Tag>> ToListAsync()
        {
            using var db = Connection;
            return await db.QueryAsync<Tag>("select * from tags");
        }
    }
}
