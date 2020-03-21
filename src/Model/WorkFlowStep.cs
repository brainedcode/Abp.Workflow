using System;

namespace MeiYiJia.Abp.Workflow.Model
{
    public class WorkFlowStep
    {
        public string Id { get; set; }
        public Type StepType { get; set; }
        // public string DataType { get; set; }
        public string NextStepId { get; set; }
        /// <summary>
        /// 失败重试次数，默认：0，不重试
        /// </summary>
        public int FailedRetryCount { get; set; } = 0;
    }
}