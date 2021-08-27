using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;

namespace AvaloniaMvcApp
{
    internal class AvaloniaMvcApp : IHostedService
    {
        private readonly IAvaloniaMvcAppBootstrapper bootstrapper;

        public AvaloniaMvcApp(IAvaloniaMvcAppBootstrapper bootstrapper)
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
