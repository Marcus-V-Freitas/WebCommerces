using BLL.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using WebCommerce.Client.Services.HTTP.Routes;

namespace WebCommerce.Client.Shared.Produto
{
    public partial class FormularioProduto
    {
#pragma warning disable
        ElementReference fuFoto;
#pragma warning restore
        [Parameter]
        public BLL.Models.Produto produto { get; set; } = new BLL.Models.Produto();

        [Parameter]
        public string Caminho { get; set; }


        public List<BLL.Models.Categoria> Categorias { get; set; }


        [Parameter]
        public EventCallback<BLL.Models.Produto> eventRetornarProduto { get; set; }

        public async Task Guardar()
        {
            produto.ImageThumbnailUrl = await jsRuntime.InvokeAsync<string>("ObterImagem");


            await eventRetornarProduto.InvokeAsync(produto);

        }

        protected async Task preview()
        {
            await jsRuntime.InvokeVoidAsync("previewData");
        }

        protected override async Task OnInitializedAsync()
        {
            Categorias = await http.GET<List<BLL.Models.Categoria>>(Api.Categoria);
        }

        public void Change(ChangeEventArgs e)
        {
            produto.CategoriaId = Convert.ToInt32(e.Value.ToString());

        }

    }
}
