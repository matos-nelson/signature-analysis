using Microsoft.Extensions.DependencyInjection;
using SignatureAnalysis.Core.Interfaces.Managers;
using SignatureAnalysis.Infrastructure.Managers;
using System;
using System.Reflection;

namespace SignatureAnalysis.Infrastructure
{
    public static class Container
    {
        public static IServiceProvider Initialize(Assembly assembly, IServiceCollection services)
        {
            var registry = new InfrastructureRegistry();
            registry.Scan(_ =>
            {
                _.AssemblyContainingType(typeof(IDirectoryManager));
                _.AssemblyContainingType(typeof(DirectoryManager));
                _.Assembly(assembly);
                _.WithDefaultConventions();
            });
            registry.AddRange(services);

            var container = new Lamar.Container(registry);
            container.AssertConfigurationIsValid();

            return container.GetInstance<IServiceProvider>();
        }
    }
}
