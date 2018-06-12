namespace Pure.NetCoreExtensions.RateLimit
{
    public class LimitingFactory
    {
        /// <summary>
        /// 创建限流服务对象
        /// </summary>
        /// <param name="limitingType">限流模型</param>
        /// <param name="maxQPS">最大QPS</param>
        /// <param name="limitSize">最大可用票据数</param>
        public static ILimitingService Build(LimitingType limitingType = LimitingType.TokenBucket, int maxQPS = 100, int limitSize = 100)
        {
            switch (limitingType)
            {
                case LimitingType.TokenBucket:
                default:
                    return new TokenBucketLimitingService(maxQPS, limitSize);
                case LimitingType.LeakageBucket:
                    return new LeakageBucketLimitingService(maxQPS, limitSize);
            }
        }
    }



    /// <summary>
    /// 限流模式
    /// </summary>
    public enum LimitingType
    {
        TokenBucket,//令牌桶模式
        LeakageBucket//漏桶模式
    }



    public class LimitedQueue<T> : System.Collections.Generic.Queue<T>
    {
        private int limit = 0;
        public const string QueueFulled = "TTP-StreamLimiting-1001";

        public int Limit
        {
            get { return limit; }
            set { limit = value; }
        }

        public LimitedQueue()
             : this(0)
        { }

        public LimitedQueue(int limit)
             : base(limit)
        {
            this.Limit = limit;
        }

        public new bool Enqueue(T item)
        {
            if (limit > 0 && this.Count >= this.Limit)
            {
                return false;
            }
            base.Enqueue(item);
            return true;
        }
    }
}