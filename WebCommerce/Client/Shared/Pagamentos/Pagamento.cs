using BLL.Models;
using BLL.Models.ViewModels;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using WebCommerce.Client.Services;
using WebCommerce.Client.Services.HTTP.Routes;
using vendasLocalizer = BLL.Library.Resources.Pages.Venda.Componentes;

namespace WebCommerce.Client.Shared.Pagamentos
{
    public partial class Pagamento
    {
        public VendaViewModel VendaViewModel { get; set; }

        public PagamentoViewModel PagamentoViewModel { get; set; } = new PagamentoViewModel();


        protected override async Task OnInitializedAsync()
        {
            VendaViewModel = await http.GET<VendaViewModel>($"{Api.Venda}/Parcelas");
        }


        public async Task CriarVenda()
        {
            if (await http.POST<bool>(Api.Venda, PagamentoViewModel))
            {
                await js.MostrarMessagemSucesso(localizer[vendasLocalizer.MensagemRealizada]);
                Notifier.Atualizar();
                navigation.NavigateTo("/");
            }
        }

        public void Change(ChangeEventArgs e)
        {
            PagamentoViewModel.CartaoDeCredito.QuantidadeParcelas = (e.Value.ToString() == localizer[vendasLocalizer.MensagemSelecionar]) ? 0 : Convert.ToInt32(e.Value.ToString());

        }

        public void ChangeNome(ChangeEventArgs e)
        {
            PagamentoViewModel.CartaoDeCredito.NomeNoCartao = (e.Value.ToString() == localizer[vendasLocalizer.MensagemSelecionar]) ? "" : e.Value.ToString();
        }

    }
}
