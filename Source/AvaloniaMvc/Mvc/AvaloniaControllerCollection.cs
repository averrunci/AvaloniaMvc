// Copyright (C) 2020-2023 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
using Avalonia;
using Avalonia.LogicalTree;

namespace Charites.Windows.Mvc;

/// <summary>
/// Represents a collection of controller objects.
/// </summary>
public sealed class AvaloniaControllerCollection : ControllerCollection<StyledElement>
{
    /// <summary>
    /// Initializes a new instance of the <see cref="AvaloniaControllerCollection"/> class
    /// with the specified <see cref="IAvaloniaDataContextFinder"/>, <see cref="IDataContextInjector"/>,
    /// <see cref="IAvaloniaElementInjector"/>, and the enumerable of the <see cref="IAvaloniaControllerExtension"/>.
    /// </summary>
    /// <param name="dataContextFinder">The finder to find a data context.</param>
    /// <param name="dataContextInjector">The injector to inject a data context.</param>
    /// <param name="elementInjector">The injector to inject elements.</param>
    /// <param name="extensions">The extensions for a controller.</param>
    public AvaloniaControllerCollection(IAvaloniaDataContextFinder dataContextFinder, IDataContextInjector dataContextInjector, IAvaloniaElementInjector elementInjector, IEnumerable<IAvaloniaControllerExtension> extensions) : base(dataContextFinder, dataContextInjector, elementInjector, extensions)
    {
    }

    /// <summary>
    /// Adds the controllers of the specified collection to the end of the <see cref="AvaloniaControllerCollection"/>.
    /// </summary>
    /// <param name="controllers">
    /// The controllers to add to the end of the <see cref="AvaloniaControllerCollection"/>.
    /// If the specified collection is <c>null</c>, nothing is added without throwing an exception.
    /// </param>
    public void AddRange(IEnumerable<object> controllers) => controllers.ForEach(Add);

    /// <summary>
    /// Gets the value that indicates whether the element to which controllers are attached is loaded.
    /// </summary>
    /// <param name="associatedElement">The element to which controllers are attached.</param>
    /// <returns>
    /// <c>true</c> if the element to which controllers are attached is loaded;
    /// otherwise, <c>false</c> is returned.
    /// </returns>
    protected override bool IsAssociatedElementLoaded(StyledElement associatedElement) => ((ILogical)associatedElement).IsAttachedToLogicalTree;

    /// <summary>
    /// Subscribes events of the element to which controllers are attached.
    /// </summary>
    /// <param name="associatedElement">The element to which controllers are attached.</param>
    protected override void SubscribeAssociatedElementEvents(StyledElement associatedElement)
    {
        associatedElement.AttachedToLogicalTree += OnElementAttached;
        associatedElement.DetachedFromLogicalTree += OnElementDetached;
        associatedElement.DataContextChanged += OnElementDataContextChanged;

        if (associatedElement.IsInitialized) OnElementAttached(associatedElement);
    }

    /// <summary>
    /// Unsubscribes events of the element to which controllers are attached.
    /// </summary>
    /// <param name="associatedElement">The element to which controllers are attached.</param>
    protected override void UnsubscribeAssociatedElementEvents(StyledElement associatedElement)
    {
        associatedElement.AttachedToLogicalTree -= OnElementAttached;
        associatedElement.DetachedFromLogicalTree -= OnElementDetached;
        associatedElement.DataContextChanged -= OnElementDataContextChanged;
    }

    private void OnElementAttached(StyledElement element)
    {
        SetElement(element);
        AttachExtensions();
    }

    private void OnElementAttached(object? sender, LogicalTreeAttachmentEventArgs e)
    {
        if (sender is not StyledElement element) return;

        OnElementAttached(element);
    }

    private void OnElementDetached(object? sender, LogicalTreeAttachmentEventArgs e)
    {
        Detach();
    }

    private void OnElementDataContextChanged(object? sender, EventArgs e)
    {
        if (sender is not StyledElement element) return;

        SetDataContext(element.DataContext);
    }
}