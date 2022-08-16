// Copyright (C) 2022 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
using Avalonia.Controls;
using Avalonia.Input;
using Charites.Windows.Mvc;
using Charites.Windows.Mvc.Bindings;

namespace Charites.Windows.Samples.SimpleTodo.Contents;

[View(Key = nameof(SimpleTodoContent))]
public class SimpleTodoContentController
{
    private void SetDataContext(SimpleTodoContent? content)
    {
        if (this.content is not null) UnsubscribeEventHandler(this.content);
        this.content = content;
        if (this.content is not null) SubscribeEventHandler(this.content);
    }
    private SimpleTodoContent? content;

    [Element]
    private void SetAllCompletedCheckBox(CheckBox? allCompletedCheckBox) => this.allCompletedCheckBox = allCompletedCheckBox;
    private CheckBox? allCompletedCheckBox;

    private void SubscribeEventHandler(SimpleTodoContent content)
    {
        content.AllCompleted.PropertyValueChanged += OnAllCompletedChanged;
    }

    private void UnsubscribeEventHandler(SimpleTodoContent content)
    {
        content.AllCompleted.PropertyValueChanged -= OnAllCompletedChanged;
    }

    private void OnAllCompletedChanged(object? sender, PropertyValueChangedEventArgs<bool?> e)
    {
        if (e.NewValue.HasValue) return;
        if (allCompletedCheckBox is null) return;

        allCompletedCheckBox.IsChecked = null;
    }

    private void TodoContentTextBox_KeyDown(KeyEventArgs e)
    {
        if (e.Key is not Key.Enter) return;

        content?.AddCurrentTodoContent();
    }
}