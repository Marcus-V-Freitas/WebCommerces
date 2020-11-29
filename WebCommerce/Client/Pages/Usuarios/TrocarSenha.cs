using BLL.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using WebCommerce.Client.Services;
using WebCommerce.Client.Services.Notificacoes;
using validationLocalizer = BLL.Library.Resources.Models.Validation;
using painelLocalizer = BLL.Library.Resources.Pages.DashBoard.Componentes;
using WebCommerce.Client.Services.HTTP.Routes;

namespace WebCommerce.Client.Pages.Usuarios
{
    public partial class TrocarSenha : IDisposable
    {
        public SenhaViewModel Model { get; set; } = new SenhaViewModel();

        protected override void OnInitialized()
        {
            interceptor.RegisterEvent();
        }


        public async Task OnSubmit()
        {
            if (await http.PUT<bool>($"{Api.Usuario}/TrocarSenha", Model))
            {
                NotificationService.Notify(PopUps.Success(validation[validationLocalizer.Sucesso], localizer[painelLocalizer.SenhaTrocada]));
                navigation.NavigateTo("/Usuario/DashBoard");
            }
        }

        public void Dispose()
        {
            interceptor.DisposeEvent();
        }
    }
}
