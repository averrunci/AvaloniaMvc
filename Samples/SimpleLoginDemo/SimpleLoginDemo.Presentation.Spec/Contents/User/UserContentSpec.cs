// Copyright (C) 2020 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
using System;
using Carna;
using NSubstitute;

namespace Charites.Windows.Samples.SimpleLoginDemo.Presentation.Contents.User
{
    [Specification("UserContent Spec")]
    class UserContentSpec : FixtureSteppable
    {
        UserContent UserContent { get; } = new UserContent("User");
        EventHandler LoggedOutEventHandler { get; } = Substitute.For<EventHandler>();

        public UserContentSpec()
        {
            UserContent.LoggedOut += LoggedOutEventHandler;
        }

        [Example("Raises the LoggedOut event")]
        void Ex01()
        {
            When("to log out", () => UserContent.Logout());
            Then("the LoggedOut event should be raised", () => LoggedOutEventHandler.Received(1).Invoke(UserContent, EventArgs.Empty));
        }
    }
}
