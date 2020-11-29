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

namespace WebCommerce.Client.Pages.CarrinhoCompras
{
    public partial class Compras : IDisposable
    {
        public CarrinhoComprasViewModel carrinho { get; set; } = new CarrinhoComprasViewModel();


        protected async override Task OnInitializedAsync()
        {
            Notificar();
            interceptor.RegisterEvent();
            carrinho = await http.GET<CarrinhoComprasViewModel>(Api.CarrinhoCompras);

        }

        private async Task Adicionar(int id)
        {
            if (await http.POST<bool>(Api.CarrinhoCompras, id))
            {
                await PreencherCarrinho();
            }
        }

        public async Task Remover(int id)
        {
            await http.PUT<int>(Api.CarrinhoCompras, id);
            await PreencherCarrinho();
        }

        public async Task RemoverTodos(int id)
        {
            await http.DELETE<int>(Api.CarrinhoCompras);
            await PreencherCarrinho();
        }

        public async Task PreencherCarrinho()
        {
            Notificar();
            carrinho = await http.GET<CarrinhoComprasViewModel>(Api.CarrinhoCompras);
        }

        public void Notificar()
        {
            Notifier.Atualizar();
        }

        public void Dispose()
        {
            interceptor.DisposeEvent();
        }
    }
}
