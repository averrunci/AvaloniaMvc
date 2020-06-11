// Copyright (C) 2020 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
using Charites.Windows.Samples.SimpleLoginDemo.Presentation.Contents.Login;

namespace Charites.Windows.Samples.SimpleLoginDemo.Adapter.User
{
    public class UserAuthentication : IUserAuthentication
    {
        protected virtual UserAuthenticationResult Authenticate(string userId, string password)
            => userId == password ? UserAuthenticationResult.Succeeded() : UserAuthenticationResult.Failed();

        UserAuthenticationResult IUserAuthentication.Authenticate(string userId, string password)
            => Authenticate(userId, password);
    }
}
