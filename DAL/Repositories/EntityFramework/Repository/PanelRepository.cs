using BLL.Models.ViewModels;
using DAL.Repositories.EntityFramework.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace DAL.Repositories.EntityFramework.Repository
{
    public class PanelRepository : IPainelRepository
    {
        private readonly DateTime inicioAnoPadrao = new DateTime(DateTime.Now.Year, 01, 01);
        private readonly DateTime FimAnoPadrao = new DateTime(DateTime.Now.Year, 12, 31);
        private readonly WebContext _context;
        public PanelRepository(WebContext context)
        {
            _context = context;
        }
        public async Task<PainelViewModel> RetornarDadosUsuario(int id, FiltroDataViewModel filtro = null)
        {
            if (filtro is null)
            {
                filtro = new FiltroDataViewModel() { DataInical = inicioAnoPadrao, Datafinal = FimAnoPadrao };
            }

            var pedidos = _context.Pedidos.AsQueryable().Where(x => x.UsuarioPedidoId == id && x.DataRegistro >= filtro.DataInical && x.DataRegistro <= filtro.Datafinal);
            var vendasFeitas = _context.Vendas.AsQueryable().Where(x => x.Horario >= filtro.DataInical && x.Horario <= filtro.Datafinal && x.usuarioVendaID == id);

            var quantidadePedidos = await pedidos.CountAsync();

            var valorTotal = await pedidos.SumAsync(x => x.ValorTotal);


            var despesas = await (from ped in pedidos
                                  group ped by ped.Situacao into situacoes
                                  select new DespesasViewModel()
                                  {
                                      TipoDeDespesa = situacoes.Key,
                                      ValorDespesa = situacoes.Sum(p => p.ValorTotal)
                                  }).ToListAsync();

            var comparativoAnual = await (from itens in _context.ItemsVenda
                                          join produtos in _context.Produtos
                                            on itens.ProdutoId equals produtos.ID
                                          join vendas in vendasFeitas
                                             on itens.VendaId equals vendas.Id
                                          group itens by produtos.Nome
                                            into produtosSelecionados
                                          select new DespesasViewModel()
                                          {
                                              TipoDeDespesa = produtosSelecionados.Key,
                                              ValorDespesa = produtosSelecionados.Sum(x => x.Quantidade)
                                          }
                             ).ToListAsync();


            var comparativoMensal = (from mes in Enumerable.Range(1, 12)
                                     join venda in vendasFeitas
                                        on mes equals venda.Horario.Month into values
                                     from ord in values.DefaultIfEmpty()
                                     group ord by mes into vendasHorario
                                     select new DespesasViewModel()
                                     {
                                         TipoDeDespesa = vendasHorario.Key.ToString(),
                                         ValorDespesa = (decimal)vendasHorario.Sum(x => x is null ? 0 : x.Total)
                                     }).ToList();

            var formasPagamento = await (from ped in pedidos
                                         group ped by ped.FormaPagamento into formas
                                         select new DespesasViewModel()
                                         {
                                             TipoDeDespesa = formas.Key,
                                             ValorDespesa = formas.Sum(x => x.ValorTotal)
                                         }).ToListAsync();

            return new PainelViewModel()
            {
                QuantidadeCompras = quantidadePedidos,
                TotalDespesas = valorTotal,
                ListaDespesas = despesas,
                ComparativoAnual = comparativoAnual,
                ComparativoMensal = comparativoMensal,
                FormasPagamento = formasPagamento
            };
        }
    }
}
