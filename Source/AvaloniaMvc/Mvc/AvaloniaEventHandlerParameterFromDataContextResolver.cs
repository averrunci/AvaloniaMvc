// Copyright (C) 2022 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
using Avalonia;

namespace Charites.Windows.Mvc;

internal sealed class AvaloniaEventHandlerParameterFromDataContextResolver : EventHandlerParameterFromDataContextResolver
{
    public AvaloniaEventHandlerParameterFromDataContextResolver(object? associatedElement) : base(associatedElement)
    {
    }

    protected override object? FindDataContext()
        => AssociatedElement is StyledElement view ? AvaloniaController.DataContextFinder.Find(view) : null;
}