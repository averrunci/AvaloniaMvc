// Copyright (C) 2022 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
using System.Globalization;
using Avalonia.Data;
using Carna;
using Charites.Windows.Samples.SimpleTodo.Contents;

namespace Charites.Windows.Samples.SimpleTodo.Converters;

[Specification("TodoItemStateToOpacityConverter Spec")]
class TodoItemStateToOpacityConverterSpec : FixtureSteppable
{
    TodoItemStateToOpacityConverter Converter { get; } = new();

    [Example("Converts a TodoItemState with a parameter to an opacity value")]
    [Sample(TodoItemState.Active, null, 0.0, Description = "When a TodoItemState is 'Active' and a parameter is null")]
    [Sample(TodoItemState.Completed, null, 0.5, Description = "When a TodoItemState is 'Completed' and a parameter is null")]
    [Sample(TodoItemState.All, null, 0.0, Description = "When a TodoItemState is 'All' and a parameter is null")]
    [Sample(TodoItemState.Active, 1, 1.0, Description = "When a TodoItemState is 'Active' and a parameter is 1")]
    [Sample(TodoItemState.Completed, 1, 0.5, Description = "When a TodoItemState is 'Completed' and a parameter is 1")]
    [Sample(TodoItemState.All, 1, 1.0, Description = "When a TodoItemState is 'Description' and a parameter is 1")]
    void Ex01(object value, object parameter, object expected)
    {
        Expect($"the converted value should be {expected}", () => Equals(Converter.Convert(value, typeof(double?), parameter, CultureInfo.CurrentCulture), expected));
    }
    
    [Example("Converts an opacity value to a TodoItemState")]
    void Ex02()
    {
        Expect($"the converted value should be {BindingOperations.DoNothing}", () =>
            Equals(Converter.ConvertBack(1, typeof(TodoItemState?), null, CultureInfo.CurrentCulture), BindingOperations.DoNothing)
        );
    }
}