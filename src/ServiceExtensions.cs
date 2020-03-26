using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using MeiYiJia.Abp.Workflow.Interface;
using MeiYiJia.Abp.Workflow.Service;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace MeiYiJia.Abp.Workflow
{
    public static class ServiceExtensions
    {
        public static IServiceCollection AddWorkFlow(
            this IServiceCollection services,
            IConfiguration configuration)
        {
            Assembly entryAssembly = Assembly.GetEntryAssembly();

            var types = Assembly.GetEntryAssembly()?.GetTypes()
                .Where(m => m.IsAssignableFrom(typeof(IStepBodyAsync)) && !m.IsAbstract && m.IsClass)
                .ToList() ?? new List<Type>();

            foreach (Type implementationType in types)
            {
                services.AddTransient(typeof (IStepBodyAsync), implementationType);
            }
            services.AddSingleton<IPersistenceProvider, InMemoryPersistenceProvider>();
            services.AddSingleton<IWorkflowRegistry, InMemoryWorkflowRegistry>();
            services.AddSingleton<ITokenBucket, InMemoryTokenBucket>();
            services.AddSingleton<IWorkflowController, WorkflowController>();
            services.AddSingleton<IWorkflowHost, WorkflowHost>();
            return services;
        }
    }
}