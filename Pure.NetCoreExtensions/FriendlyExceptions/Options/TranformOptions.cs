using Pure.NetCoreExtensions.FriendlyExceptions.Transforms.Interfaces;

namespace Pure.NetCoreExtensions.FriendlyExceptions.Options
{
    public class TranformOptions
    {
        public virtual ITransformsCollection Transforms { get; set; }
    }
}