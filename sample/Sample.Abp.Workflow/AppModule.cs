using MeiYiJia.Abp.Workflow;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Autofac;
using Volo.Abp.Modularity;

namespace Sample.Abp.Workflow
{
    [DependsOn(typeof(WorkflowModule),typeof(AbpAutofacModule))]
    public class AppModule: AbpModule
    {
        public override void ConfigureServices(ServiceConfigurationContext context)
        {
            // base.ConfigureServices(context);
            context.Services.AddHostedService<AppHostedService>();
        }
    }
}