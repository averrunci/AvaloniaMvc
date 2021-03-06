﻿// Copyright (C) 2020 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
using System;
using Avalonia;
using Avalonia.Controls.ApplicationLifetimes;
using Avalonia.Logging.Serilog;
using Charites.Windows.Mvc;
using Microsoft.Extensions.Hosting;

namespace Charites.Windows.Samples.SimpleLoginDemo
{
    internal class SimpleLoginDemoBootstrapper : ISimpleLoginDemoBootstrapper
    {
        private readonly IHostApplicationLifetime lifetime;
        private readonly IServiceProvider services;

        public SimpleLoginDemoBootstrapper(IHostApplicationLifetime lifetime, IServiceProvider services)
        {
            this.lifetime = lifetime ?? throw new ArgumentNullException(nameof(lifetime));
            this.services = services;
        }

        public void Bootstrap()
        {
            Environment.ExitCode = AppBuilder.Configure<SimpleLoginDemoApplication>()
                .UsePlatformDetect()
                .LogToDebug()
                .AfterSetup(builder => Configure(builder.Instance.ApplicationLifetime as IClassicDesktopStyleApplicationLifetime))
                .StartWithClassicDesktopLifetime(Environment.GetCommandLineArgs());
        }

        private void Configure(IClassicDesktopStyleApplicationLifetime desktop)
        {
            if (desktop == null) return;

            desktop.Startup += OnDesktopStart;
            desktop.Exit += OnDesktopExit;

            AvaloniaController.ControllerFactory = new SimpleLoginDemoControllerFactory(services);
        }

        private void OnDesktopStart(object sender, ControlledApplicationLifetimeStartupEventArgs e)
        {
        }

        private void OnDesktopExit(object sender, ControlledApplicationLifetimeExitEventArgs e)
        {
            lifetime.StopApplication();
        }
    }
}
