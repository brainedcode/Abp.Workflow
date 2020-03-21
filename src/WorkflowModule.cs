using MeiYiJia.Abp.Workflow.Interface;
using MeiYiJia.Abp.Workflow.Service;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Autofac;
using Volo.Abp.Modularity;

namespace MeiYiJia.Abp.Workflow
{
    [DependsOn(typeof(AbpAutofacModule))]
    public class WorkflowModule: AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            Configure<WorkflowOptions>(options =>
            {
                options.Start = (serviceProvider, flow) => { };
                options.End = (serviceProvider, flow, step, result) => { };
                options.MaxWaitingQueueCount = 10;
            });
            context.Services.AddSingleton<ITokenBucket, InMemoryTokenBucket>();
            context.Services.AddSingleton<IPersistenceProvider, InMemoryPersistenceProvider>();
            context.Services.AddSingleton<IWorkflowRegistry, InMemoryWorkflowRegistry>();
            context.Services.AddSingleton<IWorkflowController, WorkflowController>();
            context.Services.AddSingleton<IWorkflowHost, WorkflowHost>();
        }
    }
}
