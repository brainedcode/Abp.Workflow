namespace MeiYiJia.Abp.Workflow.Exception
{
    public class WorkflowStepNotRegisteredException: System.Exception
    {
        public WorkflowStepNotRegisteredException(string workflowId, int? version)
            : base($"Workflow {workflowId} {version} not any step has registered")
        {
            
        }
    }
}