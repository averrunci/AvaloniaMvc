// Copyright (C) 2022 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
using System.Collections;
using System.Globalization;
using Avalonia.Data;
using Carna;
using Charites.Windows.Samples.SimpleTodo.Contents;

namespace Charites.Windows.Samples.SimpleTodo.Converters;

[Specification("TodoItemDisplayStateToBooleanConverter Spec")]
class TodoItemDisplayStateToBooleanConverterSpec : FixtureSteppable
{
      TodoItemDisplayStateToBooleanConverter Converter { get; } = new();

    [Example("Converts a display state of a TodoItem with a parameter to a boolean value")]
    [Sample(TodoItemState.All, TodoItemState.All, true, Description = "When a display state of a TodoItem is equal to a parameter")]
    [Sample(TodoItemState.All, TodoItemState.Active, Description = "When a display state of a TodoItem is not equal to a parameter")]
    void Ex01(TodoItemState value, TodoItemState parameter, bool expected)
    {
        Expect($"the converted value should be {expected}", () => Equals(Converter.Convert(value, typeof(bool?), parameter, CultureInfo.CurrentCulture), expected));
    }

    [Example("Converts a boolean value with a parameter back to a display state of a TodoItem")]
    [Sample(Source = typeof(ValidConvertingBackDataSource))]
    void Ex02(bool value, TodoItemState parameter, object expected)
    {
        Expect($"the converted value should be {expected}", () => Equals(Converter.ConvertBack(value, typeof(TodoItemState?), parameter, CultureInfo.CurrentCulture), expected));
    }

    [Example("Converts a value whose type is invalid")]
    [Sample(Source = typeof(InvalidConvertingDataSource))]
    void Ex03(object value, object parameter)
    {
        Expect($"the converted value should be {BindingOperations.DoNothing}", () =>
            Equals(Converter.Convert(value, typeof(bool?), parameter, CultureInfo.CurrentCulture), BindingOperations.DoNothing)
        );
    }

    [Example("Converts back a value whose type is invalid")]
    [Sample(Source = typeof(InvalidConvertingBackDataSource))]
    void Ex04(object value, object parameter)
    {
        Expect($"the converted value should be {BindingOperations.DoNothing}", () =>
            Equals(Converter.ConvertBack(value, typeof(TodoItemState?), parameter, CultureInfo.CurrentCulture), BindingOperations.DoNothing)
        );
    }

    class ValidConvertingBackDataSource : ISampleDataSource
    {
        IEnumerable ISampleDataSource.GetData()
        {
            yield return new
            {
                Description = "When a value is true and a parameter is 'Completed'",
                Value = true,
                Parameter = TodoItemState.Completed,
                Expected = TodoItemState.Completed
            };
            yield return new
            {
                Description = "When a value is true and a parameter is 'Active'",
                Value = true,
                Parameter = TodoItemState.Active,
                Expected = TodoItemState.Active
            };
            yield return new
            {
                Description = "When a value is true and a parameter is 'All'",
                Value = true,
                Parameter = TodoItemState.All,
                Expected = TodoItemState.All
            };
            yield return new
            {
                Description = "When a value is false and a parameter is 'Completed'",
                Value = false,
                Parameter = TodoItemState.Completed,
                Expected = BindingOperations.DoNothing
            };
            yield return new
            {
                Description = "When a value is false and a parameter is 'Active'",
                Value = false,
                Parameter = TodoItemState.Active,
                Expected = BindingOperations.DoNothing
            };
            yield return new
            {
                Description = "When a value is false and a parameter is 'All'",
                Value = false,
                Parameter = TodoItemState.All,
                Expected = BindingOperations.DoNothing
            };
        }
    }

    class InvalidConvertingDataSource : ISampleDataSource
    {
        IEnumerable ISampleDataSource.GetData()
        {
            yield return new { Description = "When a value type is invalid", Value = new object(), Parameter = TodoItemState.Active };
            yield return new { Description = "When a parameter type is invalid", Value = TodoItemState.Active, Parameter = new object() };
        }
    }

    class InvalidConvertingBackDataSource : ISampleDataSource
    {
        IEnumerable ISampleDataSource.GetData()
        {
            yield return new { Description = "When a value type is invalid", Value = new object(), Parameter = TodoItemState.Active };
            yield return new { Description = "When a parameter type is invalid", Value = true, Parameter = new object() };
        }
    }
}