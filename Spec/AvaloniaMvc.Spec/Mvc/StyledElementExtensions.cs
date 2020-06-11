// Copyright (C) 2020 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
using System.Linq;
using Avalonia;

namespace Charites.Windows.Mvc
{
    internal static class StyledElementExtensions
    {
        public static T GetController<T>(this StyledElement element) => AvaloniaController.GetControllers(element).OfType<T>().FirstOrDefault();
    }
}
