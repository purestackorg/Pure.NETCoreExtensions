using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;

namespace Pure.NetCoreExtensions
{
    public abstract class BaseController< TUser, TKey> : BaseController
     where TKey : IEquatable<TKey>
     where TUser : IdentityUser<TKey>
    {
        public UserManager<TUser> UserManager { get { return HttpContext.RequestServices?.GetService<UserManager<TUser>>(); } }

        public SignInManager<TUser> SignInManager { get { return HttpContext.RequestServices?.GetService<SignInManager<TUser>>(); } }

        public RoleManager<TUser> RoleManager { get { return HttpContext.RequestServices?.GetService<RoleManager<TUser>>(); } }

        public new DefaultIdentityUser<TUser, TKey> User { get { return HttpContext.RequestServices?.GetService<DefaultIdentityUser<TUser, TKey>>(); } }
    }
}
