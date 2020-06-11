// Copyright (C) 2020 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
namespace Charites.Windows.Samples.SimpleLoginDemo.Presentation.Contents.Login
{
    public class UserAuthenticationResult
    {
        public bool Success { get; }

        protected UserAuthenticationResult(bool success)
        {
            Success = success;
        }

        public static UserAuthenticationResult Succeeded() => new UserAuthenticationResult(true);
        public static UserAuthenticationResult Failed() => new UserAuthenticationResult(false);
    }
}
