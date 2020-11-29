using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using WebCommerce.Client.Services.HTTP.Routes;

namespace WebCommerce.Client.Shared.Produto
{
    public partial class AtualizarProduto
    {
        [Parameter]
        public string Id { get; set; }

        public BLL.Models.Produto Produto { get; set; } = new BLL.Models.Produto();


        protected override async Task OnParametersSetAsync()
        {
            Produto = await http.GET<BLL.Models.Produto>($"{Api.Produto}/{Id}?viewModel=false");
        }


        public async Task Atualizar(BLL.Models.Produto produto)
        {
            await http.PUT<bool>($"{Api.Produto}/{Id}", produto);
            navigation.NavigateTo("/Produtos");
        }
    }
}
