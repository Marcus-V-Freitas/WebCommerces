using AutoMapper;
using BLL.Library.Texto;
using BLL.Models;
using BLL.Models.ViewModels;
using Microsoft.Extensions.Configuration;
using PagarMe;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace BLL.Library.Pagamento
{
    public class GerenciarPagarMe
    {
        private IMapper _mapper;
        //private LoginCliente _loginCliente;
        private readonly string DefaultApiKey = "ak_test_7VAjSBubdQj1qT3Kgc7AWluUKfiv3M";
        private readonly string DefaultEncryptionKey = "ek_test_KEA6ruzcJ7tTF8dEEJ6ik6rWfXfiGW";
        private readonly int BoletoDiaExpiracao = 2;
        private readonly int DiasNaEmpresa = 3;
        private readonly int MaxParcelas = 12;
        private readonly int ParcelaPagaVendedor = 3;
        private readonly decimal Juros = 5;

        public GerenciarPagarMe(/*LoginCliente loginCliente,*/ IMapper mapper)
        {
            //_loginCliente = loginCliente;
            _mapper = mapper;
        }

        public Transaction GerarBoleto(decimal valor, List<ItemVenda> produtos, EnderecoEntrega enderecoEntrega, ValorPrazoFrete valorFrete, Usuario usuario)
        {
            Usuario cliente = usuario;

            PagarMeService.DefaultApiKey = DefaultApiKey;
            PagarMeService.DefaultEncryptionKey = DefaultEncryptionKey;
            int DaysExpire = BoletoDiaExpiracao;

            Transaction transaction = new Transaction();

            transaction.Amount = Mascara.ConverterValorPagarMe(valor);
            transaction.PaymentMethod = PaymentMethod.Boleto;
            transaction.BoletoExpirationDate = DateTime.Now.AddDays(DaysExpire);

            transaction.Customer = new Customer
            {
                ExternalId = cliente.Id.ToString(),
                Name = cliente.Nome,
                Type = CustomerType.Individual,
                Country = "br",
                Email = cliente.Email,
                Documents = new[] {
                        new Document{
                            Type = DocumentType.Cpf,
                            Number = Mascara.Remover(cliente.CPFCNPJ)
                        }
                    },
                PhoneNumbers = new string[]
                {
                        "+55" + Mascara.Remover( cliente.Telefone )
                },
                Birthday = cliente.Nascimento.ToString("yyyy-MM-dd")
            };

            var Today = DateTime.Now;
            var fee = Convert.ToDecimal(valorFrete.Valor);

            transaction.Shipping = new Shipping
            {
                Name = enderecoEntrega.Nome,
                Fee = Mascara.ConverterValorPagarMe(fee),
                DeliveryDate = Today.AddDays(DiasNaEmpresa).AddDays(valorFrete.Prazo).ToString("yyyy-MM-dd"),
                Expedited = false,
                Address = new Address()
                {
                    Country = "br",
                    State = enderecoEntrega.Estado,
                    City = enderecoEntrega.Cidade,
                    Neighborhood = enderecoEntrega.Bairro,
                    Street = enderecoEntrega.Endereco + " " + enderecoEntrega.Complemento,
                    StreetNumber = enderecoEntrega.Numero,
                    Zipcode = Mascara.Remover(enderecoEntrega.CEP)
                }
            };

            Item[] itens = new Item[produtos.Count];

            for (var i = 0; i < produtos.Count; i++)
            {
                var item = produtos[i];

                itens[i] = new Item()
                {
                    Id = item.ID.ToString(),
                    Title = item.Produto.Nome,
                    Quantity = item.Quantidade,
                    Tangible = true,
                    UnitPrice = Mascara.ConverterValorPagarMe(item.Produto.Valor)
                };
            }

            transaction.Item = itens;

            transaction.Save();

            transaction.Customer.Gender = (cliente.Sexo == Enums.Sexo.Masculino) ? Gender.Male : Gender.Female;
            return transaction;
        }


        public Transaction GerarPagCartaoCredito(CartaoCredito cartao, Parcelamento parcelamento, EnderecoEntrega enderecoEntrega, ValorPrazoFrete valorFrete, List<ItemVendaViewModel> produtos, Usuario usuario)
        {
            Usuario cliente = usuario;

            PagarMeService.DefaultApiKey = DefaultApiKey;
            PagarMeService.DefaultEncryptionKey = DefaultEncryptionKey;

            Card card = new Card();
            card.Number = cartao.NumeroCartao;
            card.HolderName = cartao.NomeNoCartao;
            card.ExpirationDate = cartao.VecimentoMM + cartao.VecimentoYY;
            card.Cvv = cartao.CodigoSeguranca;

            card.Save();

            Transaction transaction = new Transaction();
            transaction.PaymentMethod = PaymentMethod.CreditCard;
            transaction.Card = new Card
            {
                Id = card.Id
            };

            transaction.Customer = new Customer
            {
                ExternalId = cliente.Id.ToString(),
                Name = cliente.Nome,
                Type = CustomerType.Individual,
                Country = "br",
                Email = cliente.Email,
                Documents = new[] {
                        new Document{
                            Type = DocumentType.Cpf,
                            Number = Mascara.Remover(cliente.CPFCNPJ)
                        }
                    },
                PhoneNumbers = new string[]
                    {
                        "+55" + Mascara.Remover( cliente.Telefone )
                    },
                Birthday = cliente.Nascimento.ToString("yyyy-MM-dd")
            };

            transaction.Billing = new Billing
            {
                Name = cliente.Nome,
                Address = new Address()
                {
                    Country = "br",
                    State = enderecoEntrega.Estado,
                    City = enderecoEntrega.Cidade,
                    Neighborhood = enderecoEntrega.Bairro,
                    Street = enderecoEntrega.Endereco + " " + enderecoEntrega.Complemento,
                    StreetNumber = enderecoEntrega.Numero,
                    Zipcode = Mascara.Remover(enderecoEntrega.CEP)
                }
            };

            var Today = DateTime.Now;
            var fee = Convert.ToDecimal(valorFrete.Valor);

            transaction.Shipping = new Shipping
            {
                Name = enderecoEntrega.Nome,
                Fee = Mascara.ConverterValorPagarMe(fee),
                DeliveryDate = Today.AddDays(DiasNaEmpresa).AddDays(valorFrete.Prazo).ToString("yyyy-MM-dd"),
                Expedited = false,
                Address = new Address()
                {
                    Country = "br",
                    State = enderecoEntrega.Estado,
                    City = enderecoEntrega.Cidade,
                    Neighborhood = enderecoEntrega.Bairro,
                    Street = enderecoEntrega.Endereco + " " + enderecoEntrega.Complemento,
                    StreetNumber = enderecoEntrega.Numero,
                    Zipcode = Mascara.Remover(enderecoEntrega.CEP)
                }
            };

            Item[] itens = new Item[produtos.Count];

            for (var i = 0; i < produtos.Count; i++)
            {
                var item = produtos[i];
                itens[i] = new Item()
                {
                    Id = item.Produto.ID.ToString(),
                    Title = item.Produto.Nome,
                    Quantity = item.Quantidade,
                    Tangible = true,
                    UnitPrice = Mascara.ConverterValorPagarMe(item.Produto.Valor),
                };
            }

            transaction.Item = itens;
            transaction.Amount = Mascara.ConverterValorPagarMe(parcelamento.Valor);
            transaction.Installments = parcelamento.Numero;

            transaction.Save();

            transaction.Customer.Gender = (cliente.Sexo == Enums.Sexo.Masculino) ? Gender.Male : Gender.Female;
            return transaction;
        }


        public List<Parcelamento> CalcularPagamentoParcelado(decimal valor)
        {
            List<Parcelamento> lista = new List<Parcelamento>();

            int maxParcelamento = MaxParcelas;
            int parcelaPagaVendedor = ParcelaPagaVendedor;
            decimal juros = Juros;

            for (int i = 1; i <= maxParcelamento; i++)
            {
                Parcelamento parcelamento = new Parcelamento();
                parcelamento.Numero = i;

                if (i > parcelaPagaVendedor)
                {
                    //Juros - i = (4-3 - parcelaPagaVendedor) + 5%
                    int quantidadeParcelasComJuros = i - parcelaPagaVendedor;
                    decimal valorDoJuros = valor * juros / 100;

                    parcelamento.Valor = quantidadeParcelasComJuros * valorDoJuros + valor;
                    parcelamento.ValorPorParcela = parcelamento.Valor / parcelamento.Numero;
                    parcelamento.Juros = true;

                }
                else
                {
                    parcelamento.Valor = valor;
                    parcelamento.ValorPorParcela = parcelamento.Valor / parcelamento.Numero;
                    parcelamento.Juros = false;
                }
                lista.Add(parcelamento);
            }

            return lista;
        }

        public Transaction ObterTransacao(string transactionId)
        {
            PagarMeService.DefaultApiKey = "ak_test_7VAjSBubdQj1qT3Kgc7AWluUKfiv3M";

            return PagarMeService.GetDefaultService().Transactions.Find(transactionId);
        }
        public Transaction EstornoCartaoCredito(string transactionId)
        {
            PagarMeService.DefaultApiKey = "ak_test_7VAjSBubdQj1qT3Kgc7AWluUKfiv3M";

            var transaction = PagarMeService.GetDefaultService().Transactions.Find(transactionId);

            transaction.Refund();

            return transaction;
        }


        public Transaction EstornoBoletoBancario(string transactionId, DadosCancelamentoBoleto boletoBancario)
        {
            PagarMeService.DefaultApiKey = "ak_test_7VAjSBubdQj1qT3Kgc7AWluUKfiv3M";

            var transaction = PagarMeService.GetDefaultService().Transactions.Find(transactionId);
            var bankAccount = _mapper.Map<DadosCancelamentoBoleto, BankAccount>(boletoBancario);

            transaction.Refund(bankAccount);

            return transaction;
        }
    }

}