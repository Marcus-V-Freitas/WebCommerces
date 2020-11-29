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
    public class VendaRepository : BaseRepository<Venda>, IVendaRepository
    {
        public VendaRepository(WebContext context) : base(context) { }


        public async Task<Venda> ObterPorPedido(int id)
        {
            return await _context.Vendas.Where(x => x.IDPedidoVenda == id).FirstOrDefaultAsync();
        }

        public async Task<Venda> CriarVenda(double TotalCompra, List<ItemVendaViewModel> itensVenda, Pedido pedido, Usuario usuario)
        {
            var venda = new Venda();
            venda.Horario = DateTime.Now;
            venda.Total = TotalCompra;
            venda.usuarioVendaID = usuario.Id;
            venda.IDPedidoVenda = pedido.Id;
            _context.Vendas.Add(venda);

            var vendaItens = new List<ItemVenda>();

            foreach (var item in itensVenda)
            {
                vendaItens.Add(new ItemVenda()
                {
                    Preço = item.Preço,
                    ProdutoId = item.ProdutoId,
                    Quantidade = item.Quantidade,
                    Venda = venda,
                });
            };

            _context.ItemsVenda.AddRange(vendaItens);

            await _context.SaveChangesAsync();

            return venda;
        }
    }
}
