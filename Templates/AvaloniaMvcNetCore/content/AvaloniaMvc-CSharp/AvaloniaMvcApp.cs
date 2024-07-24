using Microsoft.Extensions.Hosting;

namespace AvaloniaMvcApp;

internal class AvaloniaMvcApp(IAvaloniaMvcAppBootstrapper bootstrapper) : IHostedService
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
