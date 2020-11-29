using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WebCommerce.Client.Shared.Usuario
{
    public partial class FormularioUsuario
    {
        [Parameter]
        public BLL.Models.Usuario Usuario { get; set; } = new BLL.Models.Usuario();

        [Parameter]
        public bool SenhaVisivel { get; set; } = true;
    }
}
