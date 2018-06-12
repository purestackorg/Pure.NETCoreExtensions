using System;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Pure.NetCoreExtensions
{
    [AttributeUsage(AttributeTargets.Parameter | AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public class InjectAttribute : Attribute, IBindingSourceMetadata
    { 
        public BindingSource BindingSource => BindingSource.Services;
    }
}