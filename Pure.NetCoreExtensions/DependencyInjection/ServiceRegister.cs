using Microsoft.Extensions.DependencyInjection;
using System.Reflection;

namespace Pure.NetCoreExtensions
{
    public sealed class ServiceRegister
    {
        private static readonly TypeInfo TransientType = typeof(ITransient).GetTypeInfo();

        private static readonly TypeInfo ScopedType = typeof(IScoped).GetTypeInfo();

        private static readonly TypeInfo SingletonType = typeof(ISingleton).GetTypeInfo();

        public void RegisterAssembly(IServiceCollection services, Assembly assembly)
        {
            foreach (TypeInfo type in assembly.DefinedTypes)
            {
                RegisterType(services, type);
            }
        }

        public void RegisterType(IServiceCollection services, TypeInfo type)
        {
            //Do not regist interfaces
            if (type.IsInterface)
            {
                return;
            }

            //Do not regist abstract class
            if (type.IsAbstract)
            {
                return;
            }

            if (TransientType.IsAssignableFrom(type))
            {
                services.AddTransient(type.AsType());
            }
            else if (ScopedType.IsAssignableFrom(type))
            {
                services.AddScoped(type.AsType());
            }
            else if (SingletonType.IsAssignableFrom(type))
            {
                services.AddSingleton(type.AsType());
            }
        }
    }
}
