using DAL.Repositories.EntityFramework.Compras;
using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebCommerce.Client.Shared.CarrinhoCompras
{
    public partial class ItensCarrinhoCompras
    {
        [Parameter]
        public CarrinhoComprasViewModel carrinho { get; set; } = new CarrinhoComprasViewModel();

        [Parameter]
        public EventCallback<int> EventAdicionar { get; set; }

        [Parameter]
        public EventCallback<int> EventRemover { get; set; }

        [Parameter]
        public EventCallback<int> EventRemoverTodos { get; set; }

        [Parameter]
        public EventCallback<bool> EventCriarVenda { get; set; }


        public void Adicionar(int id)
        {
            EventAdicionar.InvokeAsync(id);
        }

        public void Remover(int id)
        {
            EventRemover.InvokeAsync(id);
        }

        public void RemoverTodos(int id)
        {
            EventRemoverTodos.InvokeAsync(id);
        }

        public void CriarVenda(bool bRealizar)
        {
            EventCriarVenda.InvokeAsync(bRealizar);
        }
    }
}
