using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MeiYiJia.Abp.Workflow.Interface;
using MeiYiJia.Abp.Workflow.Model;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Sample.Abp.Workflow.Step;

namespace Sample.Abp.Workflow
{
    public class AppHostedService: BackgroundService
    {
        private readonly ILogger _logger;
        private readonly IWorkflowRegistry _workflowRegistry;
        private readonly IWorkflowController _workflowController;
        private readonly IWorkflowHost _workHost;

        public AppHostedService(ILogger<AppHostedService> logger, 
            IWorkflowController workflowController,
            IWorkflowRegistry workflowRegistry,
            IWorkflowHost workHost)
        {
            _logger = logger;
            _workflowController = workflowController;
            _workflowRegistry = workflowRegistry;
            _workHost = workHost;
        }
        
        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            // throw new System.NotImplementedException();
            _workflowRegistry.RegisterWorkflow(new WorkflowDefinition()
            {
                Id = "test",
                Steps = new List<WorkFlowStep>()
                {
                    new WorkFlowStep()
                    {
                        Id = "No1",
                        StepType = typeof(FirstStepAsync)
                    },
                    new WorkFlowStep()
                    {
                        Id = "No2",
                        StepType = typeof(SecondStepAsync)
                    },
                    new WorkFlowStep()
                    {
                        Id = "No3",
                        StepType = typeof(ThirdStepAsync)
                    }
                }
            });
            Parallel.For(0, 5, async (i, state) => await _workflowController.StartWorkflowAsync("test", new {TaskId = i}));
            await _workHost.StartAsync(stoppingToken);
        }

        public override async Task StopAsync(CancellationToken cancellationToken)
        {
            await _workHost.StopAsync(cancellationToken);
            await base.StopAsync(cancellationToken);
        }
    }
}