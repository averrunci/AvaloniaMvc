// Copyright (C) 2020 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
using System;
using System.Collections.Generic;
using System.Linq;
using Avalonia;
using Avalonia.LogicalTree;
using Avalonia.Styling;

namespace Charites.Windows
{
    internal static class Extensions
    {
        public static void IfPresent<T>(this T @this, Action<T> action)
        {
            if (@this == null) return;

            action(@this);
        }

        public static void IfAbsent<T>(this T @this, Action action)
        {
            if (@this != null) return;

            action();
        }

        public static void ForEach<T>(this IEnumerable<T> @this, Action<T> action)
        {
            if (@this == null) return;

            foreach (var item in @this)
            {
                action(item);
            }
        }

        public static TElement FindElement<TElement>(this StyledElement rootElement, string elementName) where TElement : class
        {
            if (rootElement == null) return null;
            if (string.IsNullOrEmpty(elementName)) return rootElement as TElement;
            if (rootElement.Name == elementName) return rootElement as TElement;

            return rootElement.GetLogicalDescendants().FirstOrDefault(child => (child as StyledElement)?.Name == elementName) as TElement;
        }

        public static IStyleRoot FindStyleRoot(this IStyleHost styleHost)
        {
            while (styleHost != null)
            {
                if (styleHost is IStyleRoot styleRoot) return styleRoot;

                styleHost = styleHost.StylingParent;
            }

            return null;
        }
    }
}
