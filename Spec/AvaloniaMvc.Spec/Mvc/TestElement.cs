// Copyright (C) 2020 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
using System;
using Avalonia.Controls;
using Avalonia.LogicalTree;

namespace Charites.Windows.Mvc
{
    internal class TestElement : ContentControl
    {
        public event EventHandler Changed;

        public void RaiseAttachedToLogicalTree() => ((ILogical)this).NotifyAttachedToLogicalTree(new LogicalTreeAttachmentEventArgs(this));
        public void RaiseDetachedFromLogicalTree() => ((ILogical)this).NotifyDetachedFromLogicalTree(new LogicalTreeAttachmentEventArgs(this));
        public void RaiseChanged() => Changed?.Invoke(this, EventArgs.Empty);
    }
}
