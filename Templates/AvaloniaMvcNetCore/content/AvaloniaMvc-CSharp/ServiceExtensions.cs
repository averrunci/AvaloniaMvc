using System.Reflection;
using Charites.Windows.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace AvaloniaMvcApp;

internal static class ServiceExtensions
{
    public static IServiceCollection AddControllers(this IServiceCollection services)
        => typeof(AvaloniaController).Assembly.DefinedTypes
            .Concat(typeof(AvaloniaMvcApp).Assembly.DefinedTypes)
            .Where(type => type.GetCustomAttributes<ViewAttribute>(true).Any())
            .Aggregate(services, (s, t) => s.AddTransient(t));
}
