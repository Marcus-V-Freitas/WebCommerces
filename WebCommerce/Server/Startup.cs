using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using Microsoft.Extensions.Options;
using Microsoft.AspNetCore.Http;
using AutoMapper;
using BLL.Library.AutoMapper;
using WebCommerce.Server.Services.Exceptions;
using WebCommerce.Server.Dependency;
using WebCommerce.Server.Services.Middleware;

namespace WebCommerce.Server
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }


        public void ConfigureServices(IServiceCollection services)
        {
            services.AddSwagger();

            services.AddLogging();  //API - Logging

            services.AddMemoryCache(); //Cache de Memória

            services.AddPolicies(); //Adiciona políticas

            services.AddResponseCache(); //Cache de Saída

            services.AddNewtonsoftJson(); //Permite Serialização de objetos com referências a si mesmo

            services.AddAutoMapper(config => config.AddProfile<MappingProfile>()); //AutoMapper

            services.AddConnectionContext(Configuration); // Conexão com a Base de Dados

            services.AddHttpContextAccessor(); //Implementa serviços de Sessão

            services.AddRepository(); //Repository

            services.AddServicesDependency(); //Serviços relacionados a regras de negócios

            services.AddJWT(Configuration); //Token JWT

            services.AddDataCompression();// Adiciona Compressão de dados

            services.AddCSRF(); //AntiForgeryToken - Evitar ataques vindos de fora da página

            services.AddSessionCookie();

            services.AddControllersWithViews();

            services.AddRazorPages();

            services.AddDistributedMemoryCache(); //Habilita o cache de memória distribuído

            services.AddGlobalization(); //Globalização
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)  //Configuração do Pipeline HTTP
        {
            app.UseResponseCompression();

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseWebAssemblyDebugging(); //Debug
                app.UseSwagger(); //Adiciona o Swagger
                app.UseSwaggerUI(cfg =>
                {
                    cfg.SwaggerEndpoint("/swagger/v1/swagger.json", "WebCommerce - V1.0"); //Localidade do Arquivo 1.0
                });
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }


            app.UseHttpsRedirection(); //Redirecionamento seguro
            app.UseCors("CorsPolicy");
            app.UseDefaultFiles(); //Ativa o uso de arquivos padrão

            //Habilitar Cache do navegador para armazenamento de arquivos estáticos (JS,CSS,imagens, etc.)
            app.UseStaticFiles(new StaticFileOptions
            {
                OnPrepareResponse = (context) =>
                {
                    var headers = context.Context.Response.GetTypedHeaders();

                    headers.CacheControl = new Microsoft.Net.Http.Headers.CacheControlHeaderValue
                    {
                        Public = true,
                        MaxAge = TimeSpan.FromDays(365)
                    };
                }
            });
            app.UseBlazorFrameworkFiles();
            app.UseMiddleware<CsrfTokenCOokieMiddleware>(); //AntiForgeryToken - Evitar ataques maliciosos CSRF por método Post (Valido para toda a aplicação)
            app.UseSession(); //Métodos usado para fazer funcionar a Sessão

            var cookiePolicyOptions = new CookiePolicyOptions
            {
                MinimumSameSitePolicy = SameSiteMode.Strict,
            };
            app.UseCookiePolicy(cookiePolicyOptions);
            app.UseResponseCaching();  //Habilita o armazenamento de Cache de Saída


            var options = app.ApplicationServices.GetService<IOptions<RequestLocalizationOptions>>();
            app.UseRequestLocalization(options.Value);
            app.UseMiddleware<GlobalExceptionHandlerMiddleware>(); // Centralização do tratamento de exceções
            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapRazorPages();
                endpoints.MapControllers();
                endpoints.MapFallbackToFile("index.html");
            });
        }
    }
}
