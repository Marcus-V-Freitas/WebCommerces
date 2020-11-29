using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using WebCommerce.Client.Services.HTTP.Routes;

namespace WebCommerce.Client.Shared.MenuCategoria
{
    public partial class MenuCategoria
    {

        public BLL.Models.Categoria[] categorias { get; set; }
        public BLL.Models.Produto[] produtos { get; set; }

        protected override async Task OnInitializedAsync()
        {
            categorias = await http.GET<BLL.Models.Categoria[]>(Api.Categoria);
        }

        public void ProdutosCategoria(int id)
        {
            navigation.NavigateTo($"ListaProdutos/{id}");

        }
    }
}
