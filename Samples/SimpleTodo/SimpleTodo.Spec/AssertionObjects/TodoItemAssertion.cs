﻿// Copyright (C) 2022-2024 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.

using System.Diagnostics.CodeAnalysis;
using Carna.Assertions;
using Charites.Windows.Samples.SimpleTodo.Contents;

namespace Charites.Windows.Samples.SimpleTodo.AssertionObjects;

internal class TodoItemAssertion : AssertionObject
{
    [AssertionProperty]
    public string Content { get; }

    [AssertionProperty]
    public TodoItemState State { get; }

    private TodoItemAssertion(string content, TodoItemState state)
    {
        Content = content;
        State = state;
    }

    public static TodoItemAssertion Of(string content, TodoItemState state) => new(content, state);
    [return: NotNullIfNotNull("todoItem")]
    public static TodoItemAssertion? Of(TodoItem? todoItem)
        => todoItem is null ? null : new TodoItemAssertion(todoItem.Content.Value.Value, todoItem.State.Value);
}