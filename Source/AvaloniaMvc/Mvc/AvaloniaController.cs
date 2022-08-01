// Copyright (C) 2020-2022 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
using System.Collections.ObjectModel;
using System.Reflection;
using Avalonia;

namespace Charites.Windows.Mvc;

/// <summary>
/// Provides functions for the controllers;
/// a data context injection, elements injection, and routed event handlers injection.
/// </summary>
public class AvaloniaController
{
    /// <summary>
    /// Occurs when an exception is not handled in event handlers.
    /// </summary>
    public static event EventHandler<UnhandledExceptionEventArgs>? UnhandledException;

    /// <summary>
    /// Gets or sets the finder to find a data context in a view.
    /// </summary>
    public static IAvaloniaDataContextFinder DataContextFinder
    {
        get => dataContextFinder;
        set
        {
            dataContextFinder = value;
            EnsureControllerTypeFinder();
        }
    }
    private static IAvaloniaDataContextFinder dataContextFinder = new AvaloniaDataContextFinder();

    /// <summary>
    /// Gets or sets the injector to inject a data context to a controller.
    /// </summary>
    public static IDataContextInjector DataContextInjector { get; set; } = new DataContextInjector();

    /// <summary>
    /// Gets or sets the finder to find an element in a view.
    /// </summary>
    public static IAvaloniaElementFinder ElementFinder
    {
        get => elementFinder;
        set
        {
            elementFinder = value;
            EnsureElementInjector();
        }
    }
    private static IAvaloniaElementFinder elementFinder = new AvaloniaElementFinder();

    /// <summary>
    /// Gets or sets the finder to find a key of an element.
    /// </summary>
    public static IAvaloniaElementKeyFinder ElementKeyFinder
    {
        get => elementKeyFinder;
        set
        {
            elementKeyFinder = value;
            EnsureControllerTypeFinder();
        }
    }
    private static IAvaloniaElementKeyFinder elementKeyFinder = new AvaloniaElementKeyFinder();

    /// <summary>
    /// Gets or sets the injector to inject elements in a view to a controller.
    /// </summary>
    public static IAvaloniaElementInjector ElementInjector { get; set; } = new AvaloniaElementInjector(ElementFinder);

    /// <summary>
    /// Gets or sets the finder to find a type of a controller that controls a view.
    /// </summary>
    public static IAvaloniaControllerTypeFinder ControllerTypeFinder { get; set; } = new AvaloniaControllerTypeFinder(ElementKeyFinder, DataContextFinder);

    /// <summary>
    /// Gets or sets the factory to create a controller.
    /// </summary>
    public static IAvaloniaControllerFactory ControllerFactory { get; set; } = new SimpleAvaloniaControllerFactory();

    /// <summary>
    /// Gets the extension.
    /// </summary>
    protected static ICollection<IAvaloniaControllerExtension> Extensions { get; } = new Collection<IAvaloniaControllerExtension>();

    /// <summary>
    /// Adds an extension of a controller.
    /// </summary>
    /// <param name="extension">The extension of a controller.</param>
    public static void AddExtension(IAvaloniaControllerExtension extension) => Extensions.Add(extension);

    /// <summary>
    /// Removes an extension of a controller.
    /// </summary>
    /// <param name="extension">The extension of a controller.</param>
    public static void RemoveExtension(IAvaloniaControllerExtension extension) => Extensions.Remove(extension);

    /// <summary>
    /// Identifies the <see cref="KeyProperty"/> XAML attached property.
    /// </summary>
    public static readonly AvaloniaProperty<string?> KeyProperty = AvaloniaProperty.RegisterAttached<AvaloniaController, StyledElement, string?>("Key");

    /// <summary>
    /// Sets the value of the <see cref="KeyProperty"/> XAML attached property on the specified <see cref="IAvaloniaObject"/>.
    /// </summary>
    /// <param name="element">The element on which to set the <see cref="KeyProperty"/> XAML attached property.</param>
    /// <param name="value">The property value to set.</param>
    public static void SetKey(IAvaloniaObject element, string? value) => element.SetValue(KeyProperty, value);

    /// <summary>
    /// Gets the value of the <see cref="KeyProperty"/> XAML attached property from the specified <see cref="IAvaloniaObject"/>.
    /// </summary>
    /// <param name="element">The element from which to read the property value.</param>
    /// <returns>The value of the <see cref="KeyProperty"/> XAML attached property on the target dependency object.</returns>
    public static string? GetKey(IAvaloniaObject element) => element.GetValue(KeyProperty);

    private static void OnKeyChanged(AvaloniaPropertyChangedEventArgs e) => SetIsEnabled(e.Sender, true);

    /// <summary>
    /// Identifies the <see cref="IsEnabledProperty"/> XAML attached property.
    /// </summary>
    public static readonly AvaloniaProperty<bool> IsEnabledProperty = AvaloniaProperty.RegisterAttached<AvaloniaController, StyledElement, bool>("IsEnabled");

    /// <summary>
    /// Sets the value of the <see cref="IsEnabledProperty"/> XAML attached property on the specified <see cref="IAvaloniaObject"/>.
    /// </summary>
    /// <param name="element">The element on which to set the <see cref="IsEnabledProperty"/> XAML attached property.</param>
    /// <param name="value">The property value to set.</param>
    public static void SetIsEnabled(IAvaloniaObject element, bool value) => element.SetValue(IsEnabledProperty, value);

    /// <summary>
    /// Gets the value of the <see cref="IsEnabledProperty"/> XAML attached property from the specified <see cref="IAvaloniaObject"/>.
    /// </summary>
    /// <param name="element">The element from which to read the property value.</param>
    /// <returns>The value of the <see cref="IsEnabledProperty"/> XAML attached property on the target dependency object.</returns>
    public static bool GetIsEnabled(IAvaloniaObject element) => element.GetValue(IsEnabledProperty);

    private static void OnIsEnabledChanged(AvaloniaPropertyChangedEventArgs e)
    {
        if (e.Sender is not StyledElement element) throw new InvalidOperationException("Avalonia object must be StyledElement.");

        if ((bool)(e.NewValue ?? false))
        {
            if (element.DataContext is null)
            {
                element.DataContextChanged += OnElementDataContextChanged;
            }
            else
            {
                AttachControllers(element);
            }
        }
        else
        {
            element.DataContextChanged -= OnElementDataContextChanged;

            DetachControllers(element);
        }
    }

    private static readonly AvaloniaProperty<AvaloniaControllerCollection?> ControllersProperty
        = AvaloniaProperty.RegisterAttached<AvaloniaController, StyledElement, AvaloniaControllerCollection?>("Controllers");

    private static void OnElementDataContextChanged(object? sender, EventArgs e)
    {
        if (sender is not StyledElement element) return;

        element.DataContextChanged -= OnElementDataContextChanged;

        AttachControllers(element);
    }

    private static void AttachControllers(StyledElement element)
    {
        var controllers = new AvaloniaControllerCollection(DataContextFinder, DataContextInjector, ElementInjector, Extensions);
        controllers.AddRange(ControllerTypeFinder.Find(element).Select(ControllerFactory.Create).OfType<object>());
        controllers.AttachTo(element);
        element.SetValue(ControllersProperty, controllers);
    }

    private static void DetachControllers(StyledElement element)
    {
        var controllers = element.GetValue<AvaloniaControllerCollection?>(ControllersProperty);
        controllers?.Detach();
        element.SetValue(ControllersProperty, null);
    }

    static AvaloniaController()
    {
        Assembly.GetExecutingAssembly().GetTypes()
            .Where(type => typeof(IAvaloniaControllerExtension).IsAssignableFrom(type))
            .Where(type => type.IsClass && !type.IsAbstract)
            .Select(type => Activator.CreateInstance(type) as IAvaloniaControllerExtension)
            .Where(extension => extension is not null)
            .ForEach(extension => AddExtension(extension!));

        KeyProperty.Changed.Subscribe(OnKeyChanged);
        IsEnabledProperty.Changed.Subscribe(OnIsEnabledChanged);
    }

    private static void EnsureControllerTypeFinder()
    {
        ControllerTypeFinder = new AvaloniaControllerTypeFinder(ElementKeyFinder, DataContextFinder);
    }

    private static void EnsureElementInjector()
    {
        ElementInjector = new AvaloniaElementInjector(ElementFinder);
    }

    /// <summary>
    /// Gets the collection of the controller associated with the specified <see cref="StyledElement"/>.
    /// </summary>
    /// <param name="element">The element with which the collection of the controller is associated.</param>
    /// <returns>The collection of the controller associated with the specified <see cref="StyledElement"/>.</returns>
    public static AvaloniaControllerCollection GetControllers(StyledElement element)
    {
        var controllers = element.GetValue<AvaloniaControllerCollection?>(ControllersProperty);
        if (controllers is not null) return controllers;
        
        controllers = new AvaloniaControllerCollection(DataContextFinder, DataContextInjector, ElementInjector, Extensions);
        element.SetValue(ControllersProperty, controllers);
        return controllers;
    }

    /// <summary>
    /// Sets the specified data context to the specified controller.
    /// </summary>
    /// <param name="dataContext">The data context that is set to the controller.</param>
    /// <param name="controller">The controller to which the data context is set.</param>
    public static void SetDataContext(object dataContext, object controller) => DataContextInjector.Inject(dataContext, controller);

    /// <summary>
    /// Sets the specified element to the specified controller.
    /// </summary>
    /// <param name="rootElement">The element that is set to the controller.</param>
    /// <param name="controller">The controller to which the element is set.</param>
    /// <param name="foundElementOnly">
    /// If <c>true</c>, an element is not set to the controller when it is not found in the specified element;
    /// otherwise, <c>null</c> is set.
    /// </param>
    public static void SetElement(StyledElement rootElement, object controller, bool foundElementOnly = false)
        => ElementInjector.Inject(rootElement, controller, foundElementOnly);

    /// <summary>
    /// Gets a container of an extension that the specified controller has.
    /// </summary>
    /// <typeparam name="TExtension">The type of the extension.</typeparam>
    /// <typeparam name="T">The type of the container of the extension.</typeparam>
    /// <param name="controller">The controller that has the extension.</param>
    /// <returns>The container of the extension that the specified controller has.</returns>
    public static T Retrieve<TExtension, T>(object controller) where TExtension : IAvaloniaControllerExtension where T : class, new()
        => Extensions.OfType<TExtension>().FirstOrDefault()?.Retrieve(controller) as T ?? new T();

    /// <summary>
    /// Gets event handlers that the specified controller has.
    /// </summary>
    /// <param name="controller">The controller that has event handlers.</param>
    /// <returns>The event handlers that the specified controller has.</returns>
    public static EventHandlerBase<StyledElement, AvaloniaEventHandlerItem> EventHandlersOf(object controller)
        => Retrieve<AvaloniaEventHandlerExtension, EventHandlerBase<StyledElement, AvaloniaEventHandlerItem>>(controller);

    internal static bool HandleUnhandledException(Exception exc)
    {
        var e = new UnhandledExceptionEventArgs(exc);
        OnUnhandledException(e);
        return e.Handled;
    }

    private static void OnUnhandledException(UnhandledExceptionEventArgs e) => UnhandledException?.Invoke(null, e);
}