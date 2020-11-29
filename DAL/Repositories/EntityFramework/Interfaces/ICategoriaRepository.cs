using BLL.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories.EntityFramework.Interfaces
{
    public interface ICategoriaRepository : IBaseRepository<Categoria>
    {
        Task<List<Categoria>> ObterCategoriasFiltro(string data);
    }
}
