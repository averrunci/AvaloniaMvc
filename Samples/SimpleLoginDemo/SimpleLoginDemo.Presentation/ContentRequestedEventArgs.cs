// Copyright (C) 2020 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
using System;

namespace Charites.Windows.Samples.SimpleLoginDemo.Presentation
{
    public class ContentRequestedEventArgs : EventArgs
    {
        public ILoginDemoContent RequestedContent { get; }

        public ContentRequestedEventArgs(ILoginDemoContent requestedContent)
        {
            RequestedContent = requestedContent;
        }
    }
}
