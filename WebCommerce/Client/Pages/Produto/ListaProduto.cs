using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Json;
using System.Threading.Tasks;
using WebCommerce.Client.Services.HTTP.Routes;

namespace WebCommerce.Client.Pages.Produto
{
    public partial class ListaProduto : IDisposable
    {
        [Parameter]
        public string Id { get; set; }

        public BLL.Models.Produto[] produtos { get; set; }

        public void Dispose()
        {
            interceptor.DisposeEvent();
        }

        protected override async Task OnParametersSetAsync()
        {
            interceptor.RegisterEvent();
            produtos = await http.GET<BLL.Models.Produto[]>($"{Api.Produto}/Categoria/{Id}");
        }
    }
}
