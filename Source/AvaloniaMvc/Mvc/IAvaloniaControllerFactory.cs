// Copyright (C) 2020 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
using System;

namespace Charites.Windows.Mvc
{
    /// <summary>
    /// Provides the function to create an Avalonia controller.
    /// </summary>
    public interface IAvaloniaControllerFactory
    {
        /// <summary>
        /// Creates an Avalonia controller.
        /// </summary>
        /// <param name="controllerType">The type of an Avalonia controller.</param>
        /// <returns>The new instance of an Avalonia controller.</returns>
        object Create(Type controllerType);
    }
}
