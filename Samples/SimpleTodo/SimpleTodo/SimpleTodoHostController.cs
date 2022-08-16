// Copyright (C) 2022 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
using Charites.Windows.Mvc;

namespace Charites.Windows.Samples.SimpleTodo;

[View(Key = nameof(SimpleTodoHost))]
public class SimpleTodoHostController
{
    private void SetDataContext(SimpleTodoHost? host) => this.host = host;
    private SimpleTodoHost? host;
}