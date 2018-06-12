using System;

namespace Pure.NetCoreExtensions.RateLimit
{
    public interface ILimitingService : IDisposable
    {
        /// <summary>
        /// 申请流量处理
        /// </summary>
        /// <returns>true：获取成功，false：获取失败</returns>
        bool Request();

    }
}