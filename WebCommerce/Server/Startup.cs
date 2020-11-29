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

            services.AddMemoryCache(); //Cache de Mem�ria

            services.AddPolicies(); //Adiciona pol�ticas

            services.AddResponseCache(); //Cache de Sa�da

            services.AddNewtonsoftJson(); //Permite Serializa��o de objetos com refer�ncias a si mesmo

            services.AddAutoMapper(config => config.AddProfile<MappingProfile>()); //AutoMapper

            services.AddConnectionContext(Configuration); // Conex�o com a Base de Dados

            services.AddHttpContextAccessor(); //Implementa servi�os de Sess�o

            services.AddRepository(); //Repository

            services.AddServicesDependency(); //Servi�os relacionados a regras de neg�cios

            services.AddJWT(Configuration); //Token JWT

            services.AddDataCompression();// Adiciona Compress�o de dados

            services.AddCSRF(); //AntiForgeryToken - Evitar ataques vindos de fora da p�gina

            services.AddSessionCookie();

            services.AddControllersWithViews();

            services.AddRazorPages();

            services.AddDistributedMemoryCache(); //Habilita o cache de mem�ria distribu�do

            services.AddGlobalization(); //Globaliza��o
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)  //Configura��o do Pipeline HTTP
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
            app.UseDefaultFiles(); //Ativa o uso de arquivos padr�o

            //Habilitar Cache do navegador para armazenamento de arquivos est�ticos (JS,CSS,imagens, etc.)
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
            app.UseMiddleware<CsrfTokenCOokieMiddleware>(); //AntiForgeryToken - Evitar ataques maliciosos CSRF por m�todo Post (Valido para toda a aplica��o)
            app.UseSession(); //M�todos usado para fazer funcionar a Sess�o

            var cookiePolicyOptions = new CookiePolicyOptions
            {
                MinimumSameSitePolicy = SameSiteMode.Strict,
            };
            app.UseCookiePolicy(cookiePolicyOptions);
            app.UseResponseCaching();  //Habilita o armazenamento de Cache de Sa�da


            var options = app.ApplicationServices.GetService<IOptions<RequestLocalizationOptions>>();
            app.UseRequestLocalization(options.Value);
            app.UseMiddleware<GlobalExceptionHandlerMiddleware>(); // Centraliza��o do tratamento de exce��es
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
