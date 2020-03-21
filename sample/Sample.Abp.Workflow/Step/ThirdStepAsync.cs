using System;
using System.Threading;
using System.Threading.Tasks;
using MeiYiJia.Abp.Workflow.Interface;
using MeiYiJia.Abp.Workflow.Step;

namespace Sample.Abp.Workflow.Step
{
    public class ThirdStepAsync: BaseStepBodyAsync
    {
        public override Task WorkAsync(IStepExecutionContext context, CancellationToken stoppingToken = default)
        {
            // throw new System.NotImplementedException();
            // throw new Exception("3333333");
            return Task.CompletedTask;
        }
    }
}