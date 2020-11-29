using System;
using System.Net.Http;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Text;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using WebCommerce.Client.Services;
using Microsoft.AspNetCore.Components.Authorization;
using DAL.Repositories.EntityFramework.Compras;
using WebCommerce.Client.Services.Cookie;
using WebCommerce.Client.Services.HttpRepository;
using Blazored.LocalStorage;
using Tewr.Blazor.FileReader;
using WebCommerce.Client.Services.AuthProviders;
using WebCommerce.Client.Services.Autenticacao.Token;
using Toolbelt.Blazor.Extensions.DependencyInjection;
using Microsoft.AspNetCore.Components.WebAssembly.Services;
using Microsoft.JSInterop;
using System.Globalization;
using Radzen;
using WebCommerce.Client.Services.HTTP;
using WebCommerce.Client.Services.Notificacoes;
using WebCommerce.Client.Services.HTTP.Interface;
using WebCommerce.Client.Services.HTTP.Repository;

namespace WebCommerce.Client
{
    public static class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("app");
            builder.Services.AddScoped<LazyAssemblyLoader>();
            builder.Services.AddScoped<AuthenticationStateProvider, AuthStateProvider>();
            builder.Services.AddOptions();
            builder.Services.AddAuthorizationCore();
            builder.Services.AddHttpContextAccessor();
            builder.Services.AddScoped<ServicoNotificador>();
            builder.Services.AddSingleton<IJsInterop, JsInterop>();
            builder.Services.AddSingleton<IBrowserCookieService, BrowserCookieService>();
            builder.Services.AddFileReaderService(o => o.UseWasmSharedBuffer = true);
            builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();
            builder.Services.AddBlazoredLocalStorage();
            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) }
                .EnableIntercept(sp));
            builder.Services.AddScoped<DialogService>();
            builder.Services.AddScoped<NotificationService>();
            builder.Services.AddScoped<TooltipService>();
            builder.Services.AddScoped<ContextMenuService>();
            builder.Services.AddScoped<RefreshTokenService>();
            builder.Services.AddHttpClientInterceptor();
            builder.Services.AddScoped<HttpInterceptorService>();
            builder.Services.AddScoped<IHttpService, HttpService>();
            builder.Services.AddLocalization();
            var host = builder.Build();
            var js = host.Services.GetRequiredService<IJSRuntime>();
            var cultura = await js.InvokeAsync<string>("cultura.get");

            if (cultura == null)
            {
                var culturaPadrao = new CultureInfo("pt-BR");
                CultureInfo.DefaultThreadCurrentCulture = culturaPadrao;
                CultureInfo.DefaultThreadCurrentUICulture = culturaPadrao;
            }
            else
            {
                var culturaUsuario = new CultureInfo(cultura);
                CultureInfo.DefaultThreadCurrentCulture = culturaUsuario;
                CultureInfo.DefaultThreadCurrentUICulture = culturaUsuario;
            }

            await builder.Build().RunAsync();
        }
    }
}
