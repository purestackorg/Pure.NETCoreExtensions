using Microsoft.AspNetCore.Http;
using System;
using System.Net;

namespace Pure.NetCoreExtensions
{
    public static class HttpRequestExtentions
    { 

        private const string RequestedWithHeader = "X-Requested-With";
        private const string XmlHttpRequest = "XMLHttpRequest";

        /// <summary>
        /// 判断是否Ajax请求
        /// </summary>
        /// <param name="request">The HTTP request.</param>
        /// <returns><c>true</c> if the specified HTTP request is an AJAX request; otherwise, <c>false</c>.</returns>
        /// <exception cref="ArgumentNullException">The <paramref name="request"/> parameter is <c>null</c>.</exception>
        public static bool IsAjaxRequest(this HttpRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            if (request.Headers != null)
            {
                return request.Headers[RequestedWithHeader] == XmlHttpRequest;
            }

            return false;
        }

        /// <summary>
        /// 是否本地请求
        /// originator was 127.0.0.1.
        /// </summary>
        /// <param name="request">The HTTP request.</param>
        /// <returns><c>true</c> if the specified HTTP request is a local request; otherwise, <c>false</c>.</returns>
        /// <exception cref="ArgumentNullException">The <paramref name="request"/> parameter is <c>null</c>.</exception>
        public static bool IsLocalRequest(this HttpRequest request)
        {
            if (request == null)
            {
                throw new ArgumentNullException(nameof(request));
            }

            var connection = request.HttpContext.Connection;
            if (connection.RemoteIpAddress != null)
            {
                string str = connection.RemoteIpAddress.ToString();
                if (str == "127.0.0.1" || str == "::1" || str == "localhost")
                {
                    return true;
                }
                if (connection.LocalIpAddress != null)
                {
                    return connection.RemoteIpAddress.Equals(connection.LocalIpAddress);
                }
                else
                {
                    return IPAddress.IsLoopback(connection.RemoteIpAddress);
                }
            }

            // for in memory TestServer or when dealing with default connection info
            if (connection.RemoteIpAddress == null && connection.LocalIpAddress == null)
            {
                return true;
            }

            return false;
        }



        public static string GetClientIpAddress(this HttpRequest request)
        {
            return request.HttpContext.Connection.RemoteIpAddress.ToString();
        }

        public static string GetConnectionId(this HttpRequest request)
        {
            return request.HttpContext.Connection.Id;
        }

        public static string GetRequestId(this HttpRequest request)
        {
            return request.Headers.FirstOrDefault("X-Request-ID");
        }

        public static string GetUserAgent(this HttpRequest request)
        {
            return request.Headers.FirstOrDefault("User-Agent");
        }

    }
}
