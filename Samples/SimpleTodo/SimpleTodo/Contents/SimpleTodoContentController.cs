// Copyright (C) 2022-2023 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
using Avalonia.Input;
using Charites.Windows.Mvc;

namespace Charites.Windows.Samples.SimpleTodo.Contents;

[View(Key = nameof(SimpleTodoContent))]
public class SimpleTodoContentController : ControllerBase<SimpleTodoContent>
{
    private void AddCurrentTodoContent(SimpleTodoContent content, KeyEventArgs e)
    {
        if (e.Key is not Key.Enter) return;

        content.AddCurrentTodoContent();
    }

    private void TodoContentTextBox_KeyDown(KeyEventArgs e) => DataContext.IfPresent(e, AddCurrentTodoContent);
}