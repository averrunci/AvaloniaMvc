using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Charites.Windows.Mvc;
using Microsoft.Extensions.Hosting;

namespace $safeprojectname$;

internal class $safeitemrootname$(IHostApplicationLifetime lifetime, IServiceProvider services) : I$safeprojectname$Bootstrapper
{
    public void Bootstrap()
    {
        Environment.ExitCode = AppBuilder.Configure<$safeprojectname$Application > ()
            .UsePlatformDetect()
            .WithInterFont()
            .LogToTrace()
            .AfterSetup(builder => Configure(builder.Instance?.ApplicationLifetime as IClassicDesktopStyleApplicationLifetime))
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
