using Charites.Windows.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace AvaloniaMvcApp;

internal class AvaloniaMvcAppControllerFactory(IServiceProvider services) : IAvaloniaControllerFactory
{
    public object Create(Type controllerType) => services.GetRequiredService(controllerType);
}
