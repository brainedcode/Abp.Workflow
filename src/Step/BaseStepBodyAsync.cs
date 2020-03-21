using System.Diagnostics;
using System.Threading;
using System.Threading.Tasks;
using MeiYiJia.Abp.Workflow.Interface;
using MeiYiJia.Abp.Workflow.Model;
using Volo.Abp.DependencyInjection;

namespace MeiYiJia.Abp.Workflow.Step
{
    /// <summary>
    /// 务必继承此类，实现work方法。
    /// </summary>
    [ExposeServices(typeof(IStepBodyAsync))]
    public abstract class BaseStepBodyAsync: IStepBodyAsync 
    {
        private readonly ExecutionResult _executionResult = new ExecutionResult();
        private readonly Stopwatch _sp = new Stopwatch();

        #region 需要实现的

        public virtual Task WorkBeforeAsync(IStepExecutionContext context, CancellationToken stoppingToken = default)
        {
            return Task.CompletedTask;
        }
        public abstract Task WorkAsync(IStepExecutionContext context, CancellationToken stoppingToken = default);

        public virtual Task WorkAfterAsync(IStepExecutionContext context, CancellationToken stoppingToken = default)
        {
            return Task.CompletedTask;
        }

        #endregion

        public async Task<ExecutionResult> RunAsync(IStepExecutionContext context, CancellationToken stoppingToken = default)
        {
            try
            {
                _sp.Start();
                await WorkBeforeAsync(context, stoppingToken);
                await WorkAsync(context, stoppingToken);
                await WorkAfterAsync(context, stoppingToken);
                _executionResult.Proceed = true;
            }
            catch (System.Exception e)
            {
                _executionResult.Proceed = false;
                _executionResult.InnerException = e;
            }
            finally
            {
                _sp.Stop();
                _executionResult.ConsumeElapsedMilliseconds = _sp.ElapsedMilliseconds;
            }
            return _executionResult;
        }
    }
}