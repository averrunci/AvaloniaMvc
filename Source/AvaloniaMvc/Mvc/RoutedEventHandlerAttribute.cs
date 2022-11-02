// Copyright (C) 2022 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
using Avalonia.Interactivity;

namespace Charites.Windows.Mvc;

/// <summary>
/// Specifies the target to inject a routed event handler.
/// </summary>
[AttributeUsage(AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Field, AllowMultiple = true)]
public class RoutedEventHandlerAttribute : EventHandlerAttribute
{
    /// <summary>
    /// Gets or sets the routing strategies to listen to.
    /// </summary>
    public RoutingStrategies Routes { get; set; } = RoutingStrategies.Direct | RoutingStrategies.Bubble;
}