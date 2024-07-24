using Microsoft.Extensions.Hosting;

namespace $safeprojectname$;

internal class $safeitemrootname$(I$safeprojectname$Bootstrapper bootstrapper) : IHostedService
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
