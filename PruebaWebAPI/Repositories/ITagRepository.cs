using PruebaWebAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PruebaWebAPI.Repositories
{
    public interface ITagRepository
    {
        Task<IEnumerable<Tag>> ToListAsync();
        Task CreateAsync(EmpresaTag empresaTag);
        Task<int> CreateTagAsync(Tag tag);
        Task<Tag> FindByIdAsync(int id);
    }
}
