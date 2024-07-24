// Copyright (C) 2020-2024 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
using Avalonia;

namespace Charites.Windows.Mvc;

internal sealed class AvaloniaElementInjector(IElementFinder<StyledElement> elementFinder) : ElementInjector<StyledElement>(elementFinder), IAvaloniaElementInjector;