using Microsoft.Extensions.Hosting;

namespace AvaloniaMvcApp;

internal class AvaloniaMvcApp : IHostedService
{
    private readonly IAvaloniaMvcAppBootstrapper bootstrapper;

    public AvaloniaMvcApp(IAvaloniaMvcAppBootstrapper bootstrapper)
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
