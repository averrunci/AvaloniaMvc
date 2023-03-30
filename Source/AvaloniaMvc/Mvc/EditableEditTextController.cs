// Copyright (C) 2023 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
using Avalonia.Controls;
using Avalonia.Input;
using Avalonia.Interactivity;
using Charites.Windows.Mvc.Bindings;

namespace Charites.Windows.Mvc;

[View(Key = "Charites.Windows.Mvc.Bindings.IEditableEditText")]
internal sealed class EditableEditTextController : ControllerBase<IEditableEditText>
{
    private void FocusTo(TextBox textBox)
    {
        textBox.SelectAll();
        textBox.Focus();
    }

    private void CompleteEdit(IEditableEditText editableEditText)
    {
        if (editableEditText.Validate())
        {
            editableEditText.CompleteEdit();
        }
        else
        {
            editableEditText.CancelEdit();
        }
    }

    private void CompleteEdit(IEditableEditText editableEditText, KeyEventArgs e)
    {
        switch (e.Key)
        {
            case Key.Escape:
                editableEditText.CancelEdit();
                break;
            case Key.Enter when !editableEditText.IsMultiLine.Value && editableEditText.Validate():
                editableEditText.CompleteEdit();
                break;
        }
    }
    
    private void TextBox_Loaded(object? sender, RoutedEventArgs e) => (sender as TextBox).IfPresent(FocusTo);
    private void TextBox_LostFocus() => DataContext.IfPresent(CompleteEdit);
    private void TextBox_KeyDown(KeyEventArgs e) => DataContext.IfPresent(e, CompleteEdit);
}