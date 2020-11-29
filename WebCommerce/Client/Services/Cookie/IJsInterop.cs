using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;

namespace WebCommerce.Client.Services.Cookie
{
    public interface IJsInterop
    {
        Task<string> GetCookie();

        Task MostrarMessagemErro(string menssagem);
        Task MostrarMessagemSucesso(string menssagem);
    }
}