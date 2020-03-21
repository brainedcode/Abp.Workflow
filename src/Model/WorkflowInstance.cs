using System.Collections.Generic;

namespace MeiYiJia.Abp.Workflow.Model
{
    public class WorkflowInstance
    {
        public string Id { get; set; }
        public string WorkflowId { get; set; }
        public int Version { get; set; }
        public Dictionary<string, object> Data { get; set; }
        public string CurrentStepId { get; set; }
        public List<WorkFlowStep> Steps { get; set; }
    }
}