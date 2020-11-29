using AutoMapper;
using BLL.Constants;
using BLL.Library.Pagamento;
using BLL.Models;
using BLL.Models.ViewModels;
using DAL.Repositories.EntityFramework.Interfaces;
using Newtonsoft.Json;
using PagarMe;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebCommerce.Server.Services.Pagamento
{
    public class PagamentoServico
    {
        private readonly GerenciarPagarMe _PagarMe;
        private readonly IMapper _mapper;
        private readonly IPedidoRepository _pedidoRepository;
        private readonly IPedidoSituacaoRepository _pedidoSituacaoRepository;
        private Pedido pedido;
        private TransacaoPagarMe transacaoPagarMe;

        public PagamentoServico(GerenciarPagarMe pagarMe, IMapper mapper,
                                IPedidoSituacaoRepository pedidoSituacaoRepository,
                                IPedidoRepository pedidoRepository)
        {
            _PagarMe = pagarMe;
            _mapper = mapper;
            _pedidoRepository = pedidoRepository;
            _pedidoSituacaoRepository = pedidoSituacaoRepository;
        }

        public Parcelamento BuscarParcelamento(decimal total, int numero)
        {

            return _PagarMe.CalcularPagamentoParcelado(total).Where(a => a.Numero == numero).FirstOrDefault();
        }

        public async Task<Pedido> ProcessarPedido(List<ItemVendaViewModel> produtos, Transaction transaction)
        {

            await SalvarPedido(produtos, transaction);

            await SalvarPedidoSituacao(produtos);

            return pedido;
        }

        private async Task SalvarPedidoSituacao(List<ItemVendaViewModel> produtos)
        {
            TransactionProduto tp = new TransactionProduto { Transaction = transacaoPagarMe, Produtos = produtos };
            PedidoSituacao pedidoSituacao = _mapper.Map<Pedido, PedidoSituacao>(pedido);
            pedidoSituacao = _mapper.Map(tp, pedidoSituacao);

            pedidoSituacao.Situacao = PedidoSituacaoConstant.PEDIDO_REALIZADO;

            await _pedidoSituacaoRepository.Adicionar(pedidoSituacao);
        }

        private async Task SalvarPedido(List<ItemVendaViewModel> produtos, Transaction transaction)
        {
            transacaoPagarMe = _mapper.Map<TransacaoPagarMe>(transaction);
            pedido = _mapper.Map<TransacaoPagarMe, Pedido>(transacaoPagarMe);
            pedido = _mapper.Map(produtos, pedido);

            pedido.Situacao = PedidoSituacaoConstant.PEDIDO_REALIZADO;

            await _pedidoRepository.Adicionar(pedido);
        }

        public async Task SalvarPedido(Pedido pedido, string situacao, string nfe = null, string codRastreamento = null)
        {
            pedido.Situacao = situacao;
            if (nfe != null)
            {
                pedido.NFE = nfe;
            }
            if (codRastreamento != null)
            {
                pedido.FreteCodRastreamento = codRastreamento;
            }
            await _pedidoRepository.Atualizar(pedido.Id, pedido);
        }

        public async Task SalvarPedidoSituacao(int pedidoId, object dados, string situacao)
        {
            var pedidoSituacao = new PedidoSituacao();
            pedidoSituacao.Data = DateTime.Now;
            pedidoSituacao.Dados = JsonConvert.SerializeObject(dados);
            pedidoSituacao.PedidoId = pedidoId;
            pedidoSituacao.Situacao = situacao;
            await _pedidoSituacaoRepository.Adicionar(pedidoSituacao);
        }

    }
}
