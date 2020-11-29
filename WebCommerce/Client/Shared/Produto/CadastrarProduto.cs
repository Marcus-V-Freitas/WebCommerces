using BLL.Models;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using WebCommerce.Client.Services.HTTP.Routes;

namespace WebCommerce.Client.Shared.Produto
{
    public partial class CadastrarProduto
    {
        public BLL.Models.Categoria[] Categorias { get; set; }


        public async Task Cadastrar(BLL.Models.Produto produto)
        {
            await http.POST<bool>(Api.Produto, produto);

            navigation.NavigateTo("/Produtos");

        }
    }
}
