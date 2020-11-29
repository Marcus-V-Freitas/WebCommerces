using BLL.Models;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using WebCommerce.Client.Services.HTTP.Routes;
using _categoria = BLL.Models.Categoria;

namespace WebCommerce.Client.Pages.Categoria
{
    public partial class Categorias : IDisposable
    {

        private _categoria[] ListaCategorias;

        protected override async Task OnInitializedAsync()
        {
            interceptor.RegisterEvent();
            ListaCategorias = await http.GET<_categoria[]>(Api.Categoria);
        }

        string[] campos = { nameof(_categoria.Id), nameof(_categoria.Nome), nameof(_categoria.Descricao) };

        string[] cabecalho = { nameof(_categoria.Id), nameof(_categoria.Nome), nameof(_categoria.Descricao) };

        public async Task Buscar(string data)
        {
            ListaCategorias = await http.GET<_categoria[]>($"{Api.Categoria}?data={data}");
        }

        public async Task Eliminar(string data)
        {
            await http.DELETE<bool>($"{Api.Categoria}/{data}");

            ListaCategorias = await http.GET<_categoria[]>(Api.Categoria);
        }

        public void Dispose()
        {
            interceptor.DisposeEvent();
        }
    }
}
