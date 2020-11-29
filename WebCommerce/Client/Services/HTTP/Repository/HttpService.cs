using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Blazored.LocalStorage;
using Microsoft.Extensions.Localization;
using Newtonsoft.Json;
using Radzen;
using WebCommerce.Client.Services.Cookie;
using WebCommerce.Client.Services.HTTP.Interface;
using WebCommerce.Client.Services.Notificacoes;
using JsonSerializer = Newtonsoft.Json;
using validation = BLL.Library.Resources.Models.Validation;

namespace WebCommerce.Client.Services.HTTP.Repository
{
    public class HttpService : IHttpService
    {
        private readonly HttpClient _http;
        private readonly ILocalStorageService _localStorage;
        private readonly NotificationService _notification;
        private readonly IBrowserCookieService _browserCookieService;
        private readonly IStringLocalizer<validation> _localizer;

        public HttpService(HttpClient http,
                           ILocalStorageService localStorage,
                           NotificationService notification,
                           IBrowserCookieService browserCookieService,
                           IStringLocalizer<validation> localizer)
        {
            _http = http;
            _localStorage = localStorage;
            _notification = notification;
            _browserCookieService = browserCookieService;
            _localizer = localizer;
        }

        public async Task<T> GET<T>(string Url)
        {
            return await SendBlazorJsonAsync<T>(HttpMethod.Get, Url);
        }

        public async Task<T> POST<T>(string Url, object content = null)
        {
            return await SendBlazorJsonAsync<T>(HttpMethod.Post, Url, content);
        }

        public async Task<T> PUT<T>(string Url, object content = null)
        {
            return await SendBlazorJsonAsync<T>(HttpMethod.Put, Url, content);
        }

        public async Task<T> DELETE<T>(string Url)
        {
            return await SendBlazorJsonAsync<T>(HttpMethod.Delete, Url);
        }


        private async Task<T> SendBlazorJsonAsync<T>(HttpMethod method,
                                                string Url,
                                                object content = null,
                                                bool UsarReponse = true)
        {
            HttpResponseMessage response = null;

            await AdicionarToken();

            if (content == null)
            {
                response = await _http.SendAsync(new HttpRequestMessage(method, Url));

            }
            else
            {
                response = await _http.SendAsync(new HttpRequestMessage(method, Url)
                {
                    Content = new StringContent(JsonConvert.SerializeObject(content, Formatting.None,
                                                       new JsonSerializerSettings()
                                                       {
                                                           ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                                                       }), Encoding.UTF8, "application/json")
                });
            }
            var CounteudoString = await response.Content.ReadAsStringAsync();

            try
            {
                return JsonConvert.DeserializeObject<T>(CounteudoString);
            }
            catch (Exception ex)
            {
                await LogError(UsarReponse ? CounteudoString : $"{ex.Message} - {ex.StackTrace}");
            }
            return default;
        }

        private async Task LogError(string response)
        {
            await Task.Delay(1);
            _notification.Notify(PopUps.Error(_localizer[validation.Erro], response));
        }

        public async Task AdicionarCSRF()
        {
            string csrfCookieValue = await _browserCookieService.Get(c => c.Equals("CSRF-TOKEN"));
            if (csrfCookieValue != null)
            {
                RemoverHeader("X-CSRF-TOKEN");
                _http.DefaultRequestHeaders.Add("X-CSRF-TOKEN", csrfCookieValue);
            }
        }

        private async Task AdicionarToken()
        {
            var token = await _localStorage.GetItemAsync<string>("authToken");
            if (token is not null)
            {
                _http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("bearer", token);
            }
        }

        private void RemoverHeader(string header)
        {
            if (_http.DefaultRequestHeaders.Contains(header))
            {
                _http.DefaultRequestHeaders.Remove(header);
            }
        }

    }
}
