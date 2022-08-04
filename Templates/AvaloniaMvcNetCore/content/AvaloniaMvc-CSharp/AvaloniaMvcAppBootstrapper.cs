using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Charites.Windows.Mvc;
using Microsoft.Extensions.Hosting;

namespace AvaloniaMvcApp;

internal class AvaloniaMvcAppBootstrapper : IAvaloniaMvcAppBootstrapper
{
    private readonly IHostApplicationLifetime lifetime;
    private readonly IServiceProvider services;

    public AvaloniaMvcAppBootstrapper(IHostApplicationLifetime lifetime, IServiceProvider services)
    {
        this.lifetime = lifetime;
        this.services = services;
    }

    public void Bootstrap()
    {
        Environment.ExitCode = AppBuilder.Configure<AvaloniaMvcAppApplication> ()
            .UsePlatformDetect()
            .LogToTrace()
            .AfterSetup(builder => Configure(builder.Instance.ApplicationLifetime as IClassicDesktopStyleApplicationLifetime))
            .StartWithClassicDesktopLifetime(Environment.GetCommandLineArgs());
    }

    private void Configure(IClassicDesktopStyleApplicationLifetime? desktop)
    {
        if (desktop is null) return;

        desktop.Startup += OnDesktopStart;
        desktop.Exit += OnDesktopExit;

        AvaloniaController.ControllerFactory = new AvaloniaMvcAppControllerFactory(services);
    }

    private void OnDesktopStart(object? sender, ControlledApplicationLifetimeStartupEventArgs e)
    {
    }

    private void OnDesktopExit(object? sender, ControlledApplicationLifetimeExitEventArgs e)
    {
        lifetime.StopApplication();
    }
}
