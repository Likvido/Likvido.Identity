using System.Security.Principal;

namespace Likvido.Identity.PrincipalProviders
{
    public interface IPrincipalProvider
    {
        IPrincipal User { get; }

        bool IsSystemProvider { get; }
    }
}
