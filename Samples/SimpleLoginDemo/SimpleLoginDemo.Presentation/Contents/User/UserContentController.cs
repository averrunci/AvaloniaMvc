// Copyright (C) 2020-2022 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
using Charites.Windows.Mvc;
using Charites.Windows.Samples.SimpleLoginDemo.Presentation.Contents.Login;

namespace Charites.Windows.Samples.SimpleLoginDemo.Presentation.Contents.User;

[View(Key = nameof(UserContent))]
public class UserContentController
{
    private void LogoutButton_Click([FromDI] IContentNavigator navigator)
    {
        navigator.NavigateTo(new LoginContent());
    }
}