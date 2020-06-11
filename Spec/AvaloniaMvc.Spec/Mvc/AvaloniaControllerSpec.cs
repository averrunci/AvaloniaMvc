// Copyright (C) 2020 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
using Carna;

namespace Charites.Windows.Mvc
{
    [Specification("AvaloniaController Spec")]
    class AvaloniaControllerSpec
    {
        [Context]
        AvaloniaControllerSpec_EventHandlerDataContextElementInjection EventHandlerDataContextElementInjection { get; }

        [Context]
        AvaloniaControllerSpec_AttachingAndDetachingController AttachingAndDetachingController { get; }

        [Context]
        AvaloniaControllerSpec_ExecuteHandler ExecuteHandler { get; }

        [Context]
        AvaloniaControllerSpec_AvaloniaControllerExtension AvaloniaControllerExtension { get; }

        [Context]
        AvaloniaControllerSpec_UnhandledException UnhandledException { get; }
    }
}
