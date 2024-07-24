// Copyright (C) 2022-2024 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
using Charites.Windows.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace Charites.Windows.Samples.SimpleTodo;

internal class SimpleTodoControllerFactory(IServiceProvider services) : IAvaloniaControllerFactory
{
    public object Create(Type controllerType) => services.GetRequiredService(controllerType);
}