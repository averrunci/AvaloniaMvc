// Copyright (C) 2020-2024 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
using Charites.Windows.Samples.SimpleLoginDemo.Presentation.Properties;

namespace Charites.Windows.Samples.SimpleLoginDemo.Presentation.Contents.User;

public class UserContent(string id)
{
    public string Id => id;

    public string Message => string.Format(Resources.UserMessageFormat, Id);
}