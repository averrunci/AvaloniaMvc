// Copyright (C) 2021 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
using System;
using Avalonia;
using Charites.Windows.Mvc;
using Charites.Windows.Samples.SimpleLoginDemo.Presentation.Contents.Login;

namespace Charites.Windows.Samples.SimpleLoginDemo.Presentation
{
    [View(Key = nameof(SimpleLoginDemoContent))]
    public class SimpleLoginDemoContentController : IDisposable
    {
        private readonly IContentNavigator navigator;

        public SimpleLoginDemoContentController(IContentNavigator navigator)
        {
            this.navigator = navigator ?? throw new ArgumentNullException(nameof(navigator));

            SubscribeContentNavigatorEvent();
        }

        private void SetDataContext(SimpleLoginDemoContent content) => this.content = content;
        private SimpleLoginDemoContent content;

        public void Dispose()
        {
            UnsubscribeContentNavigatorEvent();
        }

        private void SubscribeContentNavigatorEvent()
        {
            navigator.Navigated += OnContentNavigated;
        }

        private void UnsubscribeContentNavigatorEvent()
        {
            navigator.Navigated -= OnContentNavigated;
        }

        private void OnContentNavigated(object sender, ContentNavigatedEventArgs e)
        {
            content.Content.Value = e.Content;
        }

        [EventHandler(Event = nameof(StyledElement.AttachedToLogicalTree))]
        private void OnAttachedToLogicalTree()
        {
            navigator.NavigateTo(new LoginContent());
        }
    }
}
