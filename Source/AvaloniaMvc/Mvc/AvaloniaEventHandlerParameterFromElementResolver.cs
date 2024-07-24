// Copyright (C) 2022-2024 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.

using Avalonia;

namespace Charites.Windows.Mvc;

internal sealed class AvaloniaEventHandlerParameterFromElementResolver(object? associatedElement) : EventHandlerParameterFromElementResolver(associatedElement)
{
    protected override object? FindElement(string name)
        => AssociatedElement is StyledElement rootElement ? AvaloniaController.ElementFinder.FindElement(rootElement, name) : null;
}