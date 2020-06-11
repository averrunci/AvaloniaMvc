// Copyright (C) 2020 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
namespace Charites.Windows.Samples.SimpleLoginDemo.Presentation.Contents.Login
{
    public interface IUserAuthentication
    {
        UserAuthenticationResult Authenticate(string userId, string password);
    }
}
