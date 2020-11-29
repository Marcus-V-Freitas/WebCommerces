using BLL.Models;
using DAL.Repositories.EntityFramework.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories.EntityFramework.Repository
{
    public class CategoriaRepository : BaseRepository<Categoria>, ICategoriaRepository
    {
        public CategoriaRepository(WebContext context) : base(context) { }

        public async Task<List<Categoria>> ObterCategoriasFiltro(string data)
        {
            var Entities = _entity.AsQueryable();

            if (!string.IsNullOrEmpty(data))
            {
                Entities = Entities.Where(x => EF.Functions.Like(x.Nome.ToUpper().Trim(), $"%{data.Trim()}%"));

            }
            return await Entities.ToListAsync();
        }
    }
}
