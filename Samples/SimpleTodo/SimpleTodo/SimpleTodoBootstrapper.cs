// Copyright (C) 2022-2024 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Charites.Windows.Mvc;
using Microsoft.Extensions.Hosting;

namespace Charites.Windows.Samples.SimpleTodo;

internal class SimpleTodoBootstrapper(IHostApplicationLifetime lifetime, IServiceProvider services) : ISimpleTodoBootstrapper
{
    public void Bootstrap()
    {
        Environment.ExitCode = AppBuilder.Configure<SimpleTodoApplication>()
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

        AvaloniaController.ControllerFactory = new SimpleTodoControllerFactory(services);
    }

    private void OnDesktopStart(object? sender, ControlledApplicationLifetimeStartupEventArgs e)
    {
    }

    private void OnDesktopExit(object? sender, ControlledApplicationLifetimeExitEventArgs e)
    {
        lifetime.StopApplication();
    }
}