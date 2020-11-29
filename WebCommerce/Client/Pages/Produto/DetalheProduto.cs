using BLL.Models.ViewModels;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using WebCommerce.Client.Services;
using WebCommerce.Client.Services.HTTP.Routes;

namespace WebCommerce.Client.Pages.Produto
{
    public partial class DetalheProduto : IDisposable
    {
        [Parameter]
        public string id { get; set; }

        public ProdutoViewModel produto { get; set; } = new ProdutoViewModel();

        protected override async Task OnParametersSetAsync()
        {
            interceptor.RegisterEvent();
            Notifier.Adicionar(nameof(DetalheProduto), Iniciar);
            await Iniciar();
        }

        public async Task Adicionar(int id)
        {
            if (await http.POST<bool>(Api.CarrinhoCompras, id))
            {
                navigation.NavigateTo($"/Compras/");
            }
        }

        public void Dispose()
        {
            interceptor.DisposeEvent();
        }

        public async Task Iniciar()
        {
            produto = await http.GET<ProdutoViewModel>($"{Api.Produto}/{id}");
            StateHasChanged();
        }
    }
}
