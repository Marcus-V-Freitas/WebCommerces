using BLL.Models;
using BLL.Library.Cookies;
using System.Collections.Generic;
using System.Linq;
using acao = BLL.Enums.CrudAcao;
using BLL.Models.ViewModels;
using DAL.Repositories.EntityFramework.Interfaces;
using System.Threading.Tasks;
using BLL.Exceptions;

namespace DAL.Repositories.EntityFramework.Compras
{
    public class CarrinhoCompras
    {
        private readonly CookieCompras _cookie;
        private readonly IProdutoRepository _produtoRepository;
        private readonly IEstoqueReppository _estoqueReppository;
        public List<ItemVendaViewModel> ItensCarrinho { get; set; }

        public CarrinhoCompras(CookieCompras cookie, IProdutoRepository produtoRepository, IEstoqueReppository estoqueReppository)
        {
            _cookie = cookie;
            _produtoRepository = produtoRepository;
            _estoqueReppository = estoqueReppository;
        }

        public async Task AdicionarCarrinho(Produto produto, int Quantidade)
        {
            int QuantidadeDisponivel = await _estoqueReppository.RetornarQuantidadeDisponivel(produto);

            if (QuantidadeDisponivel > 0)
            {
                var itemVendaCookie = _cookie.Consultar().FirstOrDefault(x => x.ProdutoId.Equals(produto.ID));

                if (itemVendaCookie == null)
                {
                    var itemVenda = new ItemVendaViewModel()
                    {
                        ProdutoId = produto.ID,
                        Produto = produto,
                        Quantidade = Quantidade
                    };
                    _cookie.Cadastrar(itemVenda);
                }
                else
                {
                    if (QuantidadeDisponivel - (itemVendaCookie.Quantidade + Quantidade) >= 0)
                    {
                        _cookie.Atualizar(produto, acao.Adicionar);
                    }
                    else
                    {
                        throw new RepositoryException($"A quantidade para o produto {produto.Nome} no estoque é de {(QuantidadeDisponivel <= 1 ? $"{QuantidadeDisponivel} Unidade." : $"{QuantidadeDisponivel} unidades.")}");
                    }

                }
            }
            else
            {
                throw new RepositoryException($"A quantidade para o produto {produto.Nome} no estoque é de {(QuantidadeDisponivel <= 1 ? $"{QuantidadeDisponivel} Unidade." : $"{QuantidadeDisponivel} unidades.")}");
            }
        }

        public void RemoverCarrinho(Produto produto)
        {
            var valor = _cookie.Atualizar(produto, acao.Remover);

            if (valor.Quantidade.Equals(0))
            {
                _cookie.Remover(produto);
            }
        }

        public async Task<List<ItemVendaViewModel>> TodosItemsCarrinho()
        {
            if (ItensCarrinho == null)
            {
                List<ItemVendaViewModel> itemCarrinhoCookie = _cookie.Consultar();
                List<ItemVendaViewModel> itemCarrinhoCompleto = new List<ItemVendaViewModel>();

                foreach (var item in itemCarrinhoCookie)
                {
                    item.Produto = await _produtoRepository.ObterId(item.ProdutoId);
                    itemCarrinhoCompleto.Add(item);
                }
                ItensCarrinho = itemCarrinhoCompleto;
            }

            return ItensCarrinho;
        }

        public async Task<decimal> TotalCompra()
        {
            return (decimal)(await TodosItemsCarrinho()).Select(x => x.Produto.Valor * x.Quantidade).Sum();
        }

        public void LimparCarrinho()
        {
            _cookie.RemoverCookie();
        }
    }
}
