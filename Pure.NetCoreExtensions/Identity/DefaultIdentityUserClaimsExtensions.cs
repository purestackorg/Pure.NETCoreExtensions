using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
namespace Pure.NetCoreExtensions
{
    public static class DefaultIdentityClaimsExtensions
    {
        public static bool AnyRoles<TUser, TKey>(this DefaultIdentityUser<TUser, TKey> self, string Roles)
            where TKey : IEquatable<TKey>
            where TUser : IdentityUser<TKey>
        {
            var roles = Roles.Split(',');
            for (var i = 0; i < roles.Count(); i++)
                roles[i] = roles[i].Trim(' ');
            foreach (var r in roles)
                if (self.IsInRole(r))
                    return true;
            return false;
        }

        public static bool AnyRolesOrClaims<TUser, TKey>(this DefaultIdentityUser<TUser, TKey> self, string Roles, List<Claim> Claims)
            where TKey : IEquatable<TKey>
            where TUser : IdentityUser<TKey>
        {
            var roles = Roles.Split(',');
            for (var i = 0; i < roles.Count(); i++)
                roles[i] = roles[i].Trim(' ');
            foreach (var r in roles)
                if (self.IsInRole(r))
                    return true;
            foreach (var c in Claims)
            {
                if (self.HasClaim(c.Type, c.Value))
                    return true;
            }
            return false;
        }

        public static bool AnyRolesOrClaims<TUser, TKey>(this DefaultIdentityUser<TUser, TKey> self, string Roles, string Types, string Value)
            where TKey : IEquatable<TKey>
            where TUser : IdentityUser<TKey>
        {
            var tmp = Types.Split(',');
            var claims = new List<Claim>();
            foreach (var c in tmp)
            {
                claims.Add(new Claim(c.Trim(' '), Value));
            }
            return self.AnyRolesOrClaims(Roles, claims);
        }
    }
}