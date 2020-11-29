using BLL.Library.Cache;
using BLL.Library.Cookies;
using BLL.Library.Gerenciador.Frete;
using BLL.Library.Pagamento;
using DAL.Repositories.EntityFramework;
using DAL.Repositories.EntityFramework.Compras;
using DAL.Repositories.EntityFramework.Context;
using DAL.Repositories.EntityFramework.Interfaces;
using DAL.Repositories.EntityFramework.Repository;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.PlatformAbstractions;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using WebCommerce.Server.Services.Pagamento;
using WebCommerce.Server.Services.Exceptions;
using WebCommerce.Server.Services.Frete;
using WebCommerce.Server.Services.HTTP.Interface;
using WebCommerce.Server.Services.HTTP.Repository;

namespace WebCommerce.Server.Dependency
{
    public static class StartupDependency
    {
        public static IServiceCollection AddRepository(this IServiceCollection services)
        {
            services.AddScoped<IProdutoRepository, ProdutoRepository>();
            services.AddScoped<ICategoriaRepository, CategoriaRepository>();
            services.AddScoped<IVendaRepository, VendaRepository>();
            services.AddScoped<IUsuarioRepository, UsuarioRepository>();
            services.AddScoped<IPedidoRepository, PedidoRepository>();
            services.AddScoped<IPedidoSituacaoRepository, PedidoSituacaoRepository>();
            services.AddScoped<IEstoqueReppository, EstoqueRepository>();
            services.AddScoped<IMarcaCartaoCreditoRepository, MarcaCartaoCreditoRepository>();
            services.AddScoped<IMovimentacaoRepository, MovimentacaoRepository>();
            services.AddScoped<IEnderecoEntregaRepository, EnderecoEntregaRepository>();
            services.AddScoped<IPainelRepository, PanelRepository>();
            return services;
        }


        public static IServiceCollection AddJWT(this IServiceCollection services, IConfiguration Configuration)
        {
            var jwtSettings = Configuration.GetSection("JwtSettings");
            services.AddAuthentication(opt =>
            {
                opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }
                    ).AddJwtBearer(options =>
                    {
                        options.Authority = "";
                        options.Audience = "";
                        options.TokenValidationParameters = new TokenValidationParameters
                        {
                            ValidateIssuer = true,
                            ValidateAudience = true,
                            ValidateLifetime = true,
                            ValidateIssuerSigningKey = true,
                            ValidIssuer = jwtSettings.GetSection("validIssuer").Value,
                            ValidAudience = jwtSettings.GetSection("validAudience").Value,
                            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.GetSection("securityKey").Value))
                        };
                    });

            return services;
        }


        public static IServiceCollection AddResponseCache(this IServiceCollection services)
        {
            services.AddMvc((options) =>
            {
                options.CacheProfiles.Add("default", new CacheProfile()
                {
                    Duration = 60,
                    Location = ResponseCacheLocation.None
                });
                options.CacheProfiles.Add("CachePrincipal", new CacheProfile()
                {
                    Duration = 3600,
                    Location = ResponseCacheLocation.Client,

                });
                options.CacheProfiles.Add("CacheMinimo", new CacheProfile()
                {
                    Duration = 60,
                    Location = ResponseCacheLocation.Client,
                });
            });
            services.AddResponseCaching();
            return services;
        }


        public static IServiceCollection AddGlobalization(this IServiceCollection services)
        {
            services.AddLocalization();  //Permite Uso de Recursos externos

            services.Configure<RequestLocalizationOptions>(opts =>
            {
                //Lista de Idiomas suportados
                var supportedCultures = new List<CultureInfo>
                {
                new CultureInfo("pt-BR"),
                new CultureInfo("en-US"),
                new CultureInfo("es-ES")
             };

                //Define língua padrão
                opts.DefaultRequestCulture = new RequestCulture("pt-BR");

                // Formata Números, Datas, etc.
                opts.SupportedCultures = supportedCultures;

                // UI Strings.
                opts.SupportedUICultures = supportedCultures;


                //Especifica os provedores válidos para alteração da cultura
                opts.RequestCultureProviders = new List<IRequestCultureProvider>
                    {
                    new CookieRequestCultureProvider()
                };
            });
            return services;
        }

        /// <summary>
        /// Swagger - Documentação das API's
        /// </summary>
        public static IServiceCollection AddSwagger(this IServiceCollection services)
        {
            services.AddSwaggerGen(cfg =>
            {
                cfg.ResolveConflictingActions(apiDescription => apiDescription.First()); //Resolver Conflitos de Versão/Rotas (Pega a primeira)

                cfg.SwaggerDoc("v1", new OpenApiInfo { Title = "WebCommerce", Version = "v1" });


                cfg.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
                {
                    Description = "",
                    In = ParameterLocation.Header,
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    Scheme = "Bearer"
                });
                cfg.OperationFilter<SecurityRequirementsOperationFilter>();


                var caminhoProjeto = PlatformServices.Default.Application.ApplicationBasePath;

                var nomeProjeto = $"{PlatformServices.Default.Application.ApplicationName}.xml";

                var caminhoArquivoXMLComentario = Path.Combine(caminhoProjeto, nomeProjeto);

                cfg.IncludeXmlComments(caminhoArquivoXMLComentario); //Incluir comentários XML na documentação UI do Swagger
            });
            return services;
        }

        public static IServiceCollection AddServicesDependency(this IServiceCollection services)
        {
            services.AddScoped<HttpClient>();
            services.AddScoped<Cookie>();
            services.AddScoped<CookieCompras>();
            services.AddScoped<CookieFrete>();
            services.AddScoped<PagamentoServico>();
            services.AddScoped<CarrinhoCompras>();
            services.AddScoped<GerenciarPagarMe>();
            services.AddScoped<CalcularPacote>();
            services.AddScoped<WSCorreiosCalcularFrete>();
            services.AddScoped(typeof(IServiceMemoryCache<>), typeof(ServiceMemoryCache<>));  //Serviço genêrico de Cache de Memória
            services.AddScoped<ITokenService, TokenService>();
            services.AddTransient<GlobalExceptionHandlerMiddleware>(); //Tratamento global de exceção
            return services;
        }

        public static IServiceCollection AddPolicies(this IServiceCollection services)
        {
            services.AddCors(policy =>
            {
                policy.AddPolicy("CorsPolicy", opt => opt
                .AllowAnyOrigin()
                .AllowAnyHeader()
                .AllowAnyMethod()
                .WithExposedHeaders("X-Pagination"));
            });
            return services;
        }

        public static IServiceCollection AddDataCompression(this IServiceCollection services)
        {
            services.AddResponseCompression(opts =>
            {
                opts.MimeTypes = ResponseCompressionDefaults.MimeTypes.Concat(
                    new[] { "application/octet-stream" });
            });
            return services;

        }

        public static IServiceCollection AddCSRF(this IServiceCollection services)
        {
            services.AddAntiforgery(options =>
            {
                options.HeaderName = "X-CSRF-TOKEN";
            });
            return services;
        }

        public static IServiceCollection AddSessionCookie(this IServiceCollection services)
        {
            services.AddSession(options =>
            {
                // Definir um tempo limite curto para evitar problemas relacionados ao desempenho.
                options.IdleTimeout = TimeSpan.FromMinutes(20);
                options.Cookie.HttpOnly = true;
                // Tornar o "cookie de sessão" essencial
                options.Cookie.IsEssential = true;
            });
            return services;
        }

        public static IServiceCollection AddConnectionContext(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<WebContext>(options =>
                                                    options.UseLazyLoadingProxies() //Carregamento Lento
                                                   .UseSqlServer(configuration.GetConnectionString("SqlServer"),
                                                    b => b.MigrationsAssembly(nameof(DAL)))); //Habilita o projeto de acesso à dados para Migrações do EF

            services.AddIdentity<User, IdentityRole>()
                        .AddEntityFrameworkStores<WebContext>();
            return services;
        }

        public static IServiceCollection AddNewtonsoftJson(this IServiceCollection services)
        {
            services.AddControllers().AddNewtonsoftJson(options => options.SerializerSettings.ReferenceLoopHandling = Newtonsoft.Json.ReferenceLoopHandling.Ignore);

            return services;
        }
    }
}
