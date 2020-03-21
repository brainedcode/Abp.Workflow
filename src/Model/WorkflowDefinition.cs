using System.Collections.Generic;

namespace MeiYiJia.Abp.Workflow.Model
{
    public class WorkflowDefinition
    {
        public string Id { get; set; }
        public int Version { get; set; }
        public Dictionary<string, object> Data { get; set; }
        public List<WorkFlowStep> Steps { get; set; }
        
    }
}