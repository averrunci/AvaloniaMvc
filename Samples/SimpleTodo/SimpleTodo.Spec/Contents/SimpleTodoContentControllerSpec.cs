// Copyright (C) 2022 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
using Avalonia.Controls;
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

    [Example("The IsChecked of the AllCompletedCheckBox is set to null when the AllCompleted of the content does not have a value")]
    void Ex01()
    {
        var allCompletedCheckBox = new CheckBox { Name = "AllCompletedCheckBox" };
        AvaloniaController.SetElement(allCompletedCheckBox, Controller, true);
        When("AllCompleted of the content is set to true", () => Content.AllCompleted.Value = true);
        When("IsChecked of the AllCompletedCheckBox is set to true", () => allCompletedCheckBox.IsChecked = true);
        When("the AllCompleted of the content is set to null", () => Content.AllCompleted.Value = null);
        Then("the IsChecked of the AllCompletedCheckBox should be null", () => allCompletedCheckBox.IsChecked = null);
    }

    [Example("A to-do item is added when the Enter key is pressed")]
    void Ex02()
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
    void Ex03()
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