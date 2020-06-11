// Copyright (C) 2020 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
using Charites.Windows.Mvc;

namespace Charites.Windows.Samples.SimpleLoginDemo.Presentation.Contents.User
{
    [View(Key = nameof(UserContent))]
    public class UserContentController
    {
        private void SetDataContext(UserContent content) => this.content = content;
        private UserContent content;

        private void LogoutButton_Click()
        {
            content.Logout();
        }
    }
}
