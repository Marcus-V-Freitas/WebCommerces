using System;
using System.Threading.Tasks;

namespace WebCommerce.Client.Services.Cookie
{
    public interface IBrowserCookieService
    {
        Task<string> Get(Func<string, bool> filterCookie);
    }
}