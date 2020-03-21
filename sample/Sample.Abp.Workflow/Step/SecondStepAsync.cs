using System.Threading;
using System.Threading.Tasks;
using MeiYiJia.Abp.Workflow.Interface;
using MeiYiJia.Abp.Workflow.Step;

namespace Sample.Abp.Workflow.Step
{
    public class SecondStepAsync: BaseStepBodyAsync
    {
        public override Task WorkAsync(IStepExecutionContext context, CancellationToken stoppingToken = default)
        {
            // throw new System.NotImplementedException();
            return Task.CompletedTask;
        }
    }
}