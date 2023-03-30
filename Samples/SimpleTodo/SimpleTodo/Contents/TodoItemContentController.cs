// Copyright (C) 2023 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
using Avalonia.Input;
using Charites.Windows.Mvc;
using Charites.Windows.Mvc.Bindings;

namespace Charites.Windows.Samples.SimpleTodo.Contents;

[View(Key = "TodoItemContent")]
public class TodoItemContentController : ControllerBase<IEditableDisplayContent>
{
    [EventHandler(Event = nameof(InputElement.DoubleTapped))]
    private void TodoItemContent_DoubleTapped() => DataContext.IfPresent(x => x.StartEdit());
}