// Copyright (C) 2020 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
using Carna;
using Charites.Windows.Samples.SimpleLoginDemo.Presentation.Contents.Login;
using NSubstitute;

namespace Charites.Windows.Samples.SimpleLoginDemo.Presentation
{
    [Specification("SimpleLoginDemoContent Spec")]
    class SimpleLoginDemoContentSpec : FixtureSteppable
    {
        SimpleLoginDemoContent SimpleLoginDemoContent { get; }

        ILoginDemoContent InitialContent { get; } = Substitute.For<ILoginDemoContent>();
        ILoginDemoContent NextContent { get; } = Substitute.For<ILoginDemoContent>();
        ILoginDemoContent AnotherContent { get; } = Substitute.For<ILoginDemoContent>();

        [Background("The SimpleLoginDemoContent with an initial content is created")]
        public SimpleLoginDemoContentSpec()
        {
            SimpleLoginDemoContent = new SimpleLoginDemoContent(InitialContent);
        }

        [Example("Sets a content when it is requested from the current content")]
        void Ex01()
        {
            When("the content is requested from the current content", () =>
                InitialContent.ContentRequested += Raise.EventWith(InitialContent, new ContentRequestedEventArgs(NextContent))
            );
            Then("the requested content should be set to the current content", () => SimpleLoginDemoContent.Content.Value == NextContent);

            When("another content is requested from the initial content that is changed from the current content", () =>
                InitialContent.ContentRequested += Raise.EventWith(InitialContent, new ContentRequestedEventArgs(AnotherContent))
            );
            Then("the current content should not be changed", () => SimpleLoginDemoContent.Content.Value == NextContent);
        }

        [Example("Sets the LoginContent when to log out in the current content")]
        void Ex02()
        {
            When("to log out in the current content", () => InitialContent.LoggedOut += Raise.Event());
            Then("the LoginContent should be set to the current content", () => SimpleLoginDemoContent.Content.Value.GetType() == typeof(LoginContent));

            When("to change the current content", () => SimpleLoginDemoContent.Content.Value = NextContent);
            When("to log out in the initial content that is changed from the current content", () => InitialContent.LoggedOut += Raise.Event());
            Then("the current content should not be changed", () => SimpleLoginDemoContent.Content.Value == NextContent);
        }
    }
}
