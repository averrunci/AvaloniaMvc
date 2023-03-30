// Copyright (C) 2022-2023 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
using Charites.Windows.Mvc;

namespace Charites.Windows.Samples.SimpleTodo.Contents;

[View(Key = nameof(TodoItem))]
public class TodoItemController
{
    private void DeleteButton_Click([FromDataContext] TodoItem todoItem)
    {
        todoItem.Remove();
    }
}