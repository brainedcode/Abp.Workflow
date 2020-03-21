using System;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Volo.Abp;

namespace Sample.Abp.Workflow
{
    internal static class Program
    {
        static async Task Main(string[] args)
        {
            await CreateHostBuilder(args).RunConsoleAsync();
        }

        private static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .UseEnvironment(
                    Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT", EnvironmentVariableTarget.Process) ??
                    Environments.Production)
                .ConfigureServices((context, services) =>
                {
                    var app = services.AddApplication<AppModule>(options => options.UseAutofac());
                    app.Initialize(services.BuildServiceProviderFromFactory());
                });
    }
}
