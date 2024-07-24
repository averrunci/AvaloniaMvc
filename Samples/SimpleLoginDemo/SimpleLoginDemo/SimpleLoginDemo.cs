// Copyright (C) 2020-2024 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
using Microsoft.Extensions.Hosting;

namespace Charites.Windows.Samples.SimpleLoginDemo;

internal class SimpleLoginDemo(ISimpleLoginDemoBootstrapper bootstrapper) : IHostedService
{
    public Task StartAsync(CancellationToken cancellationToken)
    {
        bootstrapper.Bootstrap();
        return Task.CompletedTask;
    }

    public Task StopAsync(CancellationToken cancellationToken)
    {
        return Task.CompletedTask;
    }
}