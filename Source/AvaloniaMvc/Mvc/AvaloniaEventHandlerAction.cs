// Copyright (C) 2020 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
using System;
using System.Collections.Generic;
using System.Reflection;

namespace Charites.Windows.Mvc
{
    internal sealed class AvaloniaEventHandlerAction : EventHandlerAction
    {
        public AvaloniaEventHandlerAction(MethodInfo method, object target) : base(method, target)
        {
        }

        protected override bool HandleUnhandledException(Exception exc) => AvaloniaController.HandleUnhandledException(exc);

        protected override IParameterDependencyResolver CreateParameterDependencyResolver(IDictionary<Type, Func<object>> dependencyResolver)
            => new AvaloniaParameterDependencyResolver(dependencyResolver);
    }
}
