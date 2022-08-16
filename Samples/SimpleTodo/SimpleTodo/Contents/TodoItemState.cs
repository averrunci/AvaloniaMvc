// Copyright (C) 2022 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
namespace Charites.Windows.Samples.SimpleTodo.Contents;

[Flags]
public enum TodoItemState
{
    Active = 1,
    Completed = 2,
    All = Active | Completed
}