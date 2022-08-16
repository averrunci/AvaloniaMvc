// Copyright (C) 2022 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
using Avalonia.Controls;
using Avalonia.Input;
using Carna;
using Charites.Windows.Mvc;
using NSubstitute;

namespace Charites.Windows.Samples.SimpleTodo.Contents;

[Specification("TodoItemController Spec")]
class TodoItemControllerSpec : FixtureSteppable
{
    TodoItemController Controller { get; } = new();
    TodoItem TodoItem { get; }

    string InitialContent => "Todo Item";
    string ModifiedContent => "Todo Item Modified";

    EventHandler RemovedRequestedHandler { get; } = Substitute.For<EventHandler>();

    public TodoItemControllerSpec()
    {
        TodoItem = new TodoItem(InitialContent);
        TodoItem.RemoveRequested += RemovedRequestedHandler;
    }

    [Example("Switches a content to an edit mode when the element is double tapped")]
    void Ex01()
    {
        When("the element is double tapped", () =>
            AvaloniaController.EventHandlersOf(Controller)
                .GetBy("TodoItemContainer")
                .ResolveFromDataContext(TodoItem)
                .ResolveFromElement("TodoContentTextBox", new TextBox())
                .Raise(nameof(InputElement.DoubleTapped))
        );
        Then("the to-do item should be editing", () => TodoItem.IsEditing.Value);
        Then("the edit content of the to-do item should be the initial content", () => TodoItem.EditContent.Value == InitialContent);
    }

    [Example("Completes an edit when the Enter key is pressed")]
    void Ex02()
    {
        When("the element is double tapped", () =>
            AvaloniaController.EventHandlersOf(Controller)
                .GetBy("TodoItemContainer")
                .ResolveFromDataContext(TodoItem)
                .ResolveFromElement("TodoContentTextBox", new TextBox())
                .Raise(nameof(InputElement.DoubleTapped))
        );
        When("the content is modified", () => TodoItem.EditContent.Value = ModifiedContent);
        When("the Enter key is pressed", () =>
            AvaloniaController.EventHandlersOf(Controller)
                .GetBy("TodoContentTextBox")
                .With(new KeyEventArgs { Key = Key.Enter })
                .ResolveFromDataContext(TodoItem)
                .Raise(nameof(InputElement.KeyDown))
        );
        Then("the to-do item should not be editing", () => !TodoItem.IsEditing.Value);
        Then("the content of the to-do item should be the modified content", () => TodoItem.Content.Value == ModifiedContent);
    }

    [Example("Completes an edit when the focus is lost")]
    void Ex03()
    {
        When("the element is double tapped", () =>
            AvaloniaController.EventHandlersOf(Controller)
                .GetBy("TodoItemContainer")
                .ResolveFromDataContext(TodoItem)
                .ResolveFromElement("TodoContentTextBox", new TextBox())
                .Raise(nameof(InputElement.DoubleTapped))
        );
        When("the content is modified", () => TodoItem.EditContent.Value = ModifiedContent);
        When("the focus is lost", () =>
            AvaloniaController.EventHandlersOf(Controller)
                .GetBy("TodoContentTextBox")
                .ResolveFromDataContext(TodoItem)
                .Raise(nameof(InputElement.LostFocus))
        );
        Then("the to-do item should not be editing", () => !TodoItem.IsEditing.Value);
        Then("the content of the to-do item should be the modified content", () => TodoItem.Content.Value == ModifiedContent);
    }

    [Example("Cancels an edit when the Esc key is pressed")]
    void Ex04()
    {
        When("the element is double tapped", () =>
            AvaloniaController.EventHandlersOf(Controller)
                .GetBy("TodoItemContainer")
                .ResolveFromDataContext(TodoItem)
                .ResolveFromElement("TodoContentTextBox", new TextBox())
                .Raise(nameof(InputElement.DoubleTapped))
        );
        When("the content is modified", () => TodoItem.EditContent.Value = ModifiedContent);
        When("the Esc key is pressed", () =>
            AvaloniaController.EventHandlersOf(Controller)
                .GetBy("TodoContentTextBox")
                .With(new KeyEventArgs { Key = Key.Escape })
                .ResolveFromDataContext(TodoItem)
                .Raise(nameof(InputElement.KeyDown))
        );
        Then("the to-do item should not be editing", () => !TodoItem.IsEditing.Value);
        Then("the content of the to-do item should be the initial content", () => TodoItem.Content.Value == InitialContent);
    }

    [Example("Removes a to-do item when the delete button is clicked")]
    void Ex05()
    {
        When("the delete button is clicked", () =>
            AvaloniaController.EventHandlersOf(Controller)
                .GetBy("DeleteButton")
                .ResolveFromDataContext(TodoItem)
                .Raise(nameof(Button.Click))
        );
        Then("it should be requested to remove the to-do item", () =>
            RemovedRequestedHandler.Received(1).Invoke(TodoItem, EventArgs.Empty)
        );
    }
}