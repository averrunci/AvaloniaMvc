// Copyright (C) 2020-2021 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
using System;
using System.Threading.Tasks;
using Charites.Windows.Mvc;
using Charites.Windows.Samples.SimpleLoginDemo.Presentation.Contents.User;
using Charites.Windows.Samples.SimpleLoginDemo.Presentation.Properties;

namespace Charites.Windows.Samples.SimpleLoginDemo.Presentation.Contents.Login
{
    [View(Key = nameof(LoginContent))]
    public class LoginContentController
    {
        private readonly IContentNavigator navigator;
        private readonly IUserAuthentication userAuthentication;

        public LoginContentController(IContentNavigator navigator, IUserAuthentication userAuthentication)
        {
            this.navigator = navigator ?? throw new ArgumentNullException(nameof(navigator));
            this.userAuthentication = userAuthentication;
        }

        private void SetDataContext(LoginContent content) => this.content = content;
        private LoginContent content;

        private async Task LoginButton_ClickAsync()
        {
            content.Message.Value = string.Empty;

            if (userAuthentication == null)
            {
                content.Message.Value = Resources.LoginNotAvailable;
                return;
            }

            if (!content.IsValid) return;

            var result = await userAuthentication.AuthenticateAsync(content.UserId.Value, content.Password.Value);
            if (result.Success)
            {
                navigator.NavigateTo(new UserContent(content.UserId.Value));
            }
            else
            {
                content.Message.Value = Resources.LoginFailureMessage;
            }
        }
    }
}
