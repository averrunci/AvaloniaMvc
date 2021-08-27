// Copyright (C) 2020-2021 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
using System.Threading.Tasks;

namespace Charites.Windows.Samples.SimpleLoginDemo.Presentation.Contents.Login
{
    public interface IUserAuthentication
    {
        Task<UserAuthenticationResult> AuthenticateAsync(string userId, string password);
    }
}
