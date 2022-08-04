// Copyright (C) 2020-2022 Fievus
//
// This software may be modified and distributed under the terms
// of the MIT license.  See the LICENSE file for details.
using Charites.Windows.Samples.SimpleLoginDemo.Adapter.Commands;
using Charites.Windows.Samples.SimpleLoginDemo.Core.Features.Users;
using Charites.Windows.Samples.SimpleLoginDemo.Presentation.Contents.Login;
using Microsoft.Extensions.DependencyInjection;

namespace Charites.Windows.Samples.SimpleLoginDemo.Adapter;

public static class ServiceExtensions
{
    public static IServiceCollection AddCommands(this IServiceCollection services)
        => services.AddTransient<ILoginCommand, LoginCommand>();

    public static IServiceCollection AddFeatures(this IServiceCollection services)
        => services.AddTransient<IUserAuthentication, UserAuthentication>();
}