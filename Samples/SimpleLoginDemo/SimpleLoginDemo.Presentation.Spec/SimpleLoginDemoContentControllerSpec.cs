// Copyright (C) 2021 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
using System;
using Avalonia;
using Carna;
using Charites.Windows.Mvc;
using Charites.Windows.Samples.SimpleLoginDemo.Presentation.Contents.Login;
using NSubstitute;

namespace Charites.Windows.Samples.SimpleLoginDemo.Presentation
{
    [Specification("SimpleLoginDemoContentController Spec")]
    class SimpleLoginDemoContentControllerSpec : FixtureSteppable
    {
        SimpleLoginDemoContentController Controller { get; }

        SimpleLoginDemoContent Content { get; } = new SimpleLoginDemoContent();
        IContentNavigator Navigator { get; } = Substitute.For<IContentNavigator>();

        object NextContent { get; } = new object();

        public SimpleLoginDemoContentControllerSpec()
        {
            Controller = new SimpleLoginDemoContentController(Navigator);

            AvaloniaController.SetDataContext(Content, Controller);
        }

        [Example("Navigates to an initial content")]
        void Ex01()
        {
            When("to attach the SimpleLoginDemoContent to the logical tree", () =>
                AvaloniaController.EventHandlersOf(Controller)
                    .GetBy(null)
                    .Raise(nameof(StyledElement.AttachedToLogicalTree))
            );
            Then("the content should be navigated to the LoginContent", () =>
            {
                Navigator.Received(1).NavigateTo(Arg.Any<LoginContent>());
            });
        }

        [Example("Sets a new content when the content is navigated to the next content")]
        void Ex02()
        {
            When("the content is navigated to the next content", () =>
                Navigator.Navigated += Raise.EventWith(Navigator, new ContentNavigatedEventArgs(ContentNavigationMode.New, NextContent, Content.Content.Value))
            );
            Then("the next content should be set to the content of the SimpleLoginDemoContent", () => Content.Content.Value == NextContent);
        }

        [Example("When the IContentNavigator is not specified")]
        void Ex03()
        {
            When("a controller to which the IContentNavigator is not specified is created", () => new SimpleLoginDemoContentController(null));
            Then<ArgumentNullException>($"{typeof(ArgumentNullException)} should be thrown");
        }
    }
}
