using BLL.Models.ViewModels;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using WebCommerce.Client.Services;
using WebCommerce.Client.Services.HTTP.Routes;

namespace WebCommerce.Client.Shared.CarrinhoCompras
{
    public partial class CarrinhoCompras
    {
        [Parameter]
        public int ID { get; set; }

        public ProdutoViewModel produto { get; set; } = new ProdutoViewModel();

        protected override async Task OnInitializedAsync()
        {
            produto = await http.GET<ProdutoViewModel>($"{Api.Produto}/{ID}");
        }

        public async Task Adicionar(int id)
        {
            if (await http.POST<bool>(Api.CarrinhoCompras, id))
            {
                navigation.NavigateTo($"/Compras/");
            }
        }

        public void Detalhes(int id)
        {
            navigation.NavigateTo($"/DetalheProduto/{id}");
        }
    }
}
