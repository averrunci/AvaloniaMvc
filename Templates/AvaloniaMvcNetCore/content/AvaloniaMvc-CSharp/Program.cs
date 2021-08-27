using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace AvaloniaMvcApp
{
    internal static class Program
    {
        private static void Main()
            => CreateHostBuilder().Build().Run();

        private static IHostBuilder CreateHostBuilder()
            => Host.CreateDefaultBuilder()
                .ConfigureServices((context, services) => ConfigureServices(context.Configuration, services));

        private static void ConfigureServices(IConfiguration configuration, IServiceCollection services)
            => services.AddHostedService<AvaloniaMvcApp>()
                .AddSingleton<IAvaloniaMvcAppBootstrapper, AvaloniaMvcAppBootstrapper>()
                .AddControllers();
    }
}
