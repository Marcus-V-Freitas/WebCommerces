using BLL.Library.ExtensionsMethods;
using BLL.Models;
using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using WebCommerce.Client.Services.HTTP.Routes;
using WebCommerce.Client.Services.Notificacoes;
using validation = BLL.Library.Resources.Models.Validation;

namespace WebCommerce.Client.Pages.Produto
{
    public partial class ProdutoComentario
    {
        public UsuarioClassificacaoProduto ClassificacaoProduto { get; set; } = new UsuarioClassificacaoProduto();

        [Parameter]
        public int ProdutoID { get; set; }

        public async Task Comentar()
        {

            ClassificacaoProduto.Use(x =>
            {
                x.DataRegistro = DateTime.Now;
                x.ProdutoClassificacaoId = ProdutoID;
                x.PermitirVisualizar = false;
            });

            if (await http.POST<bool>($"{Api.Produto}/Classificacao", ClassificacaoProduto))
            {
                NotificationService.Notify(PopUps.Success(validationLocalizer[validation.Sucesso], validationLocalizer[validation.ComentarioEnviado]));
                Notifier.Atualizar();
            }

        }

    }
}
