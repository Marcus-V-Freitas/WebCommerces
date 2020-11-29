using DAL.Repositories.EntityFramework.Compras;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using WebCommerce.Client.Services;
using WebCommerce.Client.Services.HTTP.Routes;

namespace WebCommerce.Client.Shared.SumarioCompras
{
    public partial class SumarioCompras
    {
        public CarrinhoComprasViewModel Carrinho { get; set; } = new CarrinhoComprasViewModel();


        protected override async Task OnInitializedAsync()
        {
            Notifier.Adicionar(nameof(SumarioCompras), Iniciar);
            await Iniciar();
        }

        public void IrParaCarrinho()
        {
            navigation.NavigateTo("/Compras");
        }

        public async Task Iniciar()
        {
            Carrinho = await http.GET<CarrinhoComprasViewModel>(Api.CarrinhoCompras);
            StateHasChanged();
        }

    }
}
