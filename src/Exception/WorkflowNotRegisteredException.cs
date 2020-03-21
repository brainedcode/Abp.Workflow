namespace MeiYiJia.Abp.Workflow.Exception
{
    public class WorkflowNotRegisteredException: System.Exception
    {
        public WorkflowNotRegisteredException(string workflowId, int? version)
            : base($"Workflow {workflowId} {version} is not registered")
        {
            
        }
    }
}