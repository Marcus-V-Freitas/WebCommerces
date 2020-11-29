using BLL.Models;
using BLL.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repositories.EntityFramework.Interfaces
{
    public interface IVendaRepository : IBaseRepository<Venda>
    {
        Task<Venda> ObterPorPedido(int id);
        Task<Venda> CriarVenda(double TotalCompra, List<ItemVendaViewModel> itensVenda, Pedido pedido, Usuario usuario);
    }
}
