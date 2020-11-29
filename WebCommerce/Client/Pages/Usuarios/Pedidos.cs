using BLL.Models;
using BLL.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using WebCommerce.Client.Services;
using WebCommerce.Client.Services.HTTP.Routes;
using WebCommerce.Client.Services.Notificacoes;
using validationLocalizer = BLL.Library.Resources.Models.Validation;

namespace WebCommerce.Client.Pages.Usuarios
{
    public partial class Pedidos : IDisposable
    {
        public Pedido[] PedidosCliente { get; set; }

        private FiltroDataViewModel Filtro = new FiltroDataViewModel() { DataInical = new DateTime(DateTime.Now.Year, 01, 01), Datafinal = new DateTime(DateTime.Now.Year, 12, 31) };

        protected override async Task OnInitializedAsync()
        {
            interceptor.RegisterEvent();
            await CarregarDados();
        }

        private async Task OnClick()
        {
            string mensagemErro = string.Empty;

            if (DateTime.Compare(Filtro.DataInical, Filtro.Datafinal) > 0)
            {
                mensagemErro = validation[validationLocalizer.DataInicialMaiorDataFinal];
            }

            if (string.IsNullOrEmpty(mensagemErro))
            {
                PedidosCliente = await http.POST<Pedido[]>(Api.Pedido, Filtro);
            }
            else
            {
                NotificationService.Notify(PopUps.Error(validation[validationLocalizer.Erro], mensagemErro));
                await Task.CompletedTask;
            }
        }

        public async Task CancelarPedido(int id)
        {
            if (await http.DELETE<bool>($"{Api.Venda}/{id}"))
            {
                NotificationService.Notify(PopUps.Success(validation[validationLocalizer.Sucesso], validation[validationLocalizer.CancelarPedidoMensagem]));
                await CarregarDados();
            }
        }

        public async Task CarregarDados()
        {
            PedidosCliente = await http.POST<Pedido[]>(Api.Pedido, Filtro);
        }

        public void Dispose()
        {
            interceptor.DisposeEvent();
        }
    }
}
