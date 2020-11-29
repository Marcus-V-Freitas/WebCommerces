using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using WebCommerce.Client.Services;
using WebCommerce.Client.Services.HTTP.Routes;

namespace WebCommerce.Client.Shared.Pagamentos
{
    public partial class Frete
    {
        private async Task RetornarValores()
        {
            CEP = CEP.Replace(" ", "");
            if (!string.IsNullOrEmpty(CEP) && CEP.Length == 8 && int.TryParse(CEP, out _))
            {
                frete = new BLL.Models.Frete();
                frete = await http.GET<BLL.Models.Frete>($"{Api.CarrinhoCompras}/Frete/{CEP}");
            }
            else
            {
                frete = null;
            }
        }
    }
}
