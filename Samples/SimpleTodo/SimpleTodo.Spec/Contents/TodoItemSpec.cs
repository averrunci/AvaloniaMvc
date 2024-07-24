// Copyright (C) 2022-2024 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
using Carna;
using NSubstitute;

namespace Charites.Windows.Samples.SimpleTodo.Contents;

[Specification($"{nameof(TodoItem)} Spec")]
class TodoItemSpec : FixtureSteppable
{
    TodoItem TodoItem { get; }

    string InitialContent => "Todo Item";

    EventHandler RemoveRequestedHandler { get; } = Substitute.For<EventHandler>();

    public TodoItemSpec()
    {
        TodoItem = new TodoItem(InitialContent);
        TodoItem.RemoveRequested += RemoveRequestedHandler;
    }

    [Example("Removes a to-do")]
    void Ex01()
    {
        When("to remove", () => TodoItem.Remove());
        Then("it should be requested to remove the to-do", () =>
            RemoveRequestedHandler.Received(1).Invoke(TodoItem, EventArgs.Empty)
        );
    }

    [Example("Completes and reverts a to-do")]
    void Ex02()
    {
        Expect("the state should be Active", () => TodoItem.State.Value == TodoItemState.Active);

        When("to complete", () => TodoItem.Complete());
        Then("the state should be Completed", () => TodoItem.State.Value == TodoItemState.Completed);

        When("to revert", () => TodoItem.Revert());
        Then("the state should be Active", () => TodoItem.State.Value == TodoItemState.Active);
    }

    [Example("Gets a value that indicates whether a to-do is completed")]
    [Sample(TodoItemState.Active, false, Description = "When a state is Active")]
    [Sample(TodoItemState.Completed, true, Description = "When a state is Completed")]
    [Sample(TodoItemState.All, false, Description = "When a state is All")]
    void Ex03(TodoItemState state, bool expected)
    {
        TodoItem.State.Value = state;
        Expect($"the value should be {expected}", () => TodoItem.IsCompleted.Value == expected);
    }
}