using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using WebCommerce.Client.Services.HTTP.Routes;

namespace WebCommerce.Client.Shared.Categoria
{
    public partial class CadastrarCategoria
    {
        public async Task Cadastrar(BLL.Models.Categoria produto)
        {
            await http.POST<bool>(Api.Categoria, produto);

            navigation.NavigateTo("/Categorias");

        }
    }
}
