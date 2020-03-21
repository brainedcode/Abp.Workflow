using System;
using System.Collections.Generic;
using System.Threading;
using MeiYiJia.Abp.Workflow.Interface;
using Microsoft.Extensions.Logging;

namespace MeiYiJia.Abp.Workflow.Model
{
    public class StepExecutionContext:IStepExecutionContext
    {
        public WorkFlowStep CurrentStep { get; set; }
        public string NextStepEventName { get; set; }
        public CancellationToken StoppingToken { get; set; }
        public IServiceProvider ServiceProvider { get; set; }
        public ILogger Logger { get; set; }
        public Dictionary<string, object> ContextData { get; set; } = new Dictionary<string, object>();
    }
}