// Copyright (C) 2020-2022 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
using System.Reflection;
using Carna;

namespace Charites.Windows.Mvc;

[Context("Unhandled exception")]
class AvaloniaControllerSpec_UnhandledException : FixtureSteppable
{
    TestAvaloniaControllers.ExceptionTestAvaloniaController Controller { get; } = new();
    TestElement Element { get; } = new();

    bool ExceptionHandled { get; set; }
    Exception? UnhandledException { get; set; }

    public AvaloniaControllerSpec_UnhandledException()
    {
        AvaloniaController.UnhandledException += OnAvaloniaControllerUnhandledException;
    }

    public void Dispose()
    {
        AvaloniaController.UnhandledException -= OnAvaloniaControllerUnhandledException;
    }

    void OnAvaloniaControllerUnhandledException(object? sender, UnhandledExceptionEventArgs e)
    {
        UnhandledException = e.Exception;
        e.Handled = ExceptionHandled;
    }

    [Example("Handles an unhandled exception as it is handled")]
    void Ex01()
    {
        ExceptionHandled = true;

        When("the controller is added", () => AvaloniaController.GetControllers(Element).Add(Controller));
        When("the controller is attached to the element", () => AvaloniaController.GetControllers(Element).AttachTo(Element));

        When("the element is attached to the logical tree", () => Element.AttachToLogicalTree());

        When("the Changed event of the element is raised", () => Element.RaiseChanged());
        Then("the unhandled exception should be handled", () => UnhandledException != null);
    }

    [Example("Handles an unhandled exception as it is not handled")]
    void Ex02()
    {
        ExceptionHandled = false;

        When("the controller is added", () => AvaloniaController.GetControllers(Element).Add(Controller));
        When("the controller is attached to the element", () => AvaloniaController.GetControllers(Element).AttachTo(Element));

        When("the element is attached to the logical tree", () => Element.AttachToLogicalTree());

        When("the Changed event of the element is raised", () => Element.RaiseChanged());
        Then<TargetInvocationException>("the exception should be thrown");
        Then("the unhandled exception should be handled", () => UnhandledException != null);
    }
}