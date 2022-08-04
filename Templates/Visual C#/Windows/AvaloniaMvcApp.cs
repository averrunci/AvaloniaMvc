using Microsoft.Extensions.Hosting;

namespace $safeprojectname$;

internal class $safeitemrootname$ : IHostedService
{
    private readonly I$safeprojectname$Bootstrapper bootstrapper;

    public $safeitemrootname$(I$safeprojectname$Bootstrapper bootstrapper)
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
