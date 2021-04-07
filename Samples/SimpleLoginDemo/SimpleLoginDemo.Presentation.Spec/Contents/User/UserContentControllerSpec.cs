// Copyright (C) 2020-2021 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
using System;
using Avalonia.Controls;
using Carna;
using Charites.Windows.Mvc;
using Charites.Windows.Samples.SimpleLoginDemo.Presentation.Contents.Login;
using NSubstitute;

namespace Charites.Windows.Samples.SimpleLoginDemo.Presentation.Contents.User
{
    [Specification("UserContentController Spec")]
    class UserContentControllerSpec : FixtureSteppable
    {
        UserContentController Controller { get; }

        IContentNavigator Navigator { get; } = Substitute.For<IContentNavigator>();

        public UserContentControllerSpec()
        {
            Controller = new UserContentController(Navigator);
        }

        [Example("Logs the user out")]
        void Ex01()
        {
            When("to click the Logout button", () =>
                AvaloniaController.EventHandlersOf(Controller)
                    .GetBy("LogoutButton")
                    .Raise(nameof(Button.Click))
            );
            Then("the content should be navigated to the LoginContent", () =>
            {
                Navigator.Received(1).NavigateTo(Arg.Any<LoginContent>());
            });
        }

        [Example("When the IContentNavigate is not specified")]
        void Ex02()
        {
            When("a controller to which the IContentNavigator is not specified is created", () => new UserContentController(null));
            Then<ArgumentNullException>($"{typeof(ArgumentNullException)} should be thrown");
        }
    }
}
