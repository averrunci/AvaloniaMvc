// Copyright (C) 2020-2021 Fievus
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

        public void AttachToLogicalTree() => ((ISetLogicalParent)this).SetParent(new TestLogicalRoot());
        public void DetachFromLogicalTree() => ((ISetLogicalParent)this).SetParent(null);
        public void RaiseChanged() => Changed?.Invoke(this, EventArgs.Empty);
    }

    internal class TestLogicalRoot : Control, ILogicalRoot
    {
    }
}
