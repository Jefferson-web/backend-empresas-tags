using Dapper;
using PruebaWebAPI.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace PruebaWebAPI.Repositories
{
    public interface IEmpresaRepository
    {
        Task<IEnumerable<Empresa>> ToListAsync();
        Task<int> CreateAsync(Empresa empresa);
        Task<Empresa> FindByIdAsync(int id);
    }
}
