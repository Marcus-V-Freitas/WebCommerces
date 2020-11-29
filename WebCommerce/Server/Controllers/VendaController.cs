using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BLL.Constants;
using BLL.Enums;
using BLL.Library.Cookies;
using BLL.Library.Pagamento;
using BLL.Models;
using BLL.Models.ViewModels;
using DAL.Repositories.EntityFramework.Compras;
using DAL.Repositories.EntityFramework.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PagarMe;
using WebCommerce.Server.Services.HTTP.Interface;
using WebCommerce.Server.Services.Pagamento;

namespace WebCommerce.Server.Controllers
{
    [ApiController]
    [Produces("application/json")]
    [Route("api/[controller]")]
    [Authorize(Roles = "Administrator")]
    public class VendaController : Controller
    {
        private readonly IVendaRepository _vendaRepository;
        private readonly CarrinhoCompras _carrinhoCompras;
        private readonly GerenciarPagarMe _pagarMe;
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IMarcaCartaoCreditoRepository _cartaoCreditoRepository;
        private readonly IMovimentacaoRepository _movimentacaoRepository;
        private readonly CookieFrete _cookieFrete;
        private PagamentoServico _servico;
        private readonly IEnderecoEntregaRepository _entregaRepository;
        private readonly ITokenService _tokenService;
        private readonly IPedidoRepository _pedidoRepository;

        public VendaController(IVendaRepository vendaRepository, CarrinhoCompras carrinhoCompras,
                              GerenciarPagarMe pagarMe,
                              PagamentoServico servico, IUsuarioRepository usuarioRepository,
                              IMarcaCartaoCreditoRepository cartaoCreditoRepository,
                              IMovimentacaoRepository movimentacaoRepository,
                              CookieFrete cookieFrete,
                              IEnderecoEntregaRepository entregaRepository,
                              ITokenService tokenService,
                              IPedidoRepository pedidoRepository)
        {
            _tokenService = tokenService;
            _vendaRepository = vendaRepository;
            _carrinhoCompras = carrinhoCompras;
            _pagarMe = pagarMe;
            _servico = servico;
            _usuarioRepository = usuarioRepository;
            _cartaoCreditoRepository = cartaoCreditoRepository;
            _movimentacaoRepository = movimentacaoRepository;
            _cookieFrete = cookieFrete;
            _entregaRepository = entregaRepository;
            _pedidoRepository = pedidoRepository;
        }

        /// <summary>
        /// Realiza a venda no sistema e faz a movimentação do estoque dos produtos
        /// que foram comprados
        /// </summary>
        /// <param name="pagamento"> Objeto Pagamento </param>
        /// <param name="authorization">Token de Autorizacao</param>
        /// <returns> Status da Operação </returns>
        [HttpPost("")]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> CriarVenda([FromBody] PagamentoViewModel pagamento, [FromHeader] string authorization)
        {
            var usuario = await _tokenService.ExtractUsuarioToken(authorization);

            if (usuario is null)
                return BadRequest("");

            try
            {
                _carrinhoCompras.ItensCarrinho = await _carrinhoCompras.TodosItemsCarrinho();


                if (_carrinhoCompras.ItensCarrinho.Count.Equals(0))
                {
                    ModelState.AddModelError("", "Carro Vazio");
                }

                if (ModelState.IsValid)
                {
                    Parcelamento parcela = _servico.BuscarParcelamento(await _carrinhoCompras.TotalCompra(), pagamento.CartaoDeCredito.QuantidadeParcelas);
                    Transaction transaction = _pagarMe.GerarPagCartaoCredito(pagamento.CartaoDeCredito, parcela, await _entregaRepository.EnderecoEntregaUsuario(usuario.Id), pagamento.FreteValor, _carrinhoCompras.ItensCarrinho, usuario);
                    var pedido = await _servico.ProcessarPedido(_carrinhoCompras.ItensCarrinho, transaction);
                    var venda = await _vendaRepository.CriarVenda((double)(await _carrinhoCompras.TotalCompra()), _carrinhoCompras.ItensCarrinho, pedido, usuario);
                    await _movimentacaoRepository.MovimentarEstoqueVenda(venda, StatusMovimentacao.SAIDA, TipoMovimentacao.VENDA);
                    _carrinhoCompras.LimparCarrinho();
                }
                return Ok(true);
            }
            catch (PagarMeException ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, ex.Error);
            }
        }

        /// <summary>
        /// Retorna os dados referentes ao cadastro da venda no sistema
        /// </summary>
        /// <returns> Objeto Venda com as opções de seleção do usuário </returns>
        [HttpGet("Parcelas")]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> CalcularParcelas()
        {
            return Ok(new VendaViewModel
            {
                Parcelas = _pagarMe.CalcularPagamentoParcelado(await _carrinhoCompras.TotalCompra())
                                         ,
                NomesCartoesDeCredito = await _cartaoCreditoRepository.RetornarNomesMarcasAtivas()
            });
        }


        [HttpDelete("{id}")]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> CalcelarPedido([FromRoute] int id, [FromHeader] string authorization)
        {
            Pedido pedido = await _pedidoRepository.ObterId(id);
            Venda venda = await _vendaRepository.ObterPorPedido(id);

            if (ModelState.IsValid)
            {
                _pagarMe.EstornoCartaoCredito(pedido.TransactionId);
                await _servico.SalvarPedidoSituacao(id, pedido.DadosTransaction, PedidoSituacaoConstant.ESTORNO);
                await _servico.SalvarPedido(pedido, PedidoSituacaoConstant.ESTORNO);
                await _movimentacaoRepository.MovimentarEstoqueVenda(venda, StatusMovimentacao.ENTRADA, TipoMovimentacao.VENDA);
            }
            return Ok(true);
        }

    }
}
