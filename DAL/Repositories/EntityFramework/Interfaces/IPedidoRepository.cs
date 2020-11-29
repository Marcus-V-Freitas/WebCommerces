using BLL.Models;
using BLL.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories.EntityFramework.Interfaces
{
    public interface IPedidoRepository : IBaseRepository<Pedido>
    {
        public Task<List<Pedido>> RetornarPedidosData(int id, FiltroDataViewModel filtro);
    }
}
