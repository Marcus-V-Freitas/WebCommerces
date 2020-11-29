using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebCommerce.Client.Shared.Mensagens
{
    public partial class MensagemExclusao
    {
        [Parameter]
        public string Id { get; set; }

        [Parameter]
        public EventCallback<string> eventEliminar { get; set; }

        public void Eliminar()
        {
            eventEliminar.InvokeAsync(Id);
        }
    }
}
