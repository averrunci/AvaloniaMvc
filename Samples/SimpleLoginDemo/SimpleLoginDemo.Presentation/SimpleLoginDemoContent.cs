// Copyright (C) 2020 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
using System;
using Charites.Windows.Mvc.Bindings;
using Charites.Windows.Samples.SimpleLoginDemo.Presentation.Contents.Login;

namespace Charites.Windows.Samples.SimpleLoginDemo.Presentation
{
    public class SimpleLoginDemoContent
    {
        public ObservableProperty<ILoginDemoContent> Content { get; } = new ObservableProperty<ILoginDemoContent>();

        public SimpleLoginDemoContent(ILoginDemoContent initialContent)
        {
            Content.PropertyValueChanged += (s, e) =>
            {
                UnsubscribeLoginDemoContentEvents(e.OldValue);
                SubscribeLoginDemoContentEvents(e.NewValue);
            };
            Content.Value = initialContent;
        }

        private void SubscribeLoginDemoContentEvents(ILoginDemoContent content)
        {
            if (content == null) return;

            content.ContentRequested += LoginDemoContent_ContentRequested;
            content.LoggedOut += LoginDemoContent_LoggedOut;
        }

        private void UnsubscribeLoginDemoContentEvents(ILoginDemoContent content)
        {
            if (content == null) return;

            content.ContentRequested -= LoginDemoContent_ContentRequested;
            content.LoggedOut -= LoginDemoContent_LoggedOut;
        }

        private void LoginDemoContent_ContentRequested(object sender, ContentRequestedEventArgs e)
        {
            Content.Value = e.RequestedContent;
        }

        private void LoginDemoContent_LoggedOut(object sender, EventArgs e)
        {
            Content.Value = new LoginContent();
        }
    }
}
