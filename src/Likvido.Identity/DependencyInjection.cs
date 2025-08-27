using Likvido.Identity.PrincipalProviders;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;

namespace Likvido.Identity
{
    public static class DependencyInjection
    {
        public static void UseNullPrincipalProvider(this IServiceCollection services)
        {
            services.AddSingleton<IPrincipalProvider, NullPrincipalProvider>();
        }

        public static void TryAddNullPrincipalProvider(this IServiceCollection services)
        {
            services.TryAddSingleton<IPrincipalProvider, NullPrincipalProvider>();
        }
    }
}
