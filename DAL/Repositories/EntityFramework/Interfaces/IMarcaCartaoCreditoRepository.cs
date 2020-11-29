using BLL.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories.EntityFramework.Interfaces
{
    public interface IMarcaCartaoCreditoRepository : IBaseRepository<MarcaCartaoCredito>
    {
        Task<List<string>> RetornarNomesMarcasAtivas();
    }
}
