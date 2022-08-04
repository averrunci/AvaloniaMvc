// Copyright (C) 2020-2022 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
using Charites.Windows.Mvc;
using Charites.Windows.Samples.SimpleLoginDemo.Presentation.Contents.User;
using Charites.Windows.Samples.SimpleLoginDemo.Presentation.Properties;

namespace Charites.Windows.Samples.SimpleLoginDemo.Presentation.Contents.Login;

[View(Key = nameof(LoginContent))]
public class LoginContentController
{
    private async Task LoginButton_ClickAsync([FromDataContext] LoginContent content, [FromDI] IContentNavigator navigator, [FromDI] ILoginCommand loginCommand)
    {
        content.Message.Value = string.Empty;

        if (!content.IsValid) return;

        var result = await loginCommand.AuthenticateAsync(content);
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