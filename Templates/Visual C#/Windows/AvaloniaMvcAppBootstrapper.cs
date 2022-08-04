using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Charites.Windows.Mvc;
using Microsoft.Extensions.Hosting;

namespace $safeprojectname$;

internal class $safeitemrootname$ : I$safeprojectname$Bootstrapper
{
    private readonly IHostApplicationLifetime lifetime;
    private readonly IServiceProvider services;

    public $safeitemrootname$(IHostApplicationLifetime lifetime, IServiceProvider services)
    {
        this.lifetime = lifetime;
        this.services = services;
    }

    public void Bootstrap()
    {
        Environment.ExitCode = AppBuilder.Configure<$safeprojectname$Application > ()
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

        AvaloniaController.ControllerFactory = new $safeprojectname$ControllerFactory(services);
    }

    private void OnDesktopStart(object? sender, ControlledApplicationLifetimeStartupEventArgs e)
    {
    }

    private void OnDesktopExit(object? sender, ControlledApplicationLifetimeExitEventArgs e)
    {
        lifetime.StopApplication();
    }
}
