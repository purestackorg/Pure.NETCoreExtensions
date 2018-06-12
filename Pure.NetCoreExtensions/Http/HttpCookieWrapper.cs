using Microsoft.AspNetCore.Http;
using System;
namespace Pure.NetCoreExtensions
{
    public class HttpCookieWrapper
    {
        public HttpCookieWrapper() { }
        public HttpCookieWrapper(string name)
        {
            this.Name = name;
        }
        public HttpCookieWrapper(string name, string value)
        {
            this.Name = name;
            this.Value = value;
        }

        public string Name { get; set; }
        public DateTime Expires { get; set; }
        public string Path { get; set; }
        public bool HttpOnly { get; set; }
        public string Value { get; set; }
        public string Domain { get; set; }
        /// <summary>
        /// 转换后，还得补上name,value
        /// </summary>
        /// <param name="op"></param>
        public static implicit operator HttpCookieWrapper(CookieOptions op)
        {
            HttpCookieWrapper cookie = new HttpCookieWrapper();
            cookie.Domain = op.Domain;
            if (op.Expires.HasValue)
            {
                cookie.Expires = ConvertFromDateTimeOffset(op.Expires.Value);
            }
            cookie.HttpOnly = op.HttpOnly;
            cookie.Path = op.Path;
            return cookie;
        }
        public CookieOptions ToCookieOptions()
        {
            CookieOptions op = new CookieOptions();
            op.Domain = this.Domain;
            op.Expires = ConverFromDateTime(this.Expires);
            op.HttpOnly = this.HttpOnly;
            op.Path = string.IsNullOrEmpty(this.Path) ? "/" : this.Path;
            return op;
        }
        static DateTime ConvertFromDateTimeOffset(DateTimeOffset dateTime)
        {
            if (dateTime.Offset.Equals(TimeSpan.Zero))
                return dateTime.UtcDateTime;
            else if (dateTime.Offset.Equals(TimeZoneInfo.Local.GetUtcOffset(dateTime.DateTime)))
                return DateTime.SpecifyKind(dateTime.DateTime, DateTimeKind.Local);
            else
                return dateTime.DateTime;
        }
        static DateTimeOffset ConverFromDateTime(DateTime dateTime)
        {
            return dateTime.ToUniversalTime() <= DateTimeOffset.MinValue.UtcDateTime
                   ? DateTimeOffset.MinValue
                   : new DateTimeOffset(dateTime);

            // return new DateTimeOffset(dateTime, TimeZoneInfo.Local.GetUtcOffset(dateTime));
        }
    }
}