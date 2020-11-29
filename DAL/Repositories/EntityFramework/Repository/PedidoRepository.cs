using BLL.Models;
using BLL.Models.ViewModels;
using DAL.Repositories.EntityFramework.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories.EntityFramework.Repository
{
    public class PedidoRepository : BaseRepository<Pedido>, IPedidoRepository
    {
        private readonly DateTime inicioAnoPadrao = new DateTime(DateTime.Now.Year, 01, 01);
        private readonly DateTime FimAnoPadrao = new DateTime(DateTime.Now.Year, 12, 31);

        public PedidoRepository(WebContext context) : base(context)
        {
        }

        public async Task<List<Pedido>> RetornarPedidosData(int id, FiltroDataViewModel filtro)
        {
            if (filtro is null)
            {
                filtro = new FiltroDataViewModel() { DataInical = inicioAnoPadrao, Datafinal = FimAnoPadrao };
            }

            return await _context.Pedidos.AsQueryable().Where(x => x.UsuarioPedidoId == id && x.DataRegistro >= filtro.DataInical && x.DataRegistro <= filtro.Datafinal).ToListAsync();
        }
    }
}
