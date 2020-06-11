// Copyright (C) 2020 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
using Avalonia;

namespace Charites.Windows.Mvc
{
    internal sealed class AvaloniaDataContextFinder : IAvaloniaDataContextFinder
    {
        public object Find(StyledElement view) => view.DataContext;
    }
}
