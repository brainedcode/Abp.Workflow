using System.Threading;
using System.Threading.Tasks;
using MeiYiJia.Abp.Workflow.Model;
using Volo.Abp.DependencyInjection;

namespace MeiYiJia.Abp.Workflow.Interface
{
    public interface IStepBodyAsync: ITransientDependency
    {
        Task<ExecutionResult> RunAsync(IStepExecutionContext context, CancellationToken stoppingToken = default);
    }
}