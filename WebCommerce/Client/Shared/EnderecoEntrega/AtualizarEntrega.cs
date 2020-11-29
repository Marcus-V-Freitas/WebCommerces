using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebCommerce.Client.Services.HTTP.Routes;
using WebCommerce.Client.Services.Notificacoes;
using _endereco = BLL.Models.EnderecoEntrega;
using validation = BLL.Library.Resources.Models.Validation;

namespace WebCommerce.Client.Shared.EnderecoEntrega
{
    public partial class AtualizarEntrega : IDisposable
    {
        public _endereco Endereco { get; set; }

        protected override async Task OnInitializedAsync()
        {
            interceptor.RegisterEvent();
            Endereco = await http.GET<_endereco>(Api.Endereco);
        }

        public async Task OnSubmit()
        {
            if (await http.PUT<bool>(Api.Endereco, Endereco))
            {
                NotificationService.Notify(PopUps.Success(_localizer[validation.Sucesso], _localizer[validation.EnderecoSucesso]));
                navigation.NavigateTo("/Usuario/DashBoard");
            }
        }

        public void Dispose()
        {
            interceptor.DisposeEvent();
        }
    }
}
