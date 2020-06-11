using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Extensions.Hosting;

namespace $safeprojectname$
{
    internal class $safeitemrootname$ : IHostedService
    {
        private readonly I$safeprojectname$Bootstrapper bootstrapper;

        public $safeitemrootname$(I$safeprojectname$Bootstrapper bootstrapper)
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
