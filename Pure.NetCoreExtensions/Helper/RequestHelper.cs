//using Microsoft.AspNetCore.Http;
//using System;
//using System.Text.RegularExpressions;
//using Microsoft.Extensions.DependencyInjection;
//using Pure.NetCoreExtensions;

//namespace Pure.NETCore.Ext.Helper
//{
//    public class RequestHelper
//    {
//        private static readonly Regex MOBILE_REGEX = new Regex(@"(nokia|sonyericsson|blackberry|iphone|samsung|sec\-|windows ce|motorola|mot\-|up.b|midp\-)", RegexOptions.IgnoreCase | RegexOptions.Compiled);
//        /// <summary>
//        /// 判断当前页面是否接收到了Post请求
//        /// </summary>
//        /// <returns></returns>
//        public static bool IsPost
//        {
//            get
//            {
//                return HttpContext.Current.Request.Method.Equals("POST");
//            }
//        }

//        /// <summary>
//        /// 判断当前页面是否接收到了Get请求
//        /// </summary>
//        /// <returns></returns>
//        public static bool IsGet
//        {
//            get
//            {
//                return HttpContext.Current.Request.Method.Equals("GET");
//            }
//        }

//        /// <summary>
//        /// 是否是Ajax请求
//        /// </summary>
//        /// <returns></returns>
//        public static bool IsAjax()
//        {
//            return HttpContext.Current.Request.Headers["X-Requested-With"] == "XMLHttpRequest";
//        }

//        /// <summary>
//        /// 返回指定的服务器变量信息
//        /// </summary>
//        /// <param name="strName">服务器变量名</param>
//        /// <returns>服务器变量信息</returns>
//        public static string GetServerVariables(string strName)
//        {
//            if (HttpContext.Current.Request.GetServerVariables() == null || HttpContext.Current.Request.GetServerVariables().Count == 0)
//                return "";

//            return HttpContext.Current.Request.GetServerVariables()[strName].ToString();
//        }

//        /// <summary>
//        /// 返回上一个页面的地址
//        /// </summary>
//        /// <returns>上一个页面的地址</returns>
//        public static string GetUrlReferrer()
//        {
//            string retVal = null;

//            try
//            {
//                retVal = HttpContext.Current.Request.Headers["Referer"].ToString();
//            }
//            catch { }

//            if (retVal == null)
//                return "";

//            return retVal;
//        }

//        /// <summary>
//        /// 得到当前完整主机头
//        /// </summary>
//        /// <returns></returns>
//        public static string GetCurrentFullHost()
//        {
//            var request = HttpContext.Current.Request;

//            return string.Format("{0}:{1}", request.Host.Host, request.Host.Port.ToString());

//        }

//        /// <summary>
//        /// 得到主机头
//        /// </summary>
//        /// <returns></returns>
//        public static string GetHost()
//        {
//            return HttpContext.Current.Request.Host.ToString();
//        }


//        /// <summary>
//        /// 获取当前请求的原始 URL(URL 中域信息之后的部分,包括查询字符串(如果存在))
//        /// </summary>
//        /// <returns>原始 URL</returns>
//        //public static string GetRawUrl()
//        //{
//        //    return HttpContext.Current.Request.RawUrl;
//        //}

//        /// <summary>
//        /// 判断当前访问是否来自浏览器软件
//        /// </summary>
//        /// <returns>当前访问是否来自浏览器软件</returns>
//        //public static bool IsBrowserGet()
//        //{
//        //    string[] BrowserName = { "ie", "opera", "netscape", "mozilla", "konqueror", "firefox" };
//        //    string curBrowser = HttpContext.Current.Request.Browser.Type.ToLower();
//        //    for (int i = 0; i < BrowserName.Length; i++)
//        //    {
//        //        if (curBrowser.IndexOf(BrowserName[i]) >= 0)
//        //            return true;
//        //    }
//        //    return false;
//        //}

//        /// <summary>
//        /// 判断是否来自搜索引擎链接
//        /// </summary>
//        /// <returns>是否来自搜索引擎链接</returns>
//        //public static bool IsSearchEnginesGet()
//        //{
//        //    if (HttpContext.Current.Request.UrlReferrer == null)
//        //        return false;

//        //    string[] SearchEngine = { "google", "yahoo", "msn", "baidu", "sogou", "sohu", "sina", "163", "lycos", "tom", "yisou", "iask", "soso", "gougou", "zhongsou" };
//        //    string tmpReferrer = HttpContext.Current.Request.UrlReferrer.ToString().ToLower();
//        //    for (int i = 0; i < SearchEngine.Length; i++)
//        //    {
//        //        if (tmpReferrer.IndexOf(SearchEngine[i]) >= 0)
//        //            return true;
//        //    }
//        //    return false;
//        //}

//        /// <summary>
//        /// 获得当前完整Url地址
//        /// </summary>
//        /// <returns>当前完整Url地址</returns>
//        public static string GetUrl()
//        {
//            return HttpContext.Current.Request.GetAbsoluteUri();
//        }

//        /// <summary>
//        /// 当前完整Url地址
//        /// </summary>
//        public static string CurrentUrl
//        {
//            get { return GetUrl(); }
//        }

//        /// <summary>
//        /// 获得指定Url参数的值
//        /// </summary>
//        /// <param name="strName">Url参数</param>
//        /// <returns>Url参数的值</returns>
//        public static string GetQueryString(string strName)
//        {
//            return GetQueryString(strName, false);
//        }

//        public static string GetQueryString(string strName, string defaultValue)
//        {
//            string value = GetQueryString(strName);
//            if (!string.IsNullOrWhiteSpace(value))
//                return value;
//            else
//                return defaultValue;
//        }

//        /// <summary>
//        /// 获得指定Url参数的值
//        /// </summary> 
//        /// <param name="strName">Url参数</param>
//        /// <param name="sqlSafeCheck">是否进行SQL安全检查</param>
//        /// <returns>Url参数的值</returns>
//        public static string GetQueryString(string strName, bool sqlSafeCheck)
//        {
//            try
//            {
//                if (HttpContext.Current.Request.Query == null || HttpContext.Current.Request.Query.Count == 0)
//                    return "";

//                if (sqlSafeCheck && !Validate.IsSafeSqlString(HttpContext.Current.Request.Query[strName]))
//                    return "unsafe string";

//                return HttpContext.Current.Request.Query[strName];
//            }
//            catch (Exception)
//            {
//                return "";
//            }

//        }

//        /// <summary>
//        /// 获得当前页面的名称
//        /// </summary>
//        /// <returns>当前页面的名称</returns>
//        public static string GetPageName()
//        {
//            string[] urlArr = HttpContext.Current.Request.GetAbsoluteUri().Split('/');
//            return urlArr[urlArr.Length - 1].ToLower();
//        }

//        /// <summary>
//        /// 返回表单或Url参数的总个数
//        /// </summary>
//        /// <returns></returns>
//        public static int GetParamCount()
//        {
//            return HttpContext.Current.Request.Form.Count + HttpContext.Current.Request.Query.Count;
//        }


//        /// <summary>
//        /// 获得指定表单参数的值
//        /// </summary>
//        /// <param name="strName">表单参数</param>
//        /// <returns>表单参数的值</returns>
//        public static string GetFormString(string strName)
//        {
//            return GetFormString(strName, false);
//        }

//        /// <summary>
//        /// 获得指定表单参数的值
//        /// </summary>
//        /// <param name="strName">表单参数</param>
//        /// <param name="sqlSafeCheck">是否进行SQL安全检查</param>
//        /// <returns>表单参数的值</returns>
//        public static string GetFormString(string strName, bool sqlSafeCheck)
//        {
//            try
//            {
//                if (HttpContext.Current.Request.Form == null || HttpContext.Current.Request.Form.Count == 0)
//                    return "";

//                if (sqlSafeCheck && !Validate.IsSafeSqlString(HttpContext.Current.Request.Form[strName]))
//                    return "unsafe string";

//                return HttpContext.Current.Request.Form[strName];
//            }
//            catch (Exception)
//            {
//                return "";
//            }

//        }

//        /// <summary>
//        /// 获得Url或表单参数的值, 先判断Url参数是否为空字符串, 如为True则返回表单参数的值
//        /// </summary>
//        /// <param name="strName">参数</param>
//        /// <returns>Url或表单参数的值</returns>
//        public static string GetString(string strName)
//        {
//            return GetString(strName, false);
//        }

//        /// <summary>
//        /// 获得Url或表单参数的值, 先判断Url参数是否为空字符串, 如为True则返回表单参数的值
//        /// </summary>
//        /// <param name="strName">参数</param>
//        /// <param name="sqlSafeCheck">是否进行SQL安全检查</param>
//        /// <returns>Url或表单参数的值</returns>
//        public static string GetString(string strName, bool sqlSafeCheck)
//        {
//            if ("".Equals(GetQueryString(strName)))
//                return GetFormString(strName, sqlSafeCheck);
//            else
//                return GetQueryString(strName, sqlSafeCheck);
//        }

//        /// <summary>
//        /// 获得指定Url参数的int类型值
//        /// </summary>
//        /// <param name="strName">Url参数</param>
//        /// <returns>Url参数的int类型值</returns>
//        public static int GetQueryInt(string strName)
//        {
//            return ConverterHelper.StrToInt(HttpContext.Current.Request.Query[strName], 0);
//        }


//        /// <summary>
//        /// 获得指定Url参数的int类型值
//        /// </summary>
//        /// <param name="strName">Url参数</param>
//        /// <param name="defValue">缺省值</param>
//        /// <returns>Url参数的int类型值</returns>
//        public static int GetQueryInt(string strName, int defValue)
//        {
//            return ConverterHelper.StrToInt(HttpContext.Current.Request.Query[strName], defValue);
//        }


//        /// <summary>
//        /// 获得指定表单参数的int类型值
//        /// </summary>
//        /// <param name="strName">表单参数</param>
//        /// <param name="defValue">缺省值</param>
//        /// <returns>表单参数的int类型值</returns>
//        public static int GetFormInt(string strName, int defValue)
//        {
//            return ConverterHelper.StrToInt(HttpContext.Current.Request.Form[strName], defValue);
//        }

//        /// <summary>
//        /// 获得指定Url或表单参数的int类型值, 先判断Url参数是否为缺省值, 如为True则返回表单参数的值
//        /// </summary>
//        /// <param name="strName">Url或表单参数</param>
//        /// <param name="defValue">缺省值</param>
//        /// <returns>Url或表单参数的int类型值</returns>
//        public static int GetInt(string strName, int defValue)
//        {
//            if (GetQueryInt(strName, defValue) == defValue)
//                return GetFormInt(strName, defValue);
//            else
//                return GetQueryInt(strName, defValue);
//        }

//        /// <summary>
//        /// 获得指定Url参数的float类型值
//        /// </summary>
//        /// <param name="strName">Url参数</param>
//        /// <param name="defValue">缺省值</param>
//        /// <returns>Url参数的int类型值</returns>
//        public static float GetQueryFloat(string strName, float defValue)
//        {
//            return ConverterHelper.StrToFloat(HttpContext.Current.Request.Query[strName], defValue);
//        }


//        /// <summary>
//        /// 获得指定表单参数的float类型值
//        /// </summary>
//        /// <param name="strName">表单参数</param>
//        /// <param name="defValue">缺省值</param>
//        /// <returns>表单参数的float类型值</returns>
//        public static float GetFormFloat(string strName, float defValue)
//        {
//            return ConverterHelper.StrToFloat(HttpContext.Current.Request.Form[strName], defValue);
//        }

//        /// <summary>
//        /// 获得指定Url或表单参数的float类型值, 先判断Url参数是否为缺省值, 如为True则返回表单参数的值
//        /// </summary>
//        /// <param name="strName">Url或表单参数</param>
//        /// <param name="defValue">缺省值</param>
//        /// <returns>Url或表单参数的int类型值</returns>
//        public static float GetFloat(string strName, float defValue)
//        {
//            if (GetQueryFloat(strName, defValue) == defValue)
//                return GetFormFloat(strName, defValue);
//            else
//                return GetQueryFloat(strName, defValue);
//        }

//        /// <summary>
//        /// 获得当前页面客户端的IP
//        /// </summary>
//        /// <returns>当前页面客户端的IP</returns>
//        public static string GetIP()
//        {
//            string result = "";
//            if (HttpContext.Current != null)
//            {
//                result = HttpContext.Current.GetUserIp();
//            }
//            if (string.IsNullOrEmpty(result) || !Validate.IsIP(result))
//                return "127.0.0.1";

//            return result;
//        }
//        /// <summary>
//        /// 获取客户端信息
//        /// </summary>
//        /// <returns></returns>
//        public static string GetUserAgent()
//        {
//            string agent = "";
//            if (HttpContext.Current != null)
//            {
//                agent = HttpContext.Current.Request.GetServerVariables()["HTTP_USER_AGENT"];
//            }
//            return agent;

//        }

//        /// <summary>
//        /// 保存用户上传的文件
//        /// </summary>
//        /// <param name="path">保存路径</param>
//        public static IFormFileCollection GetRequestFile()
//        {
//            try
//            {
//                if (HttpContext.Current.Request.Form != null)
//                {
//                    return HttpContext.Current.Request.Form.Files;
//                }

//                return null;
//            }
//            catch (Exception)
//            {
//                return null;
//            }

//        }
//        /// <summary>
//        /// 写cookie值
//        /// </summary>
//        /// <param name="strName">名称</param>
//        /// <param name="strValue">值</param>
//        public static void WriteCookie(string strName, string strValue)
//        {
//            HttpContext.Current.Response.Cookies.Append(strName, strValue);
//        }
//        /// <summary>
//        /// 写cookie值
//        /// </summary>
//        /// <param name="strName">名称</param>
//        /// <param name="strValue">值</param>
//        public static void WriteCookie(string strName, string strValue, CookieOptions option)
//        {
//            HttpContext.Current.Response.Cookies.Append(strName, strValue, option);
//        }

//        /// <summary>
//        /// 写cookie值
//        /// </summary>
//        /// <param name="strName">名称</param>
//        /// <param name="strValue">值</param>
//        /// <param name="strValue">过期时间(分钟)</param>
//        public static void WriteCookie(string strName, string strValue, int expires)
//        {
//            HttpContext.Current.Response.Cookies.Append(strName, strValue, new CookieOptions() { Expires = new DateTimeOffset(DateTime.Now, new TimeSpan(0, expires, 0)) });

//        }

//        /// <summary>
//        /// 读cookie值
//        /// </summary>
//        /// <param name="strName">名称</param>
//        /// <returns>cookie值</returns>
//        public static string GetCookie(string strName)
//        {
//            if (HttpContext.Current.Request.Cookies != null && HttpContext.Current.Request.Cookies[strName] != null)
//                return HttpContext.Current.Request.Cookies[strName].ToString();

//            return "";
//        }


//        public static void DeleteCookie(string strName)
//        {
//            HttpContext.Current.Response.Cookies.Delete(strName);

//        }

//        /// <summary>
//        /// 是否是手机浏览器
//        /// </summary>
//        //public static bool IsMobile
//        //{
//        //    get
//        //    {

//        //        var context = HttpContext.Current;
//        //        if (context != null)
//        //        {
//        //            HttpRequest request = context.Request;


//        //            string useragent = request.UserAgent;


//        //            string accept = request.ServerVariables["HTTP_ACCEPT"];

//        //            if (!string.IsNullOrEmpty(accept) && accept.IndexOf("wap") > 0)
//        //            {
//        //                return true;
//        //            }

//        //        }

//        //        return false;
//        //    }
//        //}

//        /// <summary>
//        /// 获取站点根目录URL
//        /// </summary>
//        /// <returns></returns>
//        public static string GetRootUrl(string sitePath)
//        {
//            var port = HttpContext.Current.Request.Host.Port;
//            return string.Format("{0}://{1}{2}{3}",
//                                 HttpContext.Current.Request.Scheme,
//                                 HttpContext.Current.Request.Host.ToString(),
//                                 (port == 80 || port == 0 || port == 82) ? "" : ":" + port,
//                                 sitePath);
//        }
//        /// <summary>
//        /// 得到站点的真实路径
//        /// </summary>
//        /// <returns></returns>
//        public static string GetTrueSitePath()
//        {
//            string sitePath = HttpContext.Current.Request.Path;
//            if (sitePath.LastIndexOf("/") != sitePath.IndexOf("/"))
//                sitePath = sitePath.Substring(sitePath.IndexOf("/"), sitePath.LastIndexOf("/") + 1);
//            else
//                sitePath = "/";

//            return sitePath;
//        }
//        /// <summary>
//        /// 得到主机头
//        /// </summary>
//        /// <returns></returns>
//        public static string Host
//        {
//            get
//            {
//                return HttpContext.Current.Request.Host.Host;
//            }
//        }

//        /// <summary>
//        /// 返回当前页面是否是跨站提交
//        /// </summary>
//        /// <returns></returns>
//        public static bool IsCrossSitePost
//        {
//            get
//            {
//                if (IsPost)
//                {
//                    if (GetUrlReferrer().Length < 7)
//                    {
//                        return true;
//                    }
//                    Uri u = new Uri(GetUrlReferrer());
//                    return u.Host != Host;
//                }
//                return false;
//            }
//        }


//        public static string GetRequestValue(string key)
//        {
//            var v = GetQueryString(key);
//            if (v == null || v == "")
//            {
//                v = GetFormString(key);
//            }

//            return v;
//        }

//    }
//}
