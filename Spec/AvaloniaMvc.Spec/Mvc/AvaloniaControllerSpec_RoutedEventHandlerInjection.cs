// Copyright (C) 2022 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
using Avalonia.Input;
using Carna;

namespace Charites.Windows.Mvc;

[Context("RoutedEventHandler injection")]
class AvaloniaControllerSpec_RoutedEventHandlerInjection : FixtureSteppable
{
    TestElement Element { get; }

     bool EventHandled { get; set; }

    public AvaloniaControllerSpec_RoutedEventHandlerInjection()
    {
        Element = new TestElement
        {
            Name = "Element",
            DataContext = new TestDataContexts.TestDataContext()
        };
        Element.EnsureVisual();
    }
    
    [Example("Adds an routed event handler")]
    void Ex01()
    {
        When("the AvaloniaController is enabled for the element", () => AvaloniaController.SetIsEnabled(Element, true));
        When("the element is attached to the logical tree", () => Element.AttachToLogicalTree());
        When("the routed event of the element is raised", () =>
        {
            Element.GetController<TestAvaloniaControllers.TestAvaloniaController>().KeyDownAssertionHandler += () => EventHandled = true;
            Element.RaiseEvent(new KeyEventArgs { RoutedEvent = InputElement.KeyDownEvent, Source = Element });
        });
        Then("the routed event should be handled", () => EventHandled);
    }
}