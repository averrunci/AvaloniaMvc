// Copyright (C) 2020 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;

namespace Charites.Windows.Samples.SimpleLoginDemo
{
    internal class SimpleLoginDemo : IHostedService
    {
        private readonly ISimpleLoginDemoBootstrapper bootstrapper;

        public SimpleLoginDemo(ISimpleLoginDemoBootstrapper bootstrapper)
        {
            this.bootstrapper = bootstrapper ?? throw new ArgumentNullException(nameof(bootstrapper));
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
}
