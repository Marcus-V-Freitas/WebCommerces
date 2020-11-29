using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebCommerce.Client.Shared.EnderecoEntrega
{
    public partial class FormularioEntrega
    {
        [Parameter]
        public BLL.Models.EnderecoEntrega Endereco { get; set; } = new BLL.Models.EnderecoEntrega();

    }
}
