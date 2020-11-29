using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using BLL.Library.Gerenciador.Helpers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Events;

namespace WebCommerce.Server
{
    public static class Program
    {

        public static void Main(string[] args)
        {
            CurrentDirectoryHelpers.SetCurrentDirectory();
            string caminhoLog = Path.Combine(Directory.GetCurrentDirectory(), "Logs.txt");

            Log.Logger = new LoggerConfiguration()
            .MinimumLevel.Debug()
            .MinimumLevel.Override("Microsoft", LogEventLevel.Error)
            .Enrich.FromLogContext()
            .WriteTo.File(caminhoLog)
            .CreateLogger();

            try
            {
                Log.Information("--- SERVIDOR INICIANDO ---");
                CreateWebHostBuilder(args).Build().Run();
            }
            catch (Exception ex)
            {
                Log.Fatal(ex, "--- SERVIDOR TERMINOU INESPERADAMENTE ---");
            }
            finally
            {
                Log.Information("--- SERVIDOR ENCERRANDO ---");
                Log.CloseAndFlush();
            }
        }

        public static IHostBuilder CreateWebHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
            .ConfigureWebHostDefaults(webbuilder =>
            {
                webbuilder.UseStartup<Startup>();
                webbuilder.UseSerilog();
            });
    }
}
