﻿// Copyright (C) 2020 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
using System;
using System.Collections.Generic;
using System.Linq;
using Avalonia;

namespace Charites.Windows.Mvc
{
    internal sealed class AvaloniaControllerTypeFinder : ControllerTypeFinder<StyledElement>, IAvaloniaControllerTypeFinder
    {
        public AvaloniaControllerTypeFinder(IElementKeyFinder<StyledElement> elementKeyFinder, IDataContextFinder<StyledElement> dataContextFinder) : base(elementKeyFinder, dataContextFinder)
        {
        }

        protected override IEnumerable<Type> FindControllerTypeCandidates(StyledElement view)
            => AppDomain.CurrentDomain.GetAssemblies().SelectMany(assembly => assembly.GetTypes());
    }
}
