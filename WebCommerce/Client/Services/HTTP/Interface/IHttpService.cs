using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace WebCommerce.Client.Services.HTTP.Interface
{
    public interface IHttpService
    {
        //private Task<T> SendBlazorJsonAsync<T>(HttpMethod method, string Url, object content = null, bool UsarReponse = false);

        public Task AdicionarCSRF();

        public Task<T> GET<T>(string Url);

        public Task<T> POST<T>(string Url, object content = null);

        public Task<T> PUT<T>(string Url, object content = null);

        public Task<T> DELETE<T>(string Url);
    }
}
