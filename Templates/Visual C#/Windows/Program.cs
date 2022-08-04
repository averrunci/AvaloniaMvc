using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace $safeprojectname$;

internal static class $safeitemrootname$
{
    [STAThread]
    private static void Main() => CreateHostBuilder().Build().Run();

    private static IHostBuilder CreateHostBuilder()
        => Host.CreateDefaultBuilder()
            .ConfigureServices((context, services) => ConfigureServices(context.Configuration, services));

    private static void ConfigureServices(IConfiguration configuration, IServiceCollection services)
        => services.AddHostedService<$safeprojectname$>()
            .AddSingleton<I$safeprojectname$Bootstrapper, $safeprojectname$Bootstrapper>()
            .AddControllers();
}
