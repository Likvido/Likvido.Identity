using Likvido.Identity.PrincipalProviders;
using Microsoft.Extensions.DependencyInjection;

namespace Likvido.Identity
{
    public static class DependencyInjection
    {
        public static void AddNullPrincipalProvider(this IServiceCollection services)
        {
            services.AddSingleton<IPrincipalProvider, NullPrincipalProvider>();
        }
    }
}
