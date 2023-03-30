// Copyright (C) 2023 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
using Avalonia.Input;
using Charites.Windows.Mvc.Bindings;

namespace Charites.Windows.Mvc;

[View(Key = "Charites.Windows.Mvc.Bindings.IEditableDisplayContent")]
internal sealed class EditableDisplayContentController : ControllerBase<IEditableDisplayContent>
{
    private void StartEdit(IEditableDisplayContent editableDisplayContent)
    {
        editableDisplayContent.StartEdit();
    }
    
    [EventHandler(Event = nameof(InputElement.Tapped))]
    private void EditableDisplayContent_Tapped() => DataContext.IfPresent(StartEdit);
}