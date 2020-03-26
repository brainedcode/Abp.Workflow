using System;
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
            Configure<WorkflowOptions>(options =>
            {
                options.Start = (serviceProvider, flow) =>
                {
                    Console.WriteLine("开始");
                };
                options.End = (serviceProvider, flow, step, result) =>
                {
                    Console.WriteLine("结束");
                };
                options.MaxWaitingQueueCount = 2;
            });
            context.Services.AddHostedService<AppHostedService>();
        }
    }
}