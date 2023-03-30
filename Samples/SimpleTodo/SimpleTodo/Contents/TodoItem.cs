// Copyright (C) 2022-2023 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
using Charites.Windows.Mvc.Bindings;

namespace Charites.Windows.Samples.SimpleTodo.Contents;

public class TodoItem
{
    public event EventHandler? RemoveRequested;

    public EditableTextProperty Content { get; }

    public ObservableProperty<TodoItemState> State { get; } = TodoItemState.Active.ToObservableProperty();
    public BoundProperty<bool> IsCompleted { get; }

    public TodoItem(string content)
    {
        IsCompleted = BoundProperty<bool>.By(State, state => state == TodoItemState.Completed);
        
        Content = content.ToEditableTextProperty();
    }

    public void Remove()
    {
        OnRemoveRequested(EventArgs.Empty);
    }

    public void Complete()
    {
        State.Value = TodoItemState.Completed;
    }

    public void Revert()
    {
        State.Value = TodoItemState.Active;
    }

    protected virtual void OnRemoveRequested(EventArgs e) => RemoveRequested?.Invoke(this, e);
}