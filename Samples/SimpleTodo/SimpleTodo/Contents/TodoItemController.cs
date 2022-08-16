// Copyright (C) 2022 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
using Avalonia.Controls;
using Avalonia.Input;
using Charites.Windows.Mvc;

namespace Charites.Windows.Samples.SimpleTodo.Contents;

[View(Key = nameof(TodoItem))]
public class TodoItemController
{
    private void TodoItemContainer_DoubleTapped([FromDataContext] TodoItem todoItem, [FromElement(Name = "TodoContentTextBox")] TextBox todoContentTextBox)
    {
        todoItem.StartEdit();

        todoContentTextBox.Focus();
        todoContentTextBox.SelectAll();
    }

    private void TodoContentTextBox_KeyDown(KeyEventArgs e, [FromDataContext] TodoItem todoItem)
    {
        switch (e.Key)
        {
            case Key.Enter:
                todoItem.CompleteEdit();
                break;
            case Key.Escape:
                todoItem.CancelEdit();
                break;
        }
    }

    private void TodoContentTextBox_LostFocus([FromDataContext] TodoItem todoItem)
    {
        if (!todoItem.IsEditing.Value) return;

        todoItem.CompleteEdit();
    }

    private void DeleteButton_Click([FromDataContext] TodoItem todoItem)
    {
        todoItem.Remove();
    }
}