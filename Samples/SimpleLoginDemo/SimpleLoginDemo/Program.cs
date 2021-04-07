// Copyright (C) 2020-2021 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
using Charites.Windows.Mvc;
using Charites.Windows.Samples.SimpleLoginDemo.Adapter;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Charites.Windows.Samples.SimpleLoginDemo.Presentation;

namespace Charites.Windows.Samples.SimpleLoginDemo
{
    internal class Program
    {
        private static void Main()
            => CreateHostBuilder().Build().Run();

        private static IHostBuilder CreateHostBuilder()
            => Host.CreateDefaultBuilder()
                .ConfigureServices((context, services) => ConfigureServices(context.Configuration, services));

        private static void ConfigureServices(IConfiguration configuration, IServiceCollection services)
            => services.AddHostedService<SimpleLoginDemo>()
                .AddSingleton<ISimpleLoginDemoBootstrapper, SimpleLoginDemoBootstrapper>()
                .AddSingleton<IContentNavigator, ContentNavigator>(p =>
                {
                    IContentNavigator navigator = new ContentNavigator();
                    navigator.IsNavigationStackEnabled = false;
                    return (ContentNavigator)navigator;
                })
                .AddControllers()
                .AddPresentationAdapters();
    }
}
