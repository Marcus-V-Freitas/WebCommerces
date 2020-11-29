using BLL.Models;
using DAL.Repositories.EntityFramework.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace DAL.Repositories.EntityFramework.Repository
{
    public class MarcaCartaoCreditoRepository : BaseRepository<MarcaCartaoCredito>, IMarcaCartaoCreditoRepository
    {
        public MarcaCartaoCreditoRepository(WebContext context) : base(context)
        {
        }

        public async Task<List<string>> RetornarNomesMarcasAtivas()
        {
            return await (from marca in _context.MarcasCartaoCredito
                          where marca.Ativa
                          select marca.Nome).ToListAsync();
        }
    }
}
