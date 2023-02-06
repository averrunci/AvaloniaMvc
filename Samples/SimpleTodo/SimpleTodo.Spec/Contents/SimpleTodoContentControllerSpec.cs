// Copyright (C) 2022-2023 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
using Avalonia.Input;
using Carna;
using Charites.Windows.Mvc;

namespace Charites.Windows.Samples.SimpleTodo.Contents;

[Specification("SimpleTodoContentController Spec")]
class SimpleTodoContentControllerSpec : FixtureSteppable
{
    SimpleTodoContentController Controller { get; } = new();
    SimpleTodoContent Content { get; } = new();

    string TodoContent => "Todo Item";

    public SimpleTodoContentControllerSpec()
    {
        AvaloniaController.SetDataContext(Content, Controller);
    }

    [Example("A to-do item is added when the Enter key is pressed")]
    void Ex01()
    {
        When("the content of the to-do is set", () => Content.TodoContent.Value = TodoContent);
        When("the Enter key is pressed", () =>
            AvaloniaController.EventHandlersOf(Controller)
                .GetBy("TodoContentTextBox")
                .With(new KeyEventArgs { Key = Key.Enter })
                .Raise(nameof(InputElement.KeyDown))
        );
        Then("a to-do item should be added", () => Content.TodoItems.Count() == 1);
    }

    [Example("A to-do item is not added when the Tab key is pressed")]
    void Ex02()
    {
        When("the content of the to-do is set", () => Content.TodoContent.Value = TodoContent);
        When("the Tab key is pressed", () =>
            AvaloniaController.EventHandlersOf(Controller)
                .GetBy("TodoContentTextBox")
                .With(new KeyEventArgs { Key = Key.Tab })
                .Raise(nameof(InputElement.KeyDown))
        );
        Then("a to-do item should not be added", () => !Content.TodoItems.Any());
    }
}