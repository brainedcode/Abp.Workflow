using System;

namespace MeiYiJia.Abp.Workflow.Model
{
    public class WorkFlowStepExecuteInfo
    {
        public string ReferId { get; set; }
        public string StepId { get; set; }
        public long ConsumeMs { get; set; }
        public string InComeData { get; set; }
        public string OutComeData { get; set; }
        public DateTime CreateTime { get; set; }
        public WorkFlowStatus WorkFlowStepStatus { get; set; }
        public string ErrorMsg { get; set; }
    }
}