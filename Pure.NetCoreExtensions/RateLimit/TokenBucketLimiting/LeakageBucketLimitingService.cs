using System;
using System.Threading;
using System.Threading.Tasks;

namespace Pure.NetCoreExtensions.RateLimit
{
    /*
     漏桶算法

      声明一个固定容量的桶，每接受到一个请求向桶中添加一个令牌，当令牌桶达到上线后请求丢弃或等待，具体算法如下：

创建一个固定容量的漏桶，请求到达时向漏桶添加一个令牌
如果请求添加令牌不成功，请求丢弃或等待
另一个线程以固定的速率消费桶里的令牌
     工作过程也包括3个阶段：产生令牌、消耗令牌和判断数据包是否通过。其中涉及到2个参数：令牌自动消费的速率和令牌桶的大小，个过程的具体工作如下。

产生令牌：业务程序根据具体业务情况申请令牌。申请一次，令牌桶令牌加一。如果桶中令牌数已到达上限，则挂起业务后等待令牌。
消费令牌：周期性的以固定速率消费令牌桶中令牌，桶中的令牌不断较少。
判断是否通过：判断是否已有令牌桶是否存在有效令牌，当桶中的令牌数量可以满足需求时，则继续业务处理，否则将挂起业务，等待令牌。

        var service = LimitingFactory.Build(LimitingType.LeakageBucket, 500, 200);

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
    public class LeakageBucketLimitingService : ILimitingService
    {
        private LimitedQueue<object> limitedQueue = null;
        private CancellationTokenSource cancelToken;
        private Task task = null;
        private int maxTPS;
        private int limitSize;
        private object lckObj = new object();
        public LeakageBucketLimitingService(int maxTPS, int limitSize)
        {
            this.limitSize = limitSize;
            this.maxTPS = maxTPS;

            if (this.limitSize <= 0)
                this.limitSize = 100;
            if (this.maxTPS <= 0)
                this.maxTPS = 1;

            limitedQueue = new LimitedQueue<object>(limitSize);
            cancelToken = new CancellationTokenSource();
            task = Task.Factory.StartNew(new Action(TokenProcess), cancelToken.Token);
        }

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

                    if (limitedQueue.Count > 0)
                    {
                        lock (lckObj)
                        {
                            if (limitedQueue.Count > 0)
                                limitedQueue.Dequeue();
                        }
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

        public bool Request()
        {
            if (limitedQueue.Count >= limitSize)
                return false;
            lock (lckObj)
            {
                if (limitedQueue.Count >= limitSize)
                    return false;

                return limitedQueue.Enqueue(new object());
            }
        }
    }
}