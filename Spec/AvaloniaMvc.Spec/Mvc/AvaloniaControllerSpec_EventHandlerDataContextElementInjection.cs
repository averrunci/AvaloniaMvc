// Copyright (C) 2020-2021 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
using System;
using System.Collections;
using Avalonia.Controls;
using Avalonia.Interactivity;
using Carna;

namespace Charites.Windows.Mvc
{
    [Context("Event handler, data context, and element injection")]
    class AvaloniaControllerSpec_EventHandlerDataContextElementInjection : FixtureSteppable
    {
        static object DataContext { get; set; }
        static Button ChildElement { get; set; }
        static TestElement Element { get; set; }

        static bool EventHandled { get; set; }
        static Action NoArgumentAssertionHandler { get; } = () => EventHandled = true;
        static Action<RoutedEventArgs> OneArgumentAssertionHandler { get; } = e => EventHandled = Equals(e.Source, ChildElement);
        static EventHandler<RoutedEventArgs> EventHandlerAssertionHandler { get; } = (s, e) => EventHandled = Equals(s, ChildElement) && Equals(e.Source, ChildElement);

        [Example("Adds event handlers")]
        [Sample(Source = typeof(AvaloniaControllerSampleDataSource))]
        void Ex01(TestAvaloniaControllers.ITestAvaloniaController controller)
        {
            Given("a data context", () => DataContext = new object());
            Given("a child element", () => ChildElement = new Button { Name = "ChildElement" });
            Given("an element that has the child element", () => Element = new TestElement { Name = "element", Content = ChildElement, DataContext = DataContext });

            When("the controller is added", () => AvaloniaController.GetControllers(Element).Add(controller));
            When("the controller is attached to the element", () => AvaloniaController.GetControllers(Element).AttachTo(Element));

            Then("the data context of the controller should be set", () => controller.DataContext == DataContext);
            Then("the element of the controller should be null", () => controller.Element == null);
            Then("the child element of the controller should be null", () => controller.ChildElement == null);

            When("the element is attached to the logical tree", () => Element.AttachToLogicalTree());

            Then("the data context of the controller should be set", () => controller.DataContext == DataContext);
            Then("the element of the controller should be set", () => controller.Element == Element);
            Then("the child element of the controller should be set", () => controller.ChildElement == ChildElement);

            EventHandled = false;
            When("the Click event of the child element is raised", () => ChildElement.RaiseEvent(new RoutedEventArgs(Button.ClickEvent, ChildElement)));

            Then("the Click event should be handled", () => EventHandled);
        }

        [Example("Sets elements")]
        [Sample(Source = typeof(AvaloniaControllerSampleDataSource))]
        void Ex02(TestAvaloniaControllers.ITestAvaloniaController controller)
        {
            Given("a child element", () => ChildElement = new Button { Name = "ChildElement" });
            Given("an element that has the child element", () => Element = new TestElement { Name = "element", Content = ChildElement });
            When("the child element is set to the controller using the AvaloniaController", () => AvaloniaController.SetElement(ChildElement, controller, true));
            When("the element is set to the controller using the AvaloniaController", () => AvaloniaController.SetElement(Element, controller, true));
            Then("the element should be set to the controller", () => controller.Element == Element);
            Then("the child element should be set to the controller", () => controller.ChildElement == ChildElement);
        }

        [Example("Sets a data context")]
        [Sample(Source = typeof(AvaloniaControllerSampleDataSource))]
        void Ex03(TestAvaloniaControllers.ITestAvaloniaController controller)
        {
            Given("a data context", () => DataContext = new object());
            When("the data context is set to the controller using the AvaloniaController", () => AvaloniaController.SetDataContext(DataContext, controller));
            Then("the data context should be set to the controller", () => controller.DataContext == DataContext);
        }

        class AvaloniaControllerSampleDataSource : ISampleDataSource
        {
            IEnumerable ISampleDataSource.GetData()
            {
                yield return new { Description = "When the contents are attributed to fields and the event handler has no argument", Controller = new TestAvaloniaControllers.AttributedToField.NoArgumentHandlerController(NoArgumentAssertionHandler) };
                yield return new { Description = "When the contents are attributed to fields and the event handler has one argument", Controller = new TestAvaloniaControllers.AttributedToField.OneArgumentHandlerController(OneArgumentAssertionHandler) };
                yield return new { Description = "When the contents are attributed to fields and the event handler has event arguments", Controller = new TestAvaloniaControllers.AttributedToField.EventHandlerController(EventHandlerAssertionHandler) };
                yield return new { Description = "When the contents are attributed to properties and the event handler has no argument", Controller = new TestAvaloniaControllers.AttributedToProperty.NoArgumentHandlerController(NoArgumentAssertionHandler) };
                yield return new { Description = "When the contents are attributed to properties and the event handler has one argument", Controller = new TestAvaloniaControllers.AttributedToProperty.OneArgumentHandlerController(OneArgumentAssertionHandler) };
                yield return new { Description = "When the contents are attributed to properties and the event handler has event arguments", Controller = new TestAvaloniaControllers.AttributedToProperty.EventHandlerController(EventHandlerAssertionHandler) };
                yield return new { Description = "When the contents are attributed to methods and the event handler has no argument", Controller = new TestAvaloniaControllers.AttributedToMethod.NoArgumentHandlerController(NoArgumentAssertionHandler) };
                yield return new { Description = "When the contents are attributed to methods and the event handler has one argument", Controller = new TestAvaloniaControllers.AttributedToMethod.OneArgumentHandlerController(OneArgumentAssertionHandler) };
                yield return new { Description = "When the contents are attributed to methods and the event handler has event arguments", Controller = new TestAvaloniaControllers.AttributedToMethod.EventHandlerController(EventHandlerAssertionHandler) };
                yield return new { Description = "When the contents are attributed to methods and the event handler has no argument using a naming convention", Controller = new TestAvaloniaControllers.AttributedToMethodUsingNamingConvention.NoArgumentHandlerController(NoArgumentAssertionHandler) };
                yield return new { Description = "When the contents are attributed to methods and the event handler has one argument using a naming convention", Controller = new TestAvaloniaControllers.AttributedToMethodUsingNamingConvention.OneArgumentHandlerController(OneArgumentAssertionHandler) };
                yield return new { Description = "When the contents are attributed to methods and the event handler has event arguments using a naming convention", Controller = new TestAvaloniaControllers.AttributedToMethodUsingNamingConvention.EventHandlerController(EventHandlerAssertionHandler) };
            }
        }
    }
}