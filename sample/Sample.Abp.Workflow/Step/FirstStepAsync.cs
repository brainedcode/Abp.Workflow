using System;
using System.Threading;
using System.Threading.Tasks;
using MeiYiJia.Abp.Workflow.Interface;
using MeiYiJia.Abp.Workflow.Step;
using Microsoft.Extensions.Logging;

namespace Sample.Abp.Workflow.Step
{
    public class FirstStepAsync: BaseStepBodyAsync
    {
        private readonly ILogger _logger;

        public FirstStepAsync(ILogger<FirstStepAsync> logger)
        {
            _logger = logger;
        }
        public override async Task WorkAsync(IStepExecutionContext context, CancellationToken stoppingToken = default)
        {
            // throw new System.NotImplementedException();
            await Task.Delay(new Random().Next(1000, 3000), stoppingToken);
            _logger.LogError($"{context.ContextData["TaskId"]}");
            // return Task.CompletedTask;
        }
    }
}