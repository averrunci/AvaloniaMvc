// Copyright (C) 2020-2023 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
using Avalonia;
using Avalonia.Controls;
using Avalonia.Controls.Embedding.Offscreen;
using NSubstitute;

namespace Charites.Windows.Mvc;

internal class TestElement : ContentControl
{
    public event EventHandler? Changed;

    public void AttachToLogicalTree() => ((ISetLogicalParent)this).SetParent(new TestLogicalRoot());
    public void DetachFromLogicalTree() => ((ISetLogicalParent)this).SetParent(null);
    public void RaiseChanged() => Changed?.Invoke(this, EventArgs.Empty);

    public void EnsureVisual()
    {
        if (Content is Visual visual) VisualChildren.Add(visual);
        if (Content is not TestElement childElement) return;

        childElement.EnsureVisual();
    }
}

internal class TestLogicalRoot : TopLevel
{
    public TestLogicalRoot() : base(Substitute.For<OffscreenTopLevelImplBase>())
    {
    }
}
