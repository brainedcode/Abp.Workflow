using System;

namespace MeiYiJia.Abp.Workflow.Model
{
    public class WorkFlowExecuteInfo
    {
        public string Id { get; set; }
        public string WorkFlowId { get; set; }
        public string InComeData { get; set; }
        public long TotalConsumeMs { get; set; }
        public DateTime? BeginTime { get; set; }
        public DateTime? EndTime { get; set; }
        public DateTime CreateTime { get; set; }
        public DateTime LastUpdateTime { get; set; }
        public WorkFlowStatus WorkFlowStatus { get; set; }
        public string ErrorStepId { get; set; }
        public string ErrorMsg { get; set; }
        
    }
}