// Copyright (C) 2022 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
using System.Globalization;
using Avalonia.Data;
using Avalonia.Data.Converters;
using Charites.Windows.Samples.SimpleTodo.Contents;

namespace Charites.Windows.Samples.SimpleTodo.Converters;

public class TodoItemDisplayStateToBooleanConverter : IValueConverter
{
    public object? Convert(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (!Enum.TryParse(parameter?.ToString(), out TodoItemState state)) return BindingOperations.DoNothing;

        return value is TodoItemState todoItemState ? todoItemState == state : BindingOperations.DoNothing;
    }

    public object? ConvertBack(object? value, Type targetType, object? parameter, CultureInfo culture)
    {
        if (!Enum.TryParse(parameter?.ToString(), out TodoItemState state)) return BindingOperations.DoNothing;

        return value is true ? state : BindingOperations.DoNothing;
    }
}