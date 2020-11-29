using BLL.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Http;
using Microsoft.JSInterop;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using WebCommerce.Client.Services;

namespace WebCommerce.Client.Shared.Login
{
    public partial class Login
    {
        public BLL.Models.Usuario Usuario { get; set; }

        protected override async Task OnInitializedAsync()
        {
            Notifier.Adicionar(nameof(Login), Iniciar);
            await Iniciar();

        }

        private async Task Iniciar()
        {
            await Task.Delay(1);
            this.StateHasChanged();
        }

        public void IrParaLogin()
        {
            navigation.NavigateTo("/Login");
        }

        public void IrParaPainel()
        {
            navigation.NavigateTo("/Usuario/DashBoard");
        }
        public async Task IrParaLogout()
        {
            await AuthenticationService.Logout();
            Notifier.Atualizar();
        }


    }
}
