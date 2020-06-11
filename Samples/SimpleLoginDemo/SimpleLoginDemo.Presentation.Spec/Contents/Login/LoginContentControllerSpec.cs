// Copyright (C) 2020 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
using System;
using Avalonia.Controls;
using Carna;
using Charites.Windows.Mvc;
using Charites.Windows.Samples.SimpleLoginDemo.Presentation.Contents.User;
using Charites.Windows.Samples.SimpleLoginDemo.Presentation.Properties;
using NSubstitute;

namespace Charites.Windows.Samples.SimpleLoginDemo.Presentation.Contents.Login
{
    [Specification("LoginContentController Spec")]
    class LoginContentControllerSpec : FixtureSteppable
    {
        LoginContentController Controller { get; set; }

        LoginContent LoginContent { get; } = new LoginContent { Message = { Value = "message" } };
        IUserAuthentication UserAuthentication { get; } = Substitute.For<IUserAuthentication>();
        EventHandler<ContentRequestedEventArgs> ContentRequestedEventHandler { get; } = Substitute.For<EventHandler<ContentRequestedEventArgs>>();

        public LoginContentControllerSpec()
        {
            Controller = new LoginContentController(UserAuthentication);
            LoginContent.ContentRequested += ContentRequestedEventHandler;

            AvaloniaController.SetDataContext(LoginContent, Controller);
        }

        [Example("When the user is authenticated")]
        void Ex01()
        {
            When("the valid user id and password are set", () =>
            {
                LoginContent.UserId.Value = "user";
                LoginContent.Password.Value = "password";

                UserAuthentication.Authenticate(LoginContent.UserId.Value, LoginContent.Password.Value)
                    .Returns(UserAuthenticationResult.Succeeded());
            });
            When("to click the login button", () =>
                AvaloniaController.EventHandlersOf(Controller)
                    .GetBy("LoginButton")
                    .Raise(nameof(Button.Click))
            );
            Then("the ContentRequested event should be raised", () =>
                ContentRequestedEventHandler.Received(1).Invoke(
                    LoginContent,
                    Arg.Is<ContentRequestedEventArgs>(e => (e.RequestedContent as UserContent).Id == LoginContent.UserId.Value)
                )
            );
        }

        [Example("When the IUserAuthentication is not specified")]
        void Ex02()
        {
            Given("a controller to which the IUserAuthentication is not specified", () =>
            {
                Controller = new LoginContentController(null);
                AvaloniaController.SetDataContext(LoginContent, Controller);
            });
            When("to click the login button", () =>
                AvaloniaController.EventHandlersOf(Controller)
                    .GetBy("LoginButton")
                    .Raise(nameof(Button.Click))
            );
            Then("the ContentRequested event should not be raised", () =>
                ContentRequestedEventHandler.DidNotReceive().Invoke(Arg.Any<object>(), Arg.Any<ContentRequestedEventArgs>())
            );
            Then("the LoginNotAvailable message should be set", () => LoginContent.Message.Value == Resources.LoginNotAvailable);
        }

        [Example("When the login content is not valid")]
        void Ex03()
        {
            When("the invalid user id and password are set", () =>
            {
                LoginContent.UserId.Value = null;
                LoginContent.Password.Value = null;
            });
            When("to click the login button", () =>
                AvaloniaController.EventHandlersOf(Controller)
                    .GetBy("LoginButton")
                    .Raise(nameof(Button.Click))
            );
            Then("the ContentRequested event should not be raised", () =>
                ContentRequestedEventHandler.DidNotReceive().Invoke(Arg.Any<object>(), Arg.Any<ContentRequestedEventArgs>())
            );
            Then("the message of the login content should be empty", () => LoginContent.Message.Value == string.Empty);
        }

        [Example("When the user is not authenticated")]
        void Ex04()
        {
            When("the no authenticated user id and password are set", () =>
            {
                LoginContent.UserId.Value = "user";
                LoginContent.Password.Value = "password";

                UserAuthentication.Authenticate(LoginContent.UserId.Value, LoginContent.Password.Value)
                    .Returns(UserAuthenticationResult.Failed());
            });
            When("to click the login button", () =>
                AvaloniaController.EventHandlersOf(Controller)
                    .GetBy("LoginButton")
                    .Raise(nameof(Button.Click))
            );
            Then("the ContentRequested event should not be raised", () =>
                ContentRequestedEventHandler.DidNotReceive().Invoke(Arg.Any<object>(), Arg.Any<ContentRequestedEventArgs>())
            );
            Then("the LoginFailureMessage message should be set", () => LoginContent.Message.Value == Resources.LoginFailureMessage);
        }
    }
}
