using Microsoft.AspNetCore.Http;
using System.Collections.Generic;
using System.Collections.Specialized;
namespace Pure.NetCoreExtensions
{
    public class HttpCookieCollection : NameObjectCollectionBase, IResponseCookies
    {
        Dictionary<string, HttpCookieWrapper> dic = new Dictionary<string, HttpCookieWrapper>();

        Microsoft.AspNetCore.Http.HttpContext context;

        public HttpCookieCollection(Microsoft.AspNetCore.Http.HttpContext context)
        {
            this.context = context;
        }

        // public HttpCookie this[int index] { get { return dic[index]; } }

        public HttpCookieWrapper this[string name] { get { return dic[name]; } }

        public void Add(HttpCookieWrapper cookie)
        {
            Add(cookie, true);
        }
        public void Add(HttpCookieWrapper cookie, bool appendToReponse)
        {
            if (!appendToReponse)
            {
                dic.Add(cookie.Name, cookie);
                return;
            }
            if (context != null && cookie != null)
            {
                Append(cookie.Name, cookie.Value, cookie.ToCookieOptions());
            }
        }

        public void Append(string key, string value)
        {
            Append(key, value, new CookieOptions());
        }

        public void Append(string key, string value, CookieOptions options)
        {
            // context.Response.Headers["Set-Cookie"]
            //context.Response.Cookies.Delete(key);
            context.Response.Cookies.Append(key, value, options);
            HttpCookieWrapper cookie = options;
            cookie.Name = key;
            cookie.Value = value;
            dic.Add(cookie.Name, cookie);
        }

        public void Delete(string key)
        {
            Delete(key, null);
        }

        public void Delete(string key, CookieOptions options)
        {
            context.Response.Cookies.Delete(key, options);
            dic.Remove(key);
        }
    }

}