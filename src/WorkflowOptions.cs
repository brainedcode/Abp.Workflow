using System;
using MeiYiJia.Abp.Workflow.Model;

namespace MeiYiJia.Abp.Workflow
{
    public class WorkflowOptions
    {
        public Action<IServiceProvider, WorkflowInstance> Start;
        public Action<IServiceProvider, WorkflowInstance, StepExecutionContext, ExecutionResult> End;
        /// <summary>
        /// 最大同时执行数量
        /// </summary>
        public int MaxWaitingQueueCount { get; set; } = 10;
    }
}