// Copyright (C) 2020 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
using Charites.Windows.Mvc;
using Charites.Windows.Samples.SimpleLoginDemo.Presentation.Properties;

namespace Charites.Windows.Samples.SimpleLoginDemo.Presentation.Contents.Login
{
    [View(Key = nameof(LoginContent))]
    public class LoginContentController
    {
        private readonly IUserAuthentication userAuthentication;

        public LoginContentController(IUserAuthentication userAuthentication)
        {
            this.userAuthentication = userAuthentication;
        }

        private void SetDataContext(LoginContent content) => this.content = content;
        private LoginContent content;

        private void LoginButton_Click()
        {
            content.Message.Value = string.Empty;

            if (userAuthentication == null)
            {
                content.Message.Value = Resources.LoginNotAvailable;
                return;
            }

            if (!content.IsValid) return;

            var result = userAuthentication.Authenticate(content.UserId.Value, content.Password.Value);
            if (result.Success)
            {
                content.Login();
            }
            else
            {
                content.Message.Value = Resources.LoginFailureMessage;
            }
        }
    }
}
