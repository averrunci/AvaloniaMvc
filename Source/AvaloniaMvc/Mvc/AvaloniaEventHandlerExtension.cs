// Copyright (C) 2020-2022 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
using System.Reflection;
using Avalonia;
using Avalonia.Data;
using Avalonia.Interactivity;
using Avalonia.LogicalTree;

namespace Charites.Windows.Mvc;

internal sealed class AvaloniaEventHandlerExtension : EventHandlerExtension<StyledElement, AvaloniaEventHandlerItem>, IAvaloniaControllerExtension
{
    private const BindingFlags RoutedEventBindingFlags = BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy;
    private const char AttachedEventSeparator = '.';

    private static readonly AvaloniaProperty<IDictionary<object, EventHandlerBase<StyledElement, AvaloniaEventHandlerItem>>> EventHandlerBasesProperty
        = AvaloniaProperty.RegisterAttached<AvaloniaEventHandlerExtension, StyledElement, IDictionary<object, EventHandlerBase<StyledElement, AvaloniaEventHandlerItem>>>("EventHandlerBases");

    public AvaloniaEventHandlerExtension()
    {
        Add<AvaloniaEventHandlerParameterFromDIResolver>();
        Add<AvaloniaEventHandlerParameterFromElementResolver>();
        Add<AvaloniaEventHandlerParameterFromDataContextResolver>();
    }

    protected override IDictionary<object, EventHandlerBase<StyledElement, AvaloniaEventHandlerItem>> EnsureEventHandlerBases(StyledElement? element)
    {
        if (element is null) return new Dictionary<object, EventHandlerBase<StyledElement, AvaloniaEventHandlerItem>>();

        if (element.GetValue(EventHandlerBasesProperty) is IDictionary<object, EventHandlerBase<StyledElement, AvaloniaEventHandlerItem>> eventHandlerBases) return eventHandlerBases;

        eventHandlerBases = new Dictionary<object, EventHandlerBase<StyledElement, AvaloniaEventHandlerItem>>();
        element.SetValue(EventHandlerBasesProperty, eventHandlerBases);
        return eventHandlerBases;
    }

    protected override void AddEventHandler(StyledElement? element, EventHandlerAttribute eventHandlerAttribute, Func<Type?, Delegate?> handlerCreator, EventHandlerBase<StyledElement, AvaloniaEventHandlerItem> eventHandlers)
    {
        var targetElement = element.FindElement<StyledElement>(eventHandlerAttribute.ElementName);
        var routedEvent = RetrieveRoutedEvent(targetElement, eventHandlerAttribute.Event);
        var eventInfo = RetrieveEventInfo(targetElement, eventHandlerAttribute.Event);
        eventHandlers.Add(new AvaloniaEventHandlerItem(
            eventHandlerAttribute.ElementName, targetElement,
            eventHandlerAttribute.Event, routedEvent, eventInfo,
            handlerCreator(routedEvent?.EventArgsType is null ? eventInfo?.EventHandlerType : typeof(EventHandler<>).MakeGenericType(routedEvent.EventArgsType)),
            eventHandlerAttribute.HandledEventsToo,
            CreateParameterResolver(element)
        ));
    }

    protected override EventHandlerAction CreateEventHandlerAction(MethodInfo method, object? target, StyledElement? element)
        => new AvaloniaEventHandlerAction(method, target, CreateParameterDependencyResolver(CreateParameterResolver(element)));

    protected override void OnEventHandlerAdded(EventHandlerBase<StyledElement, AvaloniaEventHandlerItem> eventHandlers, StyledElement element)
    {
        base.OnEventHandlerAdded(eventHandlers, element);

        eventHandlers.GetBy(element.Name)
            .From(element)
            .With(new AvaloniaPropertyChangedEventArgs<object?>(element, StyledElement.DataContextProperty, null, element.DataContext, BindingPriority.LocalValue))
            .Raise(nameof(StyledElement.DataContextChanged));
        eventHandlers.GetBy(element.Name)
            .From(element)
            .With(new LogicalTreeAttachmentEventArgs(element.FindLogicalRoot(), element, element.Parent))
            .Raise(nameof(StyledElement.AttachedToLogicalTree));
    }

    private RoutedEvent? RetrieveRoutedEvent(StyledElement? element, string name)
    {
        if (element is null || string.IsNullOrWhiteSpace(name)) return null;

        return name.Contains(AttachedEventSeparator) ? RetrieveRoutedEvent(name) : RetrieveRoutedEvent(element.GetType(), name);
    }

    private RoutedEvent? RetrieveRoutedEvent(string name)
    {
        var fields = name.Split(AttachedEventSeparator);
        if (fields.Length is not 2) return null;

        var elementName = fields[0];
        var routedEventName = fields[1];

        var elementType = AppDomain.CurrentDomain.GetAssemblies()
            .SelectMany(assembly => assembly.GetTypes())
            .FirstOrDefault(type => type.Name == elementName);

        return elementType is null ? null : RetrieveRoutedEvent(elementType, routedEventName);
    }

    private RoutedEvent? RetrieveRoutedEvent(Type elementType, string name)
        => elementType.GetFields(RoutedEventBindingFlags)
            .Where(field => field.Name == EnsureRoutedEventName(name))
            .Select(field => field.GetValue(null))
            .FirstOrDefault() as RoutedEvent;

    private string EnsureRoutedEventName(string routedEventName)
        =>routedEventName.EndsWith("Event") ? routedEventName : $"{routedEventName}Event";

    private EventInfo? RetrieveEventInfo(StyledElement? element, string name)
        => element?.GetType()
            .GetEvents()
            .FirstOrDefault(e => e.Name == name);
}