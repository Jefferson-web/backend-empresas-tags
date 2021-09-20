using Dapper;
using Microsoft.Extensions.Configuration;
using PruebaWebAPI.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace PruebaWebAPI.Repositories
{
    public class EmpresaRepository : IEmpresaRepository
    {
        private readonly IConfiguration _configuration;

        public EmpresaRepository(IConfiguration configuration)
        {
            this._configuration = configuration;
        }
        private IDbConnection Connection {
            get {
                var db = new SqlConnection(_configuration.GetConnectionString("default"));
                db.Open();
                return db;
            }
        } 
        public async Task<int> CreateAsync(Empresa empresa)
        {
            using var db = Connection;
            string query = "insert into empresas(nombre, ruc, fecha_registro) values(@nombre, @ruc, @fecha_registro);" +
                "select cast(scope_identity() as int)";
            var param = new { 
                nombre = empresa.nombre, 
                ruc = empresa.ruc, 
                fecha_registro = empresa.fecha_registro 
            };
            int insertedId = await db.QuerySingleAsync<int>(query, param);
            return insertedId;
        }
        public async Task<Empresa> FindByIdAsync(int id) {
            using var db = Connection;
            string query = "select * from empresas where empresaId=@empresaId";
            var param = new { empresaId = id };
            try
            {
                return await db.QuerySingleAsync<Empresa>(query, param);
            }
            catch (Exception)
            {
                throw;
            }
        }
        public async Task<IEnumerable<Empresa>> ToListAsync()
        {
            using var db = Connection;
            string query = @"select e.empresaId, e.nombre, e.ruc, e.fecha_registro, t.tagId, t.nombre 
                            from empresas e 
                            inner join empresatag et on e.empresaId=et.empresaId 
                            inner join tags t on et.tagId=t.tagId";
            var empresas = await db.QueryAsync<Empresa, Tag, Empresa>(query, (empresa, tag) =>
            {
                empresa.tags.Add(tag);
                return empresa;
            }, splitOn: "tagId");
            var result = empresas.GroupBy(e => e.empresaId).Select(g =>
            {
                var grupedEmpresas = g.First();
                grupedEmpresas.tags = g.Select(e => e.tags.Single()).ToList();
                return grupedEmpresas;
            });
            return result;
        }
    }
}
