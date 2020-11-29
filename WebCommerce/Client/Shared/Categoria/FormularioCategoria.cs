using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using _categoria = BLL.Models.Categoria;

namespace WebCommerce.Client.Shared.Categoria
{
    public partial class FormularioCategoria
    {
        [Parameter]
        public _categoria categoria { get; set; } = new _categoria();

        [Parameter]
        public EventCallback<_categoria> EventRetornarCategoria { get; set; }

        public void Guardar()
        {
            EventRetornarCategoria.InvokeAsync(categoria);
        }
    }
}
