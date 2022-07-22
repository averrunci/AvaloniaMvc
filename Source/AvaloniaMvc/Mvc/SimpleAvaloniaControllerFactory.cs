// Copyright (C) 2020-2022 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
namespace Charites.Windows.Mvc;

internal sealed class SimpleAvaloniaControllerFactory : IAvaloniaControllerFactory
{
    public object? Create(Type controllerType) => Activator.CreateInstance(controllerType);
}