// Copyright (C) 2022 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
using System.Globalization;
using Avalonia.Data;
using Avalonia.Data.Converters;
using Charites.Windows.Samples.SimpleTodo.Contents;

namespace Charites.Windows.Samples.SimpleTodo.Converters;

public class TodoItemStateToBooleanConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
        => value is TodoItemState todoItemState ? todoItemState == TodoItemState.Completed : BindingOperations.DoNothing;

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
        => value is bool booleanValue ? booleanValue ? TodoItemState.Completed : TodoItemState.Active : BindingOperations.DoNothing;
}