// Copyright (C) 2020-2021 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using Avalonia;
using Avalonia.Data;
using Avalonia.Interactivity;
using Avalonia.LogicalTree;

namespace Charites.Windows.Mvc
{
    internal sealed class AvaloniaEventHandlerExtension : EventHandlerExtension<StyledElement, AvaloniaEventHandlerItem>, IAvaloniaControllerExtension
    {
        private const BindingFlags RoutedEventBindingFlags = BindingFlags.Public | BindingFlags.Static | BindingFlags.FlattenHierarchy;
        private const char AttachedEventSeparator = '.';

        private static readonly Regex EventHandlerNamingConventionRegex = new Regex("^[^_]+_[^_]+$", RegexOptions.Compiled);

        private static readonly AvaloniaProperty<IDictionary<object, EventHandlerBase<StyledElement, AvaloniaEventHandlerItem>>> EventHandlerBasesProperty
            = AvaloniaProperty.RegisterAttached<AvaloniaEventHandlerExtension, StyledElement, IDictionary<object, EventHandlerBase<StyledElement, AvaloniaEventHandlerItem>>>("EventHandlerBases");

        protected override IDictionary<object, EventHandlerBase<StyledElement, AvaloniaEventHandlerItem>> EnsureEventHandlerBases(StyledElement element)
        {
            if (element == null) return new Dictionary<object, EventHandlerBase<StyledElement, AvaloniaEventHandlerItem>>();

            if (element.GetValue(EventHandlerBasesProperty) is IDictionary<object, EventHandlerBase<StyledElement, AvaloniaEventHandlerItem>> eventHandlerBases) return eventHandlerBases;

            eventHandlerBases = new Dictionary<object, EventHandlerBase<StyledElement, AvaloniaEventHandlerItem>>();
            element.SetValue(EventHandlerBasesProperty, eventHandlerBases);
            return eventHandlerBases;
        }

        protected override void AddEventHandler(StyledElement element, EventHandlerAttribute eventHandlerAttribute, Func<Type, Delegate> handlerCreator, EventHandlerBase<StyledElement, AvaloniaEventHandlerItem> eventHandlers)
        {
            var targetElement = element.FindElement<StyledElement>(eventHandlerAttribute.ElementName);
            var routedEvent = RetrieveRoutedEvent(targetElement, eventHandlerAttribute.Event);
            var eventInfo = RetrieveEventInfo(element, eventHandlerAttribute.Event);
            eventHandlers.Add(new AvaloniaEventHandlerItem(
                eventHandlerAttribute.ElementName, targetElement,
                eventHandlerAttribute.Event, routedEvent, eventInfo,
                handlerCreator(routedEvent?.EventArgsType == null ? eventInfo?.EventHandlerType : typeof(EventHandler<>).MakeGenericType(routedEvent.EventArgsType)),
                eventHandlerAttribute.HandledEventsToo
            ));
        }

        protected override void RetrieveEventHandlersFromMethodUsingNamingConvention(object controller, StyledElement element, EventHandlerBase<StyledElement, AvaloniaEventHandlerItem> eventHandlers)
            => controller.GetType()
                .GetMethods(EventHandlerBindingFlags)
                .Where(method => EventHandlerNamingConventionRegex.IsMatch(method.Name))
                .Where(method => !method.Name.StartsWith("get_"))
                .Where(method => !method.Name.StartsWith("set_"))
                .Where(method => !method.GetCustomAttributes<EventHandlerAttribute>(true).Any())
                .Select(method =>
                {
                    var separatorIndex = method.Name.IndexOf("_", StringComparison.Ordinal);
                    return new
                    {
                        MethodInfo = method,
                        EventHanndlerAttribute = new EventHandlerAttribute
                        {
                            ElementName = method.Name.Substring(0, separatorIndex),
                            Event = method.Name.Substring(separatorIndex + 1)
                        }
                    };
                })
                .ForEach(x => AddEventHandler(element, x.EventHanndlerAttribute, handlerType => CreateEventHandler(x.MethodInfo, controller, handlerType), eventHandlers));

        protected override EventHandlerAction CreateEventHandlerAction(MethodInfo method, object target) => new AvaloniaEventHandlerAction(method, target);

        protected override void OnEventHandlerAdded(EventHandlerBase<StyledElement, AvaloniaEventHandlerItem> eventHandlers, StyledElement element)
        {
            base.OnEventHandlerAdded(eventHandlers, element);

            eventHandlers.GetBy(element.Name)
                .From(element)
                .With(new AvaloniaPropertyChangedEventArgs<object>(element, StyledElement.DataContextProperty, null, element.DataContext, BindingPriority.LocalValue))
                .Raise(nameof(StyledElement.DataContextChanged));
            eventHandlers.GetBy(element.Name)
                .From(element)
                .With(new LogicalTreeAttachmentEventArgs(element.FindLogicalRoot(), element, element.Parent))
                .Raise(nameof(StyledElement.AttachedToLogicalTree));
        }

        private RoutedEvent RetrieveRoutedEvent(StyledElement element, string name)
        {
            if (element == null || string.IsNullOrWhiteSpace(name)) return null;

            return name.Contains(AttachedEventSeparator) ? RetrieveRoutedEvent(name) : RetrieveRoutedEvent(element.GetType(), name);
        }

        private RoutedEvent RetrieveRoutedEvent(string name)
        {
            var fields = name.Split(AttachedEventSeparator);
            if (fields.Length != 2) return null;

            var elementName = fields[0];
            var routedEventName = fields[1];

            var elementType = AppDomain.CurrentDomain.GetAssemblies()
                .SelectMany(assembly => assembly.GetTypes())
                .FirstOrDefault(type => type.Name == elementName);

            return RetrieveRoutedEvent(elementType, routedEventName);
        }

        private RoutedEvent RetrieveRoutedEvent(Type elementType, string name)
            => elementType.GetFields(RoutedEventBindingFlags)
                .Where(field => field.Name == EnsureRoutedEventName(name))
                .Select(field => field.GetValue(null))
                .FirstOrDefault() as RoutedEvent;

        private string EnsureRoutedEventName(string routedEventName)
            => routedEventName != null && !routedEventName.EndsWith("Event") ? $"{routedEventName}Event" : routedEventName;

        private EventInfo RetrieveEventInfo(StyledElement element, string name)
            => element?.GetType()
                .GetEvents()
                .FirstOrDefault(e => e.Name == name);
    }
}
