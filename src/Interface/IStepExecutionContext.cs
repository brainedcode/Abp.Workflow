using System;
using System.Collections.Generic;
using System.Threading;
using MeiYiJia.Abp.Workflow.Model;
using Microsoft.Extensions.Logging;

namespace MeiYiJia.Abp.Workflow.Interface
{
    public interface IStepExecutionContext
    {
        WorkFlowStep CurrentStep { get; set; }
        string NextStepEventName { get; set; }
        CancellationToken StoppingToken { get; set; }
        IServiceProvider ServiceProvider { get; set; }
        ILogger Logger { get; set; }
        Dictionary<string, object> ContextData { get; set; }
        
    }
}