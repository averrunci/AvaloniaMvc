// Copyright (C) 2020-2022 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
using System.Reflection;

namespace Charites.Windows.Mvc;

internal sealed class AvaloniaEventHandlerAction : EventHandlerAction
{
    public AvaloniaEventHandlerAction(MethodInfo method, object? target, IParameterDependencyResolver parameterDependencyResolver) : base(method, target, parameterDependencyResolver)
    {
    }

    protected override bool HandleUnhandledException(Exception exc) => AvaloniaController.HandleUnhandledException(exc);
}