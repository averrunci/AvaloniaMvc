// Copyright (C) 2020 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
using Avalonia.Controls;
using Carna;
using Charites.Windows.Mvc;
using NSubstitute;

namespace Charites.Windows.Samples.SimpleLoginDemo.Presentation.Contents.User
{
    [Specification("UserContentController Spec")]
    class UserContentControllerSpec : FixtureSteppable
    {
        UserContentController Controller { get; } = new UserContentController();

        UserContent UserContent { get; } = Substitute.For<UserContent>("User");

        public UserContentControllerSpec()
        {
            AvaloniaController.SetDataContext(UserContent, Controller);
        }

        [Example("Logs the user out")]
        void Ex01()
        {
            When("to click the Logout button", () =>
                AvaloniaController.EventHandlersOf(Controller)
                    .GetBy("LogoutButton")
                    .Raise(nameof(Button.Click))
            );
            Then("the Logout of the UserContent is called", () => UserContent.Received().Logout());
        }
    }
}
