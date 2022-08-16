// Copyright (C) 2022 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
using Microsoft.Extensions.Hosting;

namespace Charites.Windows.Samples.SimpleTodo;

internal class SimpleTodo : IHostedService
{
    private readonly ISimpleTodoBootstrapper bootstrapper;

    public SimpleTodo(ISimpleTodoBootstrapper bootstrapper)
    {
        this.bootstrapper = bootstrapper;
    }

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