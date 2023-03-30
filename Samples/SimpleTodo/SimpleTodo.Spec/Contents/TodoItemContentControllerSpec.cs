// Copyright (C) 2023 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
using Avalonia.Input;
using Carna;
using Charites.Windows.Mvc;
using Charites.Windows.Mvc.Bindings;
using NSubstitute;

namespace Charites.Windows.Samples.SimpleTodo.Contents;

[Specification("TodoItemContentController Spec")]
class TodoItemContentControllerSpec : FixtureSteppable
{
    TodoItemContentController Controller { get; } = new();
    IEditableDisplayContent Content { get; } = Substitute.For<IEditableDisplayContent>();

    public TodoItemContentControllerSpec()
    {
        AvaloniaController.SetDataContext(Content, Controller);
    }

    [Example("Starts editing a content when the element is double tapped")]
    void Ex01()
    {
        When("the element is double tapped", () =>
            AvaloniaController.EventHandlersOf(Controller)
                .GetBy(null)
                .Raise(nameof(InputElement.DoubleTapped))
        );
        Then("the content should be start editing", () => Content.Received(1).StartEdit());
    }
}