# Release note

## v1.0.0

### Add

- Add the IAvaloniaElementFinder interface that extends IElementFinder&lt;StyledElement&gt; interface.
- Add the ElementFinder property to the AvaloniaController class.
- Add the RoutedEventHandlerAttribute class so that the RoutingStrategies can be specified.
- Add the AvaloniaMvcDataTemplates class that includes the following data templates.
  - A data template whose data type is the IEditableDisplayContent interface.
  - A data template whose data type is the IEditableEditText interface.
  - A data template whose data type is the IEditableEditSelection interface.

### Changes

- Change the target framework version to .NET 6.0.
- Change Avalonia.Desktop version to 11.0.0.
- Change Charites version to 2.2.0.
- Change Charites.Bindings version to 2.2.0.
- Enable Nullable reference types.
- Change the AvaloniaEventHandlerExtension class so that event handlers that have parameters attributed by the following attribute can be injected .
  - FromDIAttribute
  - FromElementAttribute
  - FromDataContextAttribute

### Bug fix

- Fixed an issue where events that are not a routed event can't be handled.
- Fixed a value that indicates whether an element to which controllers are attached is loaded.
- Fix an issue that the AttachedToLogicalTree and the DataContextChanged events can't be handled on a controller is attached when a name of a root element is specified and a name of its event handler is not specified.

## v0.10.1

### Changes

- Change Avalonia.Desktop version to 0.10.7.
- Change Charites version to 1.3.2.
- Modify how to retrieve an event name from a method that represents an event handler using naming convention. If its name ends with "Async", it is ignored.

## v0.10.0

### Changes

- Change Avalonia.Desktop version to 0.10.0.
- Change Charites version to 1.3.1.
- Change Charites.Bindings version to 1.2.1.
