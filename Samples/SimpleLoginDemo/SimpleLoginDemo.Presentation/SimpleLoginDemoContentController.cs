// Copyright (C) 2021-2023 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
using Avalonia;
using Charites.Windows.Mvc;
using Charites.Windows.Samples.SimpleLoginDemo.Presentation.Contents.Login;

namespace Charites.Windows.Samples.SimpleLoginDemo.Presentation;

[View(Key = nameof(SimpleLoginDemoContent))]
public class SimpleLoginDemoContentController : ControllerBase<SimpleLoginDemoContent>, IDisposable
{
    private readonly IContentNavigator navigator;

    public SimpleLoginDemoContentController(IContentNavigator navigator)
    {
        this.navigator = navigator;

        SubscribeToContentNavigatorEvent();
    }

    public void Dispose()
    {
        UnsubscribeFromContentNavigatorEvent();
    }

    private void SubscribeToContentNavigatorEvent()
    {
        navigator.Navigated += OnContentNavigated;
    }

    private void UnsubscribeFromContentNavigatorEvent()
    {
        navigator.Navigated -= OnContentNavigated;
    }

    private void Navigate(SimpleLoginDemoContent content, object navigatedContent)
    {
        content.Content.Value = navigatedContent;
    }

    private void NavigateToLoginContent()
    {
        navigator.NavigateTo(new LoginContent());
    }

    private void OnContentNavigated(object? sender, ContentNavigatedEventArgs e) => DataContext.IfPresent(e.Content, Navigate);

    [EventHandler(Event = nameof(StyledElement.AttachedToLogicalTree))]
    private void OnAttachedToLogicalTree() => NavigateToLoginContent();
}