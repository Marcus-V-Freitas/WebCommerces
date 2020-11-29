using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BLL.Constants;
using BLL.Library.JSON.Resolver;
using BLL.Library.Texto;
using BLL.Models;
using BLL.Models.ViewModels;
using Newtonsoft.Json;
using PagarMe;

namespace BLL.Library.AutoMapper
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {

            CreateMap<Transaction, TransacaoPagarMe>();

            CreateMap<TransacaoPagarMe, Pedido>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(orig => 0))
                .ForMember(dest => dest.UsuarioPedidoId, opt => opt.MapFrom(orig => int.Parse(orig.Customer.ExternalId)))
                .ForMember(dest => dest.TransactionId, opt => opt.MapFrom(orig => orig.Id))
                .ForMember(dest => dest.FreteEmpresa, opt => opt.MapFrom(orig => "ECT - Correios"))
                .ForMember(dest => dest.FormaPagamento, opt => opt.MapFrom(orig => (orig.PaymentMethod == 0) ? MetodoPagamentoConstant.CartaoCredito : MetodoPagamentoConstant.Boleto))
                .ForMember(dest => dest.DadosTransaction, opt => opt.MapFrom(orig => JsonConvert.SerializeObject(orig, new JsonSerializerSettings() { ReferenceLoopHandling = ReferenceLoopHandling.Ignore })))
                .ForMember(dest => dest.DataRegistro, opt => opt.MapFrom(orig => DateTime.Now))
                .ForMember(dest => dest.ValorTotal, opt => opt.MapFrom(orig => Mascara.ConverterPagarMeIntToDecimal(orig.Amount)));

            CreateMap<List<ItemVendaViewModel>, Pedido>()
                .ForMember(dest => dest.DadosProdutos, opt => opt.MapFrom(orig => JsonConvert.SerializeObject(orig, new JsonSerializerSettings() { ContractResolver = new ProdutoItemResolver<List<ItemVendaViewModel>>(), ReferenceLoopHandling = ReferenceLoopHandling.Ignore })));

            CreateMap<Pedido, PedidoSituacao>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(orig => 0))
                .ForMember(dest => dest.PedidoId, opt => opt.MapFrom(orig => orig.Id))
                .ForMember(dest => dest.Data, opt => opt.MapFrom(orig => DateTime.Now));

            CreateMap<TransactionProduto, PedidoSituacao>()
                .ForMember(dest => dest.Dados, opt => opt.MapFrom(orig => JsonConvert.SerializeObject(orig, new JsonSerializerSettings() { ContractResolver = new ProdutoItemResolver<List<ItemVendaViewModel>>(), ReferenceLoopHandling = ReferenceLoopHandling.Ignore })));

            //CreateMap<DadosCancelamentoBoleto, BankAccount>()
            //    .ForMember(dest => dest.BankCode, opt => opt.MapFrom(orig => orig.BancoCodigo))
            //    .ForMember(dest => dest.Agencia, opt => opt.MapFrom(orig => orig.Agencia))
            //    .ForMember(dest => dest.AgenciaDv, opt => opt.MapFrom(orig => orig.AgenciaDV))
            //    .ForMember(dest => dest.Conta, opt => opt.MapFrom(orig => orig.Conta))
            //    .ForMember(dest => dest.ContaDv, opt => opt.MapFrom(orig => orig.ContaDV))
            //    .ForMember(dest => dest.LegalName, opt => opt.MapFrom(orig => orig.Nome))
            //    .ForMember(dest => dest.DocumentNumber, opt => opt.MapFrom(orig => orig.CPF));
        }
    }
}
