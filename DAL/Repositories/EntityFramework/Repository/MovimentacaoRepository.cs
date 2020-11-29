using BLL.Enums;
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
    public class MovimentacaoRepository : BaseRepository<MovimentacaoEstoque>, IMovimentacaoRepository
    {
        public MovimentacaoRepository(WebContext context) : base(context)
        {
        }

        public async Task MovimentarEstoqueVenda(Venda venda, StatusMovimentacao status, TipoMovimentacao tipo)
        {
            List<MovimentacaoEstoque> movimentacoes = new List<MovimentacaoEstoque>();

            foreach (var item in await _context.ItemsVenda.AsQueryable().Where(x => x.VendaId == venda.Id).ToListAsync())
            {
                movimentacoes.Add(new MovimentacaoEstoque()
                {
                    DataMovimentacao = DateTime.Now,
                    MovUsuarioID = venda.usuarioVendaID,
                    ProdutoMovId = item.ProdutoId,
                    Quantidade = item.Quantidade,
                    TipoMovimentacao = tipo,
                    StatusMovimetacao = status,
                    VendaMovID = venda.Id
                });
                await AtualizarEstoque(item, status);
            }
            _context.MovimentacoesEstoque.AddRange(movimentacoes);
            await _context.SaveChangesAsync();
        }

        private async Task AtualizarEstoque(ItemVenda itemVenda, StatusMovimentacao status)
        {
            var produto = await _context.Estoques.Where(x => x.ProdutoEstoqueId == itemVenda.ProdutoId).FirstOrDefaultAsync();
            produto.Disponivel = (status == StatusMovimentacao.ENTRADA) ? produto.Disponivel + itemVenda.Quantidade : produto.Disponivel - itemVenda.Quantidade;

        }
    }
}
