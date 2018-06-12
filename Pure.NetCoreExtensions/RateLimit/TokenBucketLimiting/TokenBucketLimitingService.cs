using System;
using System.Threading;
using System.Threading.Tasks;

namespace Pure.NetCoreExtensions.RateLimit
{
    /*
     令牌桶算法：

    令牌桶算法的基本过程如下：

假如用户配置的平均发送速率为r，则每隔1/r秒速率将一个令牌被加入到桶中；
假设桶最多可以存发b个令牌。当桶中的令牌达到上限后，丢弃令牌。
当一个有请求到达时，首先去令牌桶获取令牌，能够取到，则处理这个请求
如果桶中没有令牌，那么请求排队或者丢弃
     工作过程包括3个阶段：产生令牌、消耗令牌和判断数据包是否通过。其中涉及到2个参数：令牌产生的速率和令牌桶的大小，这个过程的具体工作如下。

产生令牌：周期性的以固定速率向令牌桶中增加令牌，桶中的令牌不断增多。如果桶中令牌数已到达上限，则丢弃多余令牌。
消费 令牌：业务程序根据具体业务情况消耗桶中的令牌。消费一次，令牌桶令牌减少一个。
判断是否通过：判断是否已有令牌桶是否存在有效令牌，当桶中的令牌数量可以满足需求时，则继续业务处理，否则将挂起业务，等待令牌。


        var service = LimitingFactory.Build(LimitingType.TokenBucket, 500, 200);

while (true)
{
      var result = service.Request();
       //如果返回true，说明可以进行业务处理，否则需要继续等待
       if (result)
       {
             //业务处理......
       }
       else
             Thread.Sleep(1);
}

         */
    public class TokenBucketLimitingService : ILimitingService
    {
        private LimitedQueue<object> limitedQueue = null;
        private CancellationTokenSource cancelToken;
        private Task task = null;
        private int maxTPS;
        private int limitSize;
        private object lckObj = new object();
        public TokenBucketLimitingService(int maxTPS, int limitSize)
        {
            this.limitSize = limitSize;
            this.maxTPS = maxTPS;

            if (this.limitSize <= 0)
                this.limitSize = 100;
            if (this.maxTPS <= 0)
                this.maxTPS = 1;

            limitedQueue = new LimitedQueue<object>(limitSize);
            for (int i = 0; i < limitSize; i++)
            {
                limitedQueue.Enqueue(new object());
            }
            cancelToken = new CancellationTokenSource();
            task = Task.Factory.StartNew(new Action(TokenProcess), cancelToken.Token);
        }

        /// <summary>
        /// 定时消息令牌
        /// </summary>
        private void TokenProcess()
        {
            int sleep = 1000 / maxTPS;
            if (sleep == 0)
                sleep = 1;

            DateTime start = DateTime.Now;
            while (cancelToken.Token.IsCancellationRequested == false)
            {
                try
                {
                    lock (lckObj)
                    {
                        limitedQueue.Enqueue(new object());
                    }
                }
                catch
                {
                }
                finally
                {
                    if (DateTime.Now - start < TimeSpan.FromMilliseconds(sleep))
                    {
                        int newSleep = sleep - (int)(DateTime.Now - start).TotalMilliseconds;
                        if (newSleep > 1)
                            Thread.Sleep(newSleep - 1); //做一下时间上的补偿
                    }
                    start = DateTime.Now;
                }
            }
        }

        public void Dispose()
        {
            cancelToken.Cancel();
        }

        /// <summary>
        /// 请求令牌
        /// </summary>
        /// <returns>true：获取成功，false：获取失败</returns>
        public bool Request()
        {
            if (limitedQueue.Count <= 0)
                return false;
            lock (lckObj)
            {
                if (limitedQueue.Count <= 0)
                    return false;

                object data = limitedQueue.Dequeue();
                if (data == null)
                    return false;
            }

            return true;
        }
    }
}