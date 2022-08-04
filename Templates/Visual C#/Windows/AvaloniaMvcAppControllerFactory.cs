using Charites.Windows.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace $safeprojectname$;

internal class $safeitemrootname$ : IAvaloniaControllerFactory
{
    private readonly IServiceProvider services;

    public $safeitemrootname$(IServiceProvider services)
    {
        this.services = services;
    }

    public object Create(Type controllerType) => services.GetRequiredService(controllerType);
}
