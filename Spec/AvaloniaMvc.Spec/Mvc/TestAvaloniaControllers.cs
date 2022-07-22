// Copyright (C) 2020-2022 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
using Avalonia;
using Avalonia.Controls;
using Avalonia.Interactivity;

namespace Charites.Windows.Mvc;

internal class TestAvaloniaControllers
{
    public interface ITestAvaloniaController
    {
        object? DataContext { get; }
        StyledElement? Element { get; }
        Button? ChildElement { get; }
    }

    public class TestAvaloniaControllerBase
    {
        [DataContext]
        public object? DataContext { get; set; }

        [Element]
        public TestElement? Element { get; set; }

        [EventHandler(ElementName = nameof(Element), Event = nameof(StyledElement.AttachedToLogicalTree))]
        protected void Element_AttachedToLogicalTree() => AttachedToLogicalTreeAssertionHandler?.Invoke();

        [EventHandler(ElementName = nameof(Element), Event = "Button.Click")]
        protected void Element_ButtonClick() => ButtonClickAssertionHandler?.Invoke();

        [EventHandler(ElementName = nameof(Element), Event = nameof(TestElement.Changed))]
        protected void Element_Changed() => ChangedAssertionHandler?.Invoke();

        [EventHandler(Event = nameof(StyledElement.DataContextChanged))]
        protected void OnDataContextChanged() { }

        public Action? AttachedToLogicalTreeAssertionHandler { get; set; }
        public Action? ButtonClickAssertionHandler { get; set; }
        public Action? ChangedAssertionHandler { get; set; }
    }

    [View(Key = "Charites.Windows.Mvc.TestDataContexts+TestDataContext")]
    public class TestAvaloniaController : TestAvaloniaControllerBase { }

    [View(Key = "Charites.Windows.Mvc.TestDataContexts+MultiTestDataContext")]
    public class MultiTestAvaloniaControllerA : TestAvaloniaControllerBase { }

    [View(Key = "Charites.Windows.Mvc.TestDataContexts+MultiTestDataContext")]
    public class MultiTestAvaloniaControllerB : TestAvaloniaControllerBase { }

    [View(Key = "Charites.Windows.Mvc.TestDataContexts+MultiTestDataContext")]
    public class MultiTestAvaloniaControllerC : TestAvaloniaControllerBase { }

    public class TestController {[DataContext] public object? DataContext { get; set; } }

    [View(Key = "AttachingTestDataContext")]
    public class TestDataContextController : TestController { }

    [View(Key = "BaseAttachingTestDataContext")]
    public class BaseTestDataContextController : TestController { }

    [View(Key = "Charites.Windows.Mvc.TestDataContexts+AttachingTestDataContextFullName")]
    public class TestDataContextFullNameController : TestController { }

    [View(Key = "Charites.Windows.Mvc.TestDataContexts+BaseAttachingTestDataContextFullName")]
    public class BaseTestDataContextFullNameController : TestController { }

    [View(Key = "GenericAttachingTestDataContext`1")]
    public class GenericTestDataContextController : TestController { }

    [View(Key = "Charites.Windows.Mvc.TestDataContexts+GenericAttachingTestDataContextFullName`1[System.String]")]
    public class GenericTestDataContextFullNameController : TestController { }

    [View(Key = "Charites.Windows.Mvc.TestDataContexts+GenericAttachingTestDataContextFullName`1")]
    public class GenericTestDataContextFullNameWithoutParametersController : TestController { }

    [View(Key = "IAttachingTestDataContext")]
    public class InterfaceImplementedTestDataContextController : TestController { }

    [View(Key = "Charites.Windows.Mvc.TestDataContexts+IAttachingTestDataContextFullName")]
    public class InterfaceImplementedTestDataContextFullNameController : TestController { }

    [View(Key = "TestElement")]
    public class KeyTestDataContextController : TestController { }

    public class TestAvaloniaControllerAsync
    {
        [DataContext]
        public object? DataContext { get; set; }

        [Element]
        public StyledElement? Element { get; set; }

        [EventHandler(ElementName = nameof(Element), Event = nameof(StyledElement.AttachedToLogicalTree))]
        private async Task Element_AttachedToLogicalTreeAsync()
        {
            await Task.Run(() => LoadedAssertionHandler?.Invoke());
        }

        public Action? LoadedAssertionHandler { get; set; }
    }

    public class ExceptionTestAvaloniaController
    {
        [EventHandler(Event = "Changed")]
        private void OnChanged()
        {
            throw new Exception();
        }
    }

    public class AttributedToField
    {
        public class NoArgumentHandlerController : ITestAvaloniaController
        {
            [DataContext]
            private object? dataContext;

            [Element]
            private StyledElement? element;

            [Element(Name = "ChildElement")]
            private Button? childElement;

            [EventHandler(ElementName = "ChildElement", Event = nameof(Button.Click))]
            private Action? handler;

            public NoArgumentHandlerController(Action assertionHandler)
            {
                handler = assertionHandler;
            }

            public object? DataContext => dataContext;
            public StyledElement? Element => element;
            public Button? ChildElement => childElement;
        }

        public class OneArgumentHandlerController : ITestAvaloniaController
        {
            [DataContext]
            private object? dataContext;

            [Element]
            private StyledElement? element;

            [Element(Name = "ChildElement")]
            private Button? childElement;

            [EventHandler(ElementName = "ChildElement", Event = nameof(Button.Click))]
            private Action<RoutedEventArgs> handler;

            public OneArgumentHandlerController(Action<RoutedEventArgs> assertionHandler)
            {
                handler = assertionHandler;
            }

            public object? DataContext => dataContext;
            public StyledElement? Element => element;
            public Button? ChildElement => childElement;
        }

        public class EventHandlerController : ITestAvaloniaController
        {
            [DataContext]
            private object? dataContext;

            [Element]
            private StyledElement? element;

            [Element(Name = "ChildElement")]
            private Button? childElement;

            [EventHandler(ElementName = "ChildElement", Event = nameof(Button.Click))]
            private EventHandler<RoutedEventArgs> handler;

            public EventHandlerController(EventHandler<RoutedEventArgs> assertionHandler)
            {
                handler = assertionHandler;
            }

            public object? DataContext => dataContext;
            public StyledElement? Element => element;
            public Button? ChildElement => childElement;
        }
    }

    public class AttributedToProperty
    {
        public class NoArgumentHandlerController : ITestAvaloniaController
        {
            [DataContext]
            public object? DataContext { get; private set; }

            [Element(Name = "element")]
            public StyledElement? Element { get; private set; }

            [Element]
            public Button? ChildElement { get; private set; }

            [EventHandler(ElementName = nameof(ChildElement), Event = nameof(Button.Click))]
            private Action Handler { get; set; }

            public NoArgumentHandlerController(Action assertionHandler)
            {
                Handler = assertionHandler;
            }
        }

        public class OneArgumentHandlerController : ITestAvaloniaController
        {
            [DataContext]
            public object? DataContext { get; private set; }

            [Element(Name = "element")]
            public StyledElement? Element { get; private set; }

            [Element]
            public Button? ChildElement { get; private set; }

            [EventHandler(ElementName = nameof(ChildElement), Event = nameof(Button.Click))]
            private Action<RoutedEventArgs> Handler { get; set; }

            public OneArgumentHandlerController(Action<RoutedEventArgs> assertionHandler)
            {
                Handler = assertionHandler;
            }
        }

        public class EventHandlerController : ITestAvaloniaController
        {
            [DataContext]
            public object? DataContext { get; private set; }

            [Element(Name = "element")]
            public StyledElement? Element { get; private set; }

            [Element]
            public Button? ChildElement { get; private set; }

            [EventHandler(ElementName = nameof(ChildElement), Event = nameof(Button.Click))]
            private EventHandler<RoutedEventArgs> Handler { get; set; }

            public EventHandlerController(EventHandler<RoutedEventArgs> assertionHandler)
            {
                Handler = assertionHandler;
            }
        }
    }

    public class AttributedToMethod
    {
        public class NoArgumentHandlerController : ITestAvaloniaController
        {
            [DataContext]
            public void SetDataContext(object? dataContext) => DataContext = dataContext;
            public object? DataContext { get; private set; }

            [Element(Name = "element")]
            public void SetElement(StyledElement? element) => Element = element;
            public StyledElement? Element { get; private set; }

            [Element]
            public void SetChildElement(Button? childElement) => ChildElement = childElement;
            public Button? ChildElement { get; private set; }

            [EventHandler(ElementName = nameof(ChildElement), Event = nameof(Button.Click))]
            public void ChildElement_Click() => handler();
            private readonly Action handler;

            public NoArgumentHandlerController(Action assertionHandler)
            {
                handler = assertionHandler;
            }
        }

        public class OneArgumentHandlerController : ITestAvaloniaController
        {
            [DataContext]
            public void SetDataContext(object? dataContext) => DataContext = dataContext;
            public object? DataContext { get; private set; }

            [Element(Name = "element")]
            public void SetElement(StyledElement? element) => Element = element;
            public StyledElement? Element { get; private set; }

            [Element]
            public void SetChildElement(Button? childElement) => ChildElement = childElement;
            public Button? ChildElement { get; private set; }

            [EventHandler(ElementName = nameof(ChildElement), Event = nameof(Button.Click))]
            public void ChildElement_Click(RoutedEventArgs e) => handler(e);
            private readonly Action<RoutedEventArgs> handler;

            public OneArgumentHandlerController(Action<RoutedEventArgs> assertionHandler)
            {
                handler = assertionHandler;
            }
        }

        public class EventHandlerController : ITestAvaloniaController
        {
            [DataContext]
            public void SetDataContext(object? dataContext) => DataContext = dataContext;
            public object? DataContext { get; private set; }

            [Element(Name = "element")]
            public void SetElement(StyledElement? element) => Element = element;
            public StyledElement? Element { get; private set; }

            [Element]
            public void SetChildElement(Button? childElement) => ChildElement = childElement;
            public Button? ChildElement { get; private set; }

            [EventHandler(ElementName = nameof(ChildElement), Event = nameof(Button.Click))]
            public void ChildElement_Click(object? sender, RoutedEventArgs e) => handler(sender, e);
            private readonly EventHandler<RoutedEventArgs> handler;

            public EventHandlerController(EventHandler<RoutedEventArgs> assertionHandler)
            {
                handler = assertionHandler;
            }
        }
    }

    public class AttributedToMethodUsingNamingConvention
    {
        public class NoArgumentHandlerController : ITestAvaloniaController
        {
            public void SetDataContext(object? dataContext) => DataContext = dataContext;
            public object? DataContext { get; private set; }

            [Element(Name = "element")]
            public void SetElement(StyledElement? element) => Element = element;
            public StyledElement? Element { get; private set; }

            [Element]
            public void SetChildElement(Button? childElement) => ChildElement = childElement;
            public Button? ChildElement { get; private set; }

            public void ChildElement_Click() => handler();
            private readonly Action handler;

            public NoArgumentHandlerController(Action assertionHandler)
            {
                handler = assertionHandler;
            }
        }

        public class OneArgumentHandlerController : ITestAvaloniaController
        {
            public void SetDataContext(object? dataContext) => DataContext = dataContext;
            public object? DataContext { get; private set; }

            [Element(Name = "element")]
            public void SetElement(StyledElement? element) => Element = element;
            public StyledElement? Element { get; private set; }

            [Element]
            public void SetChildElement(Button? childElement) => ChildElement = childElement;
            public Button? ChildElement { get; private set; }

            public void ChildElement_Click(RoutedEventArgs e) => handler(e);
            private readonly Action<RoutedEventArgs> handler;

            public OneArgumentHandlerController(Action<RoutedEventArgs> assertionHandler)
            {
                handler = assertionHandler;
            }
        }

        public class EventHandlerController : ITestAvaloniaController
        {
            public void SetDataContext(object? dataContext) => DataContext = dataContext;
            public object? DataContext { get; private set; }

            [Element(Name = "element")]
            public void SetElement(StyledElement? element) => Element = element;
            public StyledElement? Element { get; private set; }

            [Element]
            public void SetChildElement(Button? childElement) => ChildElement = childElement;
            public Button? ChildElement { get; private set; }

            public void ChildElement_Click(object? sender, RoutedEventArgs e) => handler(sender, e);
            private readonly EventHandler<RoutedEventArgs> handler;

            public EventHandlerController(EventHandler<RoutedEventArgs> assertionHandler)
            {
                handler = assertionHandler;
            }
        }
    }

    public class AttributedToAsyncMethodUsingNamingConvention
    {
        public class NoArgumentHandlerController : ITestAvaloniaController
        {
            public void SetDataContext(object? dataContext) => DataContext = dataContext;
            public object? DataContext { get; private set; }

            [Element(Name = "element")]
            public void SetElement(StyledElement? element) => Element = element;
            public StyledElement? Element { get; private set; }

            [Element]
            public void SetChildElement(Button? childElement) => ChildElement = childElement;
            public Button? ChildElement { get; private set; }

            public Task ChildElement_ClickAsync()
            {
                handler();
                return Task.CompletedTask;
            }
            private readonly Action handler;

            public NoArgumentHandlerController(Action assertionHandler)
            {
                handler = assertionHandler;
            }
        }

        public class OneArgumentHandlerController : ITestAvaloniaController
        {
            public void SetDataContext(object? dataContext) => DataContext = dataContext;
            public object? DataContext { get; private set; }

            [Element(Name = "element")]
            public void SetElement(StyledElement? element) => Element = element;
            public StyledElement? Element { get; private set; }

            [Element]
            public void SetChildElement(Button? childElement) => ChildElement = childElement;
            public Button? ChildElement { get; private set; }

            public Task ChildElement_ClickAsync(RoutedEventArgs e)
            {
                handler(e);
                return Task.CompletedTask;
            }
            private readonly Action<RoutedEventArgs> handler;

            public OneArgumentHandlerController(Action<RoutedEventArgs> assertionHandler)
            {
                handler = assertionHandler;
            }
        }

        public class EventHandlerController : ITestAvaloniaController
        {
            public void SetDataContext(object? dataContext) => DataContext = dataContext;
            public object? DataContext { get; private set; }

            [Element(Name = "element")]
            public void SetElement(StyledElement? element) => Element = element;
            public StyledElement? Element { get; private set; }

            [Element]
            public void SetChildElement(Button? childElement) => ChildElement = childElement;
            public Button? ChildElement { get; private set; }

            public Task ChildElement_ClickAsync(object? sender, RoutedEventArgs e)
            {
                handler(sender, e);
                return Task.CompletedTask;
            }
            private readonly EventHandler<RoutedEventArgs> handler;

            public EventHandlerController(EventHandler<RoutedEventArgs> assertionHandler)
            {
                handler = assertionHandler;
            }
        }
    }
}