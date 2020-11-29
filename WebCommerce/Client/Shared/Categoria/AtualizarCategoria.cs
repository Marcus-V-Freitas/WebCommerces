using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using WebCommerce.Client.Services.HTTP.Routes;
using _categoria = BLL.Models.Categoria;

namespace WebCommerce.Client.Shared.Categoria
{
    public partial class AtualizarCategoria
    {
        [Parameter]
        public string Id { get; set; }

        public _categoria Categoria { get; set; } = new _categoria();

        protected override async Task OnParametersSetAsync()
        {
            Categoria = await http.GET<_categoria>($"{Api.Categoria}/{Id}");
        }

        public async Task Atualizar(_categoria categoria)
        {
            await http.PUT<bool>($"{Api.Categoria}/{Id}", categoria);
            navigation.NavigateTo("/Categorias");
        }
    }
}
