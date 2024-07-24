// Copyright (C) 2020-2024 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
using Carna;

namespace Charites.Windows.Mvc;

[Specification(
    "AvaloniaController Spec",
    typeof(AvaloniaControllerSpec_EventHandlerDataContextElementInjection),
    typeof(AvaloniaControllerSpec_AttachingAndDetachingController),
    typeof(AvaloniaControllerSpec_ExecuteHandler),
    typeof(AvaloniaControllerSpec_AvaloniaControllerExtension),
    typeof(AvaloniaControllerSpec_EventHandlerInjectionForAttachedEvent),
    typeof(AvaloniaControllerSpec_UnhandledException),
    typeof(AvaloniaControllerSpec_RoutedEventHandlerInjection)
)]
class AvaloniaControllerSpec;