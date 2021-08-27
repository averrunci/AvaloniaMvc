using System;
using Charites.Windows.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace AvaloniaMvcApp
{
    internal class AvaloniaMvcAppControllerFactory : IAvaloniaControllerFactory
    {
        private readonly IServiceProvider services;

        public AvaloniaMvcAppControllerFactory(IServiceProvider services)
        {
            this.services = services ?? throw new ArgumentNullException(nameof(services));
        }

        public object Create(Type controllerType) => services.GetRequiredService(controllerType);
    }
}
