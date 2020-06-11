using System.Linq;
using System.Reflection;
using Charites.Windows.Mvc;
using Microsoft.Extensions.DependencyInjection;

namespace $safeprojectname$
{
    public static class $safeitemrootname$
    {
        public static IServiceCollection AddControllers(this IServiceCollection services)
            => typeof(AvaloniaController).Assembly.DefinedTypes
                .Concat(typeof($safeitemrootname$).Assembly.DefinedTypes)
                .Where(type => type.GetCustomAttributes<ViewAttribute>(true).Any())
                .Aggregate(services, (s, t) => s.AddTransient(t));
    }
}
