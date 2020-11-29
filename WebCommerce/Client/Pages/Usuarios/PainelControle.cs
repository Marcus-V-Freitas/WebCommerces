using BLL.Models.ViewModels;
using Microsoft.JSInterop;
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
    public partial class PainelControle : IDisposable
    {
        public PainelViewModel Painel { get; set; } = new PainelViewModel();

        private FiltroDataViewModel filtro = new FiltroDataViewModel() { DataInical = new DateTime(DateTime.Now.Year, 01, 01), Datafinal = new DateTime(DateTime.Now.Year, 12, 31) };

        protected async override Task OnInitializedAsync()
        {
            interceptor.RegisterEvent();
            Painel = await http.POST<PainelViewModel>(Api.Painel, filtro);
        }

        public void Dispose()
        {
            interceptor.DisposeEvent();
        }

        public void VerTodos()
        {
            navigation.NavigateTo("/Usuario/Pedidos");
        }

        private async Task OnClick()
        {
            string mensagemErro = string.Empty;

            if (filtro.DataInical.Year != filtro.Datafinal.Year)
            {
                mensagemErro = validation[validationLocalizer.PesquisasMesmoAno];

            }
            else if (DateTime.Compare(filtro.DataInical, filtro.Datafinal) > 0)
            {
                mensagemErro = validation[validationLocalizer.DataInicialMaiorDataFinal];
            }

            if (string.IsNullOrEmpty(mensagemErro))
            {
                Painel = await http.POST<PainelViewModel>(Api.Painel, filtro);
            }
            else
            {
                NotificationService.Notify(PopUps.Error(validation[validationLocalizer.Erro], mensagemErro));
                await Task.CompletedTask;
            }
        }
    }
}
