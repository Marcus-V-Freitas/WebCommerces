using BLL.Models;
using BLL.Models.DTOs;
using BLL.Models.ViewModels;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using WebCommerce.Client.Services;
using WebCommerce.Client.Services.HTTP.Routes;
using WebCommerce.Client.Services.Notificacoes;
using validationLocalizer = BLL.Library.Resources.Models.Validation;


namespace WebCommerce.Client.Pages.Usuarios
{
    public partial class Logins : IDisposable
    {
        public LoginViewModel Usuario { get; set; } = new LoginViewModel();

        public AuthResponseDto AuthResponseDto { get; set; } = new AuthResponseDto();

        public void Dispose()
        {
            interceptor.DisposeEvent();
        }

        public async Task Guardar()
        {
            interceptor.RegisterEvent();
            await http.AdicionarCSRF();
            AuthResponseDto = await http.POST<AuthResponseDto>($"{Api.Usuario}/Token", Usuario);

            if (AuthResponseDto != null && AuthResponseDto.IsAuthSuccessful)
            {
                await AuthenticationService.Login(AuthResponseDto);
                navigation.NavigateTo("/Finalizar");

            }
        }

    }
}
