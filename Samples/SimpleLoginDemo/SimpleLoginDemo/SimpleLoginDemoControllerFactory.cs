// Copyright (C) 2020 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
using System;
using Charites.Windows.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace Charites.Windows.Samples.SimpleLoginDemo
{
    internal class SimpleLoginDemoControllerFactory : IAvaloniaControllerFactory
    {
        private readonly IServiceProvider services;

        public SimpleLoginDemoControllerFactory(IServiceProvider services)
        {
            this.services = services ?? throw new ArgumentNullException(nameof(services));
        }

        public object Create(Type controllerType) => services.GetRequiredService(controllerType);
    }
}
