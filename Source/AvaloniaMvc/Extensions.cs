// Copyright (C) 2020-2022 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
using Avalonia;
using Avalonia.LogicalTree;
using Avalonia.Styling;

namespace Charites.Windows;

internal static class Extensions
{
    public static void ForEach<T>(this IEnumerable<T> @this, Action<T> action)
    {
        foreach (var item in @this)
        {
            action(item);
        }
    }

    public static TElement? FindElement<TElement>(this StyledElement? rootElement, string elementName) where TElement : class
    {
        if (rootElement is null) return null;
        if (string.IsNullOrEmpty(elementName)) return rootElement as TElement;
        if (rootElement.Name == elementName) return rootElement as TElement;

        return rootElement.GetLogicalDescendants().FirstOrDefault(child => (child as StyledElement)?.Name == elementName) as TElement;
    }

    public static ILogicalRoot? FindLogicalRoot(this IStyleHost? styleHost)
    {
        while (styleHost is not null)
        {
            if (styleHost is ILogicalRoot logicalRoot) return logicalRoot;

            styleHost = styleHost.StylingParent;
        }

        return null;
    }
}