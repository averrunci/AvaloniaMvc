// Copyright (C) 2021 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
using Avalonia.Controls;
using Avalonia.Interactivity;
using Carna;

namespace Charites.Windows.Mvc
{
    [Context("EventHandler injection for an attached event")]
    class AvaloniaControllerSpec_EventHandlerInjectionForAttachedEvent : FixtureSteppable
    {
        Button Button { get; } = new Button();
        TestElement Element { get; }

        bool ClickEventHandled { get; set; }

        public AvaloniaControllerSpec_EventHandlerInjectionForAttachedEvent()
        {
            Element = new TestElement
            {
                Name = "Element",
                Content = new TestElement { Content = Button },
                DataContext = new TestDataContexts.TestDataContext()
            };
            Element.EnsureVisual();
        }

        [Example("Adds an event handler for an attached event")]
        void Ex01()
        {
            When("the AvaloniaController is enabled for the element", () => AvaloniaController.SetIsEnabled(Element, true));
            When("the element is attached to the logical tree", () => Element.AttachToLogicalTree());
            When("the Click event of the button is raised", () =>
            {
                Element.GetController<TestAvaloniaControllers.TestAvaloniaController>().ButtonClickAssertionHandler += () => ClickEventHandled = true;
                Button.RaiseEvent(new RoutedEventArgs(Button.ClickEvent, Button));
            });
            Then("the Click event should be handled", () => ClickEventHandled);
        }
    }
}