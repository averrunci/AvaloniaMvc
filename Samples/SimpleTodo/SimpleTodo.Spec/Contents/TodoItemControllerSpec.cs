// Copyright (C) 2022-2023 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
using Avalonia.Controls;
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

    [Example("Removes a to-do item when the delete button is clicked")]
    void Ex01()
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