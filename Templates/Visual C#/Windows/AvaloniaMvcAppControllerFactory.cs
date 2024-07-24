using Charites.Windows.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace $safeprojectname$;

internal class $safeitemrootname$(IServiceProvider services) : IAvaloniaControllerFactory
{
    public object Create(Type controllerType) => services.GetRequiredService(controllerType);
}
