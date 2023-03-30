// Copyright (C) 2023 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
using Avalonia.Input;
using Avalonia.Interactivity;
using Charites.Windows.Mvc.Bindings;

namespace Charites.Windows.Mvc;

[View(Key = "Charites.Windows.Mvc.Bindings.IEditableEditSelection")]
internal sealed class EditableEditSelectionController : ControllerBase<IEditableEditSelection>
{
    private void FocusTo(InputElement inputElement)
    {
        inputElement.Focus();
    }

    private void CompleteEdit(IEditableEditSelection editableEditSelection)
    {
        if (editableEditSelection.IsSelecting.Value) return;
        
        editableEditSelection.CompleteEdit();
    }

    private void CancelEdit(IEditableEditSelection editableEditSelection, KeyEventArgs e)
    {
        if (e.Key is not Key.Escape) return;
        
        editableEditSelection.CancelEdit();
    }

    private void ComboBox_Loaded(object? sender, RoutedEventArgs e) => (sender as InputElement).IfPresent(FocusTo);
    private void ComboBox_LostFocus() => DataContext.IfPresent(CompleteEdit);
    private void ComboBox_KeyDown(KeyEventArgs e) => DataContext.IfPresent(e, CancelEdit);
}