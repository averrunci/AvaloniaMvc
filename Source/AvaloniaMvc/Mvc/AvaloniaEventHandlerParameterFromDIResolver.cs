// Copyright (C) 2022-2024 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
namespace Charites.Windows.Mvc;

internal sealed class AvaloniaEventHandlerParameterFromDIResolver(object? associatedElement) : EventHandlerParameterFromDIResolver(associatedElement)
{
    protected override object? CreateParameter(Type parameterType)
        => AvaloniaController.ControllerFactory.Create(parameterType);
}