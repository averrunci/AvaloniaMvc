// Copyright (C) 2020-2021 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
using System;
using Avalonia.Controls;
using Avalonia.LogicalTree;
using Avalonia.VisualTree;

namespace Charites.Windows.Mvc
{
    internal class TestElement : ContentControl
    {
        public event EventHandler Changed;

        public void AttachToLogicalTree() => ((ISetLogicalParent)this).SetParent(new TestLogicalRoot());
        public void DetachFromLogicalTree() => ((ISetLogicalParent)this).SetParent(null);
        public void RaiseChanged() => Changed?.Invoke(this, EventArgs.Empty);

        public void EnsureVisual()
        {
            VisualChildren.Add(Content as IVisual);

            if (!(Content is TestElement childElement)) return;

            childElement.EnsureVisual();
        }
    }

    internal class TestLogicalRoot : Control, ILogicalRoot
    {
    }
}
