using System;

namespace Pure.NetCoreExtensions.FriendlyExceptions.Transforms.Interfaces
{
    public interface ITransformsCollection
    {
        ITransform FindTransform(Exception exception);
    }
}