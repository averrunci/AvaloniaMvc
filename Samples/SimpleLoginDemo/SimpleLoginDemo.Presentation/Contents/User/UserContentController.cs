// Copyright (C) 2020-2021 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
using System;
using Charites.Windows.Mvc;
using Charites.Windows.Samples.SimpleLoginDemo.Presentation.Contents.Login;

namespace Charites.Windows.Samples.SimpleLoginDemo.Presentation.Contents.User
{
    [View(Key = nameof(UserContent))]
    public class UserContentController
    {
        private readonly IContentNavigator navigator;

        public UserContentController(IContentNavigator navigator)
        {
            this.navigator = navigator ?? throw new ArgumentNullException(nameof(navigator));
        }

        private void LogoutButton_Click()
        {
            navigator.NavigateTo(new LoginContent());
        }
    }
}
