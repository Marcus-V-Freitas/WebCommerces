using BLL.Library.ExtensionsMethods;
using BLL.Models;
using DAL.Repositories.EntityFramework.Compras;
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

namespace WebCommerce.Client.Pages.Venda
{
    public partial class Finalizar : IDisposable
    {
        private CarrinhoComprasViewModel carrinho = new CarrinhoComprasViewModel();

        public void Dispose()
        {
            interceptor.DisposeEvent();
        }

        protected override async Task OnInitializedAsync()
        {
            try
            {
                interceptor.RegisterEvent();
                carrinho = await http.GET<CarrinhoComprasViewModel>(Api.CarrinhoCompras);
                Notifier.Atualizar();
            }
            catch (Exception ex)
            {
                await js.MostrarMessagemErro(ex.TratarExececao());
            }
        }
    }
}
