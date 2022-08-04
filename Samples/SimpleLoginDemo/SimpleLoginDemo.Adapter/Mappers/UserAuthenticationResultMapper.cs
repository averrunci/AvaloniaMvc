// Copyright (C) 2022 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
using Charites.Windows.Samples.SimpleLoginDemo.Presentation.Contents.Login;

namespace Charites.Windows.Samples.SimpleLoginDemo.Adapter.Mappers;

internal static class UserAuthenticationResultMapper
{
    public static LoginAuthenticationResult ToLoginAuthenticationResult(this Core.Features.Users.UserAuthenticationResult @this)
        => @this switch
        {
            Core.Features.Users.UserAuthenticationResult.Success => LoginAuthenticationResult.Succeeded(),
            Core.Features.Users.UserAuthenticationResult.Failure => LoginAuthenticationResult.Failed(),
            _ => throw new InvalidOperationException()
        };
}