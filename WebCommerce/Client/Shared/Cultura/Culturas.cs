using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Globalization;
using Microsoft.JSInterop;
using System.Threading;
using WebCommerce.Client.Services;
using System.Net.Http;
using WebCommerce.Client.Services.HTTP.Routes;

namespace WebCommerce.Client.Shared.Cultura
{
    public partial class Culturas
    {
        private readonly CultureInfo[] culturas = new[]
 {
        new CultureInfo("pt-BR"),
        new CultureInfo("en-US"),
        new CultureInfo("es-ES")
        };

        private CultureInfo cultura
        {
            get => CultureInfo.CurrentUICulture;
            set
            {
                if (CultureInfo.CurrentUICulture != value)
                {
                    new Task(async () =>
                    {
                        await MudarCultura(value.Name);

                        var jsInProcessRuntime = (IJSInProcessRuntime)jsRuntime;
                        jsInProcessRuntime.InvokeVoid("cultura.set", value.Name);
                        navigation.NavigateTo(navigation.Uri, forceLoad: true);

                    }).Start();
                }
            }
        }

        public async Task<bool> MudarCultura(string culturaEscolhida)
        {
            return await http.POST<bool>(Api.Cultura, culturaEscolhida);
        }
    }
}
