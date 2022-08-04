// Copyright (C) 2022 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
namespace Charites.Windows.Samples.SimpleLoginDemo.Presentation.Contents.Login;

public class LoginAuthenticationResult
{
    public bool Success { get; }

    private LoginAuthenticationResult(bool success)
    {
        Success = success;
    }

    public static LoginAuthenticationResult Succeeded() => new(true);
    public static LoginAuthenticationResult Failed() => new(false);
}