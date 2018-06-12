using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Security.Claims;
namespace Pure.NetCoreExtensions
{
    public class DefaultIdentityUser<TUser, TKey> : ClaimsPrincipal
    where TKey : IEquatable<TKey>
    where TUser : IdentityUser<TKey>
    {
        public Microsoft.AspNetCore.Http.HttpContext HttpContext { get; }

        public UserManager<TUser> Manager { get; }

        private TUser _current = null;

        public bool IsSignedIn()
        {
            return HttpContext.User.Identity.IsAuthenticated;
        }

        public new TUser Current
        {
            get
            {
                if (_current == null)
                {
                    if (!HttpContext.User.Identity.IsAuthenticated) return null;
                    var um = HttpContext.RequestServices.GetRequiredService<UserManager<TUser>>();
                    var Type = typeof(TUser);
                    var tmp = um.GetUserId(HttpContext.User);
                    TKey uid;
                    if (typeof(TKey) == typeof(Guid))
                    {
                        uid = (dynamic)Guid.Parse(tmp);
                    }
                    else
                    {
                        uid = (TKey)Convert.ChangeType(tmp, typeof(TKey));
                    }
                    try
                    {
                        _current = um.Users.Where(x => x.Id.Equals(uid)).Single();
                        return _current;
                    }
                    catch
                    {
                        return null;
                    }
                }
                else
                {
                    return _current;
                }
            }
        }

        public DefaultIdentityUser(IHttpContextAccessor accessor, UserManager<TUser> userManager)
        {
            HttpContext = accessor.HttpContext;
            Manager = userManager;
            this.AddIdentity(new ClaimsIdentity(HttpContext.User.Identity));
            this.AddIdentities(HttpContext.User.Identities);
        }
    }

}