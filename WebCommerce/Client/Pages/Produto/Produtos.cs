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
    public partial class Produtos : IDisposable
    {
        string[] cabecalho = { "ID", "Nome", "Valor" };
        string[] campos = { "ID", "Nome", "Valor" };

        private BLL.Models.Produto[] ListaProdutos;

        protected override async Task OnInitializedAsync()
        {
            interceptor.RegisterEvent();
            ListaProdutos = await http.GET<BLL.Models.Produto[]>(Api.Produto);
        }

        public async Task Buscar(string data)
        {
            ListaProdutos = await http.GET<BLL.Models.Produto[]>($"{Api.Produto}?data={data}");
        }

        public async Task Eliminar(string data)
        {
            await http.DELETE<bool>($"{Api.Produto}/{data}");

            ListaProdutos = await http.GET<BLL.Models.Produto[]>(Api.Produto);
        }

        public void Dispose()
        {
            interceptor.DisposeEvent();
        }
    }
}
