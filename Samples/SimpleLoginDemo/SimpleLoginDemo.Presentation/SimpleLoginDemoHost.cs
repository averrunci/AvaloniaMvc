// Copyright (C) 2020 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
using Charites.Windows.Samples.SimpleLoginDemo.Presentation.Contents.Login;

namespace Charites.Windows.Samples.SimpleLoginDemo.Presentation
{
    public class SimpleLoginDemoHost
    {
        public SimpleLoginDemoContent Content { get; } = new SimpleLoginDemoContent(new LoginContent());
    }
}
