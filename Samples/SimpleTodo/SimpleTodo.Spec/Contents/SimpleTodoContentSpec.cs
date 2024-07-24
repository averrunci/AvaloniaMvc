// Copyright (C) 2022^2024 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
using Carna;
using Charites.Windows.Samples.SimpleTodo.AssertionObjects;

namespace Charites.Windows.Samples.SimpleTodo.Contents;

[Specification($"{nameof(SimpleTodoContent)} Spec")]
class SimpleTodoContentSpec : FixtureSteppable
{
    SimpleTodoContent SimpleTodoContent { get; } = new();

    string TodoContent1 => "Todo Item 1";
    string TodoContent2 => "Todo Item 2";
    string TodoContent3 => "Todo Item 3";

    void AddTodoContent(string content)
    {
        SimpleTodoContent.TodoContent.Value = content;
        SimpleTodoContent.AddCurrentTodoContent();
    }

    [Example("Initial state")]
    void Ex01()
    {
        Expect("the Items should be empty", () => !SimpleTodoContent.TodoItems.Any());
        Expect("the TodoContent should be empty", () => SimpleTodoContent.TodoContent.Value == string.Empty);
        Expect("the AllCompleted should be false", () => SimpleTodoContent.AllCompleted.Value == false);
        Expect("the AllCompleted should be invisible", () => !SimpleTodoContent.CanCompleteAllTodoItems.Value);
        Expect("the ItemsLeftMessage should be a string that expresses 0 items left", () => SimpleTodoContent.ItemsLeftMessage.Value == "0 items left");
        Expect("the TodoItemDisplayState should be All", () => SimpleTodoContent.TodoItemDisplayState.Value == TodoItemState.All);
    }

    [Example("Adds one item")]
    void Ex02()
    {
        When("the content of the to-do is set", () => SimpleTodoContent.TodoContent.Value = TodoContent1);
        When("to add the current content of the to-do", () => SimpleTodoContent.AddCurrentTodoContent());
        Then("the items should have one item that has the given content of the to-do and is Active state", () =>
            SimpleTodoContent.TodoItems.Select(TodoItemAssertion.Of)
                .SequenceEqual(new[] { TodoItemAssertion.Of(TodoContent1, TodoItemState.Active) })
        );
        Then("the content of the to-do should be empty", () => SimpleTodoContent.TodoContent.Value = string.Empty);
        Then("the AllCompleted should be visible", () => SimpleTodoContent.CanCompleteAllTodoItems.Value);
        Then("the ItemsLeftMessage should be a string that expresses 1 item left", () => SimpleTodoContent.ItemsLeftMessage.Value = "1 item left");
    }

    [Example("Adds two items")]
    void Ex03()
    {
        When("the content of the to-do is set", () => SimpleTodoContent.TodoContent.Value = TodoContent1);
        When("to add the current content of the to-do", () => SimpleTodoContent.AddCurrentTodoContent());
        When("the content of the to-do is set again", () => SimpleTodoContent.TodoContent.Value = TodoContent2);
        When("to add the current content of the to-do", () => SimpleTodoContent.AddCurrentTodoContent());
        Then("the items should be two items that have the given content of the to-do and are Active state", () =>
            SimpleTodoContent.TodoItems.Select(TodoItemAssertion.Of)
                .SequenceEqual(new[]
                {
                    TodoItemAssertion.Of(TodoContent1, TodoItemState.Active),
                    TodoItemAssertion.Of(TodoContent2, TodoItemState.Active)
                })
        );
        Then("the content of the to-do should be empty", () => SimpleTodoContent.TodoContent.Value = string.Empty);
        Then("the AllCompleted should be visible", () => SimpleTodoContent.CanCompleteAllTodoItems.Value);
        Then("the ItemsLeftMessage should be a string that expresses 2 items left", () => SimpleTodoContent.ItemsLeftMessage.Value = "2 items left");
    }

    [Example("Removes an item")]
    void Ex04()
    {
        When("to add three to-do items", () =>
        {
            AddTodoContent(TodoContent1);
            AddTodoContent(TodoContent2);
            AddTodoContent(TodoContent3);
        });
        When("to remove the item that is added secondly", () => SimpleTodoContent.TodoItems.ElementAt(1).Remove());
        Then("the item should be removed", () =>
            SimpleTodoContent.TodoItems.Select(TodoItemAssertion.Of)
                .SequenceEqual(new[]
                {
                    TodoItemAssertion.Of(TodoContent1, TodoItemState.Active),
                    TodoItemAssertion.Of(TodoContent3, TodoItemState.Active)
                })
        );
        Then("the AllCompleted should be visible", () => SimpleTodoContent.CanCompleteAllTodoItems.Value);
        Then("the ItemsLeftMessage should be a string that expresses 2 items left", () => SimpleTodoContent.ItemsLeftMessage.Value = "2 items left");
    }

    [Example("Removes the last item")]
    void Ex05()
    {
        When("to add a to-do items", () => AddTodoContent(TodoContent1));
        When("to remove the item", () => SimpleTodoContent.TodoItems.First().Remove());
        Then("the item should be empty", () => !SimpleTodoContent.TodoItems.Any());
        Then("the AllCompleted should be invisible", () => !SimpleTodoContent.CanCompleteAllTodoItems.Value);
        Then("the ItemsLeftMessage should be a string that expresses 0 items left", () => SimpleTodoContent.ItemsLeftMessage.Value = "0 items left");
    }

    [Example("Completes an item")]
    void Ex06()
    {
        When("to add a to-do items", () => AddTodoContent(TodoContent1));
        When("to complete the item", () => SimpleTodoContent.TodoItems.First().Complete());
        Then("the ItemsLeftMessage should be a string that expresses 0 items left", () => SimpleTodoContent.ItemsLeftMessage.Value = "0 items left");
    }

    [Example("Reverts an item that is completed")]
    void Ex07()
    {
        When("to add a to-do items", () => AddTodoContent(TodoContent1));
        When("to complete the item", () => SimpleTodoContent.TodoItems.First().Complete());
        When("to revert the item", () => SimpleTodoContent.TodoItems.First().Revert());
        Then("the ItemsLeftMessage should be a string that expresses 1 item left", () => SimpleTodoContent.ItemsLeftMessage.Value = "1 item left");
    }

    [Example("Switches a filter")]
    void Ex08()
    {
        When("to add five items", () =>
        {
            AddTodoContent("Active Item 1");
            AddTodoContent("Completed Item 1");
            AddTodoContent("Active Item 2");
            AddTodoContent("Completed Item 2");
            AddTodoContent("Active Item 3");
        });
        When("to complete two items", () =>
        {
            SimpleTodoContent.TodoItems.ElementAt(1).Complete();
            SimpleTodoContent.TodoItems.ElementAt(3).Complete();
        });
        When("to select All state", () => SimpleTodoContent.TodoItemDisplayState.Value = TodoItemState.All);
        Then("the number of the to-do items should be 5", () => SimpleTodoContent.TodoItems.Count() == 5);

        When("to select Active state", () => SimpleTodoContent.TodoItemDisplayState.Value = TodoItemState.Active);
        Then("the number of the to-do items should be 3", () => SimpleTodoContent.TodoItems.Count() == 3);

        When("to select Completed state", () => SimpleTodoContent.TodoItemDisplayState.Value = TodoItemState.Completed);
        Then("the number of the to-do items should be 2", () => SimpleTodoContent.TodoItems.Count() == 2);
    }

    [Example("Items list is updated correctly when the state of the items is changed")]
    void Ex09()
    {
        Expect("the number of the to-do items should be 0", () => !SimpleTodoContent.TodoItems.Any());
        When("to add a to-do item", () => AddTodoContent(TodoContent1));

        When("to select Active state", () => SimpleTodoContent.TodoItemDisplayState.Value = TodoItemState.Active);
        Then("the number of the to-do items should be 1", () => SimpleTodoContent.TodoItems.Count() == 1);

        When("to complete the item", () => SimpleTodoContent.TodoItems.First().Complete());
        Then("the number of the to-do items should be 0", () => !SimpleTodoContent.TodoItems.Any());

        When("to select Completed state", () => SimpleTodoContent.TodoItemDisplayState.Value = TodoItemState.Completed);
        Then("the number of the to-do items should be 1", () => SimpleTodoContent.TodoItems.Count() == 1);

        When("to revert the item", () => SimpleTodoContent.TodoItems.First().Revert());
        Then("the number of the to-do items should be 0", () => !SimpleTodoContent.TodoItems.Any());
    }

    [Example("Completes and reverts all items")]
    void Ex10()
    {
        When("to add three items", () =>
        {
            AddTodoContent(TodoContent1);
            AddTodoContent(TodoContent2);
            AddTodoContent(TodoContent3);
        });

        When("the AllCompleted is set to true", () => SimpleTodoContent.AllCompleted.Value = true);
        Then("all items should be completed", () => SimpleTodoContent.TodoItems.All(item => item.State.Value == TodoItemState.Completed));
        Then("the ItemsLeftMessage should be a string that expresses 0 items left", () => SimpleTodoContent.ItemsLeftMessage.Value = "0 items left");

        When("the AllCompleted is set to false", () => SimpleTodoContent.AllCompleted.Value = false);
        Then("all items should be reverted", () => SimpleTodoContent.TodoItems.All(item => item.State.Value == TodoItemState.Active));
        Then("the ItemsLeftMessage should be a string that expresses 3 items left", () => SimpleTodoContent.ItemsLeftMessage.Value = "3 items left");
    }

    [Example("Behavior of the AllCompleted")]
    void Ex11()
    {
        When("to add three items", () =>
        {
            AddTodoContent(TodoContent1);
            AddTodoContent(TodoContent2);
            AddTodoContent(TodoContent3);
        });

        When("to complete all items", () => SimpleTodoContent.TodoItems.ToList().ForEach(item => item.Complete()));
        Then("the AllCompleted should be true", () => SimpleTodoContent.AllCompleted.Value == true);

        When("to revert a first item", () => SimpleTodoContent.TodoItems.First().Revert());
        Then("the AllCompleted should be null", () => SimpleTodoContent.AllCompleted.Value == null);

        When("to revert remained items", () =>
        {
            SimpleTodoContent.TodoItems.ElementAt(1).Revert();
            SimpleTodoContent.TodoItems.ElementAt(2).Revert();
        });
        Then("the AllCompleted should be false", () => SimpleTodoContent.AllCompleted.Value == false);

        When("to complete all items", () => SimpleTodoContent.TodoItems.ToList().ForEach(item => item.Complete()));
        Then("the AllCompleted should be true", () => SimpleTodoContent.AllCompleted.Value == true);

        When("to add a to-do item", () => AddTodoContent("Todo Item 4"));
        Then("the AllCompleted should be null", () => SimpleTodoContent.AllCompleted.Value == null);
    }
}