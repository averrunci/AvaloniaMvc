// Copyright (C) 2022-2024 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
using Charites.Windows.Samples.SimpleLoginDemo.Adapter.Mappers;
using Charites.Windows.Samples.SimpleLoginDemo.Core.Features.Users;
using Charites.Windows.Samples.SimpleLoginDemo.Presentation.Contents.Login;

namespace Charites.Windows.Samples.SimpleLoginDemo.Adapter.Commands;

public class LoginCommand(IUserAuthentication userAuthentication) : ILoginCommand
{
    protected IUserAuthentication UserAuthentication { get; } = userAuthentication;

    protected virtual LoginAuthenticationResult Authenticate(LoginContent loginContent)
        => UserAuthentication.Authenticate(loginContent.ToUser()).ToLoginAuthenticationResult();

    Task<LoginAuthenticationResult> ILoginCommand.AuthenticateAsync(LoginContent loginContent)
        => Task.Run(() => Authenticate(loginContent));
}