using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System;
using System.Threading.Tasks;

namespace WebCommerce.Client.Services.Cookie
{
    /// <summary>
    /// Utility class for calling JS function declared in index.html
    /// </summary>
    public class JsInterop : IJsInterop
    {
        private readonly IJSRuntime jsRuntime;

        public JsInterop(IJSRuntime jsRuntime)
        {
            this.jsRuntime = jsRuntime;
        }

        public async Task<string> GetCookie()
        {
            return await jsRuntime.InvokeAsync<string>("getDocumentCookie");
        }

        public async Task MostrarMessagemErro(string menssagem)
        {
            await MostrarMensagem("Error", menssagem, "error");
        }

        public async Task MostrarMessagemSucesso(string menssagem)
        {
            await MostrarMensagem("Sucesso", menssagem, "success");
        }

        private async ValueTask MostrarMensagem(string titulo, string menssagem, string tipoMensagem)
        {
            await jsRuntime.InvokeVoidAsync("Swal.fire", titulo, menssagem, tipoMensagem);
        }
    }
}
