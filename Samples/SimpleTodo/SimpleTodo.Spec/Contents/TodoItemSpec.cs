// Copyright (C) 2022 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
using Carna;
using NSubstitute;

namespace Charites.Windows.Samples.SimpleTodo.Contents;

[Specification("TodoItem Spec")]
class TodoItemSpec : FixtureSteppable
{
    TodoItem TodoItem { get; }

    string InitialContent => "Todo Item";
    string ModifiedContent => "Todo Item Modified";

    EventHandler RemoveRequestedHandler { get; } = Substitute.For<EventHandler>();

    public TodoItemSpec()
    {
        TodoItem = new TodoItem(InitialContent);
        TodoItem.RemoveRequested += RemoveRequestedHandler;
    }

    [Example("Edits the content")]
    void Ex01()
    {
        Expect("the IsEditing should be false", () => !TodoItem.IsEditing.Value);

        When("to start an edit", () => TodoItem.StartEdit());
        Then("the IsEditing should be true", () => TodoItem.IsEditing.Value);
        Then("the edit content should be the initial content", () => TodoItem.EditContent.Value == InitialContent);

        When("the content is modified", () => TodoItem.EditContent.Value = ModifiedContent);
        When("to complete the edit", () => TodoItem.CompleteEdit());
        Then("the IsEditing should be false", () => !TodoItem.IsEditing.Value);
        Then("the content should be the modified content", () => TodoItem.Content.Value == ModifiedContent);
    }

    [Example("Cancels an edit of the content")]
    void Ex02()
    {
        Expect("the IsEditing should be false", () => !TodoItem.IsEditing.Value);

        When("to start an edit", () => TodoItem.StartEdit());
        Then("the IsEditing should be true", () => TodoItem.IsEditing.Value);
        Then("the edit content should be the initial content", () => TodoItem.EditContent.Value == InitialContent);

        When("the content is modified", () => TodoItem.EditContent.Value = ModifiedContent);
        When("to cancel the edit", () => TodoItem.CancelEdit());
        Then("the IsEditing should be false", () => !TodoItem.IsEditing.Value);
        Then("the content should be the initial content", () => TodoItem.Content.Value == InitialContent);
    }

    [Example("Removes a to-do")]
    void Ex03()
    {
        When("to remove", () => TodoItem.Remove());
        Then("it should be requested to remove the to-do", () =>
            RemoveRequestedHandler.Received(1).Invoke(TodoItem, EventArgs.Empty)
        );
    }

    [Example("Completes and reverts a to-do")]
    void Ex04()
    {
        Expect("the state should be Active", () => TodoItem.State.Value == TodoItemState.Active);

        When("to complete", () => TodoItem.Complete());
        Then("the state should be Completed", () => TodoItem.State.Value == TodoItemState.Completed);

        When("to revert", () => TodoItem.Revert());
        Then("the state should be Active", () => TodoItem.State.Value == TodoItemState.Active);
    }
}