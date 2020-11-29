using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WebCommerce.Client.Services.HTTP.Routes;
using WebCommerce.Client.Services.Notificacoes;
using _usuario = BLL.Models.Usuario;
using validation = BLL.Library.Resources.Models.Validation;

namespace WebCommerce.Client.Shared.Usuario
{
    public partial class AtualizarUsuario
    {
        public _usuario UsuarioConta { get; set; }


        protected override async Task OnInitializedAsync()
        {
            UsuarioConta = await http.GET<_usuario>(Api.Usuario);
        }

        public async Task OnSubmit()
        {
            if (await http.PUT<bool>(Api.Usuario, UsuarioConta))
            {
                NotificationService.Notify(PopUps.Success(_localizer[validation.Sucesso], _localizer[validation.dadosContaSucesso]));
                navigation.NavigateTo("/Usuario/DashBoard");
            }
        }
    }
}
