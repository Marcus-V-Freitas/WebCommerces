using BLL.Models;
using BLL.Models.ViewModels;
using Microsoft.JSInterop;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using WebCommerce.Client.Services;
using WebCommerce.Client.Services.HTTP.Routes;
using WebCommerce.Client.Services.Notificacoes;
using validation = BLL.Library.Resources.Models.Validation;

namespace WebCommerce.Client.Pages.Usuarios
{
    public partial class Cadastro : IDisposable
    {
        protected override void OnInitialized()
        {
            interceptor.RegisterEvent();
        }

        public UsuarioCadastroViewModel UsuarioEndereco { get; set; } = new UsuarioCadastroViewModel();

        public void Dispose()
        {
            interceptor.DisposeEvent();
        }

        private async Task OnSubmit()
        {
            await http.AdicionarCSRF();
            if (await http.POST<bool>(Api.Usuario, UsuarioEndereco))
            {
                NotificationService.Notify(PopUps.Success(validation.Sucesso, _localizer[validation.CadastradoSucesso]));
                await Task.Delay(5);
                navigation.NavigateTo("/Login");
            }
        }
    }
}
