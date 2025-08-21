using System.Linq;
using System.Security.Claims;
using System.Security.Principal;
using System.Text.Json;
using Likvido.Identity.Claims;
using Likvido.Identity.Roles;

namespace Likvido.Identity.Extensions
{
    public static class PrincipalExtensions
    {
        public static bool HasAnyRole(this IPrincipal user, params string[] roles)
        {
            if (user == null || !user.Identity.IsAuthenticated)
            {
                return false;
            }

            return roles.Any(user.IsInRole);
        }

        public static string[] GetRoles(this IPrincipal user)
        {
            if (user == null || !user.Identity.IsAuthenticated)
            {
                return new string[0];
            }

            return ((ClaimsPrincipal)user).FindAll(ClaimTypes.Role).Select(c => c.Value).ToArray();
        }

        public static string GetUserId(this IPrincipal user)
        {
            if (user == null || !user.Identity.IsAuthenticated)
            {
                return null;
            }

            return ((ClaimsIdentity)user.Identity).FindFirst(ClaimTypes.NameIdentifier)?.Value;
        }

        public static string GetImpersonateOriginalUserId(this IPrincipal user)
        {
            if (user == null || !user.Identity.IsAuthenticated || !user.IsImpersonated())
            {
                return null;
            }

            return ((ClaimsIdentity)user.Identity)
                .Claims
                .FirstOrDefault(c => c.Type == LikvidoClaimType.UserImpersonationId)?
                .Value;
        }

        public static int? GetLanguageId(this IPrincipal user)
        {
            if (user == null || !user.Identity.IsAuthenticated)
            {
                return null;
            }

            var languageIdString = ((ClaimsPrincipal)user)
                .FindAll(LikvidoClaimType.LanguageId)
                .FirstOrDefault()?.Value;

            if (int.TryParse(languageIdString, out var languageId))
            {
                return languageId;
            }

            return null;
        }

        public static bool AllowAccessToAllCreditors(this IPrincipal principal) =>
            principal == null ||
            !principal.Identity.IsAuthenticated ||
            principal.IsInRole(LikvidoRoles.Internals.LikvidoEmployee);

        public static string[] GetUsableRoles(this IPrincipal user)
        {
            if (user.HasAnyRole(LikvidoRoles.LikvidoInternalRoles))
            {
                return LikvidoRoles.VisibleRoles;
            }

            if (user.IsInRole(LikvidoRoles.Creditors.Creditor))
            {
                return new[] { LikvidoRoles.Creditors.Creditor, LikvidoRoles.Creditors.CreditorEmployee };
            }

            if (user.IsInRole(LikvidoRoles.Creditors.CreditorEmployee))
            {
                return new[] { LikvidoRoles.Creditors.CreditorEmployee };
            }

            return new string[0];
        }

        public static bool IsImpersonated(this IPrincipal user)
        {
            var identity = (ClaimsIdentity)user?.Identity;
            return identity.IsImpersonated();
        }

        public static string ImpersonatedUsername(this IPrincipal user)
        {
            var claimIdentity = (ClaimsIdentity)user?.Identity;
            return claimIdentity?.Claims.FirstOrDefault(c => c.Type == LikvidoClaimType.ImpersonatedUsername)?.Value;
        }

        public static bool IsImpersonated(this ClaimsIdentity identity)
        {
            var impersonationClaim = identity?.Claims
                .FirstOrDefault(c => c.Type == LikvidoClaimType.UserImpersonation);

            if (impersonationClaim != null)
            {
                if (impersonationClaim.Value == "true")
                {
                    return true;
                }
            }

            return false;
        }

        public static CreditorClaimData GetCreditorClaimData(this IPrincipal user)
        {
            if (user == null)
            {
                return null;
            }

            var creditorClaimDataStr = ((ClaimsPrincipal)user)
                .FindAll(LikvidoClaimType.Creditor)
                .FirstOrDefault()?.Value;

            if (creditorClaimDataStr == null)
            {
                return null;
            }

            return JsonSerializer.Deserialize<CreditorClaimData>(creditorClaimDataStr);
        }

        public static bool HasCreditorRole(this IPrincipal user) =>
            user.IsInRole(LikvidoRoles.Creditors.Creditor) || user.IsInRole(LikvidoRoles.Creditors.CreditorEmployee);
    }
}
