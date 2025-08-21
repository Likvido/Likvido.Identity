using System.Security.Principal;

namespace Likvido.Identity.PrincipalProviders
{
    public class NullPrincipalProvider : IPrincipalProvider
    {
        public IPrincipal User => null;

        public bool IsSystemProvider => true;
    }
}
