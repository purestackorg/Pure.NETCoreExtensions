using System;
using System.Net;

namespace Pure.NetCoreExtensions.FriendlyExceptions.Transforms.Interfaces
{
    public interface ITransform
    {
        HttpStatusCode StatusCode { get; }
        string ReasonPhrase { get; }
        string ContentType { get; }
        string GetContent(Exception ex);
        bool CanHandle<T2>(T2 ex) where T2 : Exception;
    }
}