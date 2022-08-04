// Copyright (C) 2020-2022 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
using Carna;

namespace Charites.Windows.Samples.SimpleLoginDemo.Presentation.Contents.Login;

[Specification("LoginContent Spec")]
class LoginContentSpec : FixtureSteppable
{
    LoginContent LoginContent { get; } = new LoginContent();

    [Example("Validates the user id and the password")]
    [Sample("", "", false, Description = "When the user id is empty and the password is empty")]
    [Sample("", "password", false, Description = "When the user id is empty and the password is not null or empty")]
    [Sample("user", "", false, Description = "When the user id is not null or empty and the password is empty")]
    [Sample("user", "password", true, Description = "when the user id is not null or empty and the password is not null or empty")]
    void Ex01(string userId, string password, bool expected)
    {
        When("the user id is set", () => LoginContent.UserId.Value = userId);
        When("the password is set", () => LoginContent.Password.Value = password);
        Then($"the validation result should be {expected}", () => LoginContent.IsValid == expected);
    }

    [Example("Enables or disables the login button")]
    [Sample("", "password", false, Description = "When the user id is empty")]
    [Sample("user", "", false, Description = "When the password is empty")]
    [Sample("user", "password", true, Description = "When the user id is not null or empty and the password is not null or empty")]
    void Ex02(string userId, string password, bool expected)
    {
        When("the user id is set", () => LoginContent.UserId.Value = userId);
        When("the password is set", () => LoginContent.Password.Value = password);
        Then($"the login button should be {(expected ? "enabled" : "disabled")}", () => LoginContent.CanExecute.Value == expected);
    }
}