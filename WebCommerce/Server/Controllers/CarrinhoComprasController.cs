using Microsoft.AspNetCore.Mvc;
using DAL.Repositories.EntityFramework.Compras;
using DAL.Repositories.EntityFramework.Interfaces;
using System.Threading.Tasks;
using BLL.Exceptions;
using System.Collections.Generic;
using WebCommerce.Client.Pages.Produto;
using BLL.Models.ViewModels;
using BLL.Models;
using BLL.Library.Gerenciador.Frete;
using WebCommerce.Server.Services.Frete;
using BLL.Constants;
using BLL.Library.Criptografia;
using System;
using BLL.Library.Cache;
using BLL.Library.Cookies;
using System.Linq;
using BLL.Library.Texto;

namespace WebCommerce.Server.Controllers
{
    [ApiController]
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class CarrinhoComprasController : Controller
    {
        private readonly IProdutoRepository _produtoRepository;
        private readonly CarrinhoCompras _carrinhoCompras;
        private readonly CalcularPacote _calcularPacote;
        private readonly WSCorreiosCalcularFrete _wscorreios;
        private readonly CookieFrete _cookieFrete;
        private readonly IServiceMemoryCache<List<ItemVendaViewModel>> _memoryCache;



        public CarrinhoComprasController(IProdutoRepository produtoRepository, CarrinhoCompras carrinhoCompras,
                                        CalcularPacote calcularPacote, WSCorreiosCalcularFrete calcularFrete,
                                        IServiceMemoryCache<List<ItemVendaViewModel>> memoryCache,
                                        CookieFrete cookieFrete)
        {
            _produtoRepository = produtoRepository;
            _carrinhoCompras = carrinhoCompras;
            _calcularPacote = calcularPacote;
            _wscorreios = calcularFrete;
            _memoryCache = memoryCache;
            _cookieFrete = cookieFrete;
        }

        /// <summary>
        /// Retorna todos os itens do carrinho
        /// </summary>
        /// <returns> Lista de Itens do Carrinho </returns>
        [HttpGet("")]
        public async Task<IActionResult> Index()
        {
            return Ok(await Carrinho());
        }

        /// <summary>
        /// Adiciona uma unidade do Produto no carrinho
        /// </summary>
        /// <param name="Id">Código do Produto</param>
        /// <returns> Status da Operação </returns>
        [HttpPost("")]
        public async Task<IActionResult> AdicionarNoCarrinho([FromBody] int Id)
        {
            try
            {
                var produtoSelecionado = await _produtoRepository.ObterId(Id);

                if (produtoSelecionado != null)
                {
                    await _carrinhoCompras.AdicionarCarrinho(produtoSelecionado, 1);
                }
                return Ok(true);
            }
            catch (RepositoryException ex)
            {
                return BadRequest(ex.Message);
            }
        }

        /// <summary>
        /// Atualizar a quantidade do produto no carrinho para 
        /// menos 1 unidade
        /// </summary>
        /// <param name="Id"> Código do Produto </param>
        /// <returns> Status da Operação </returns>
        [HttpPut("")]
        public async Task<IActionResult> RemoverNoCarrinho([FromBody] int Id)
        {
            var produtoSelecionado = await _produtoRepository.ObterId(Id);

            if (produtoSelecionado != null)
            {
                _carrinhoCompras.RemoverCarrinho(produtoSelecionado);
            }
            return Ok(1);
        }

        /// <summary>
        /// Remove todos os itens do carrinho
        /// </summary>
        /// <returns> Status da Operação </returns>
        [HttpDelete("")]
        public IActionResult LimparCarrinho()
        {

            _carrinhoCompras.LimparCarrinho();
            return Ok(1);
        }

        private async Task<CarrinhoComprasViewModel> Carrinho()
        {
            _carrinhoCompras.ItensCarrinho = await _carrinhoCompras.TodosItemsCarrinho();

            return new CarrinhoComprasViewModel()
            {
                CarrinhoCompras = _carrinhoCompras,
                TotalCompra = await _carrinhoCompras.TotalCompra()
            };
        }


        /// <summary>
        /// Retorna os valores cobrados de frete do
        /// cep de origem (Empresa) ao cep de destino
        /// </summary>
        /// <param name="cepDestino"> CEP do Usuário </param>
        /// <returns> Lista com os valores de acordo com tipo de frete </returns>
        [HttpGet("Frete/{cepDestino}")]
        public async Task<IActionResult> CalcularFrete([FromRoute] string cepDestino)
        {
            var produtos = await _carrinhoCompras.TodosItemsCarrinho();
            Frete frete = _cookieFrete.Consultar().Where(a => a.CEP == Convert.ToInt32(cepDestino) && a.CodCarrinho == StringMD5.GerarHash(produtos)).FirstOrDefault();

            if (frete != null)
            {
                return Ok(frete);
            }
            //await _memoryCache.GetOrCreate("Itens",async () => await _carrinhoCompras.TodosItemsCarrinho());
            List<Pacote> pacotes = _calcularPacote.CalcularPacotesDeProdutos(produtos);

            ValorPrazoFrete valorPAC = await _wscorreios.CalcularFrete(cepDestino, TipoFreteConstant.PAC, pacotes);
            ValorPrazoFrete valorSEDEX = await _wscorreios.CalcularFrete(cepDestino, TipoFreteConstant.SEDEX, pacotes);
            ValorPrazoFrete valorSEDEX10 = await _wscorreios.CalcularFrete(cepDestino, TipoFreteConstant.SEDEX10, pacotes);

            List<ValorPrazoFrete> lista = new List<ValorPrazoFrete>();
            if (valorPAC != null) lista.Add(valorPAC);
            if (valorSEDEX != null) lista.Add(valorSEDEX);
            if (valorSEDEX10 != null) lista.Add(valorSEDEX10);

            frete = new Frete()
            {
                CEP = Convert.ToInt32(cepDestino),
                CodCarrinho = StringMD5.GerarHash(produtos),
                ListaValores = lista
            };
            _cookieFrete.Cadastrar(frete);

            return Ok(frete);
        }

    }
}
