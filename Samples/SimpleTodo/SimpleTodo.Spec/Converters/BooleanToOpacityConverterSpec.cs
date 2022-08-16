// Copyright (C) 2022 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
using System.Globalization;
using Avalonia.Data;
using Carna;

namespace Charites.Windows.Samples.SimpleTodo.Converters;

[Specification("BooleanToOpacityConverter Spec")]
class BooleanToOpacityConverterSpec : FixtureSteppable
{
    BooleanToOpacityConverter Converter { get; } = new();

    [Example("Converts a boolean value to an opacity value")]
    [Sample(true, 1, Description = "When a value is true")]
    [Sample(false, 0, Description = "When a value is false")]
    [Sample("Test", 0, Description = "When a value is not a boolean value")]
    void Ex01(object value, object expected)
    {
        Expect($"the converted value should be {expected}", () =>
            Equals(Converter.Convert(value, typeof(double?), null, CultureInfo.CurrentCulture), expected)
        );
    }

    [Example("Converts an opacity value to a boolean value")]
    void Ex02()
    {
        Expect($"the converted value should be {BindingOperations.DoNothing}", () =>
            Equals(Converter.ConvertBack(1, typeof(bool?), null, CultureInfo.CurrentCulture), BindingOperations.DoNothing)
        );
    }
}