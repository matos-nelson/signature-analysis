using Microsoft.Extensions.DependencyInjection;
using SignatureAnalysis.Infrastructure;
using System;
using System.Reflection;

namespace SignatureAnalysis.ConsoleApp.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceProvider ConfigureIoC(this IServiceCollection services)
        {
            return Container.Initialize(Assembly.GetExecutingAssembly(), services);
        }
    }
}
