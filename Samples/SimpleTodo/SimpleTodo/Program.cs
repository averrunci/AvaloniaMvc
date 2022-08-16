// Copyright (C) 2022 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Charites.Windows.Samples.SimpleTodo;

internal static class Program
{
    [STAThread]
    private static void Main() => CreateHostBuilder().Build().Run();

    private static IHostBuilder CreateHostBuilder()
        => Host.CreateDefaultBuilder()
            .ConfigureServices((context, services) => ConfigureServices(context.Configuration, services));

    private static void ConfigureServices(IConfiguration configuration, IServiceCollection services)
        => services.AddHostedService<SimpleTodo>()
            .AddSingleton<ISimpleTodoBootstrapper, SimpleTodoBootstrapper>()
            .AddControllers();
}