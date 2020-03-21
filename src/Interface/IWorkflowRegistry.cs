using MeiYiJia.Abp.Workflow.Model;

namespace MeiYiJia.Abp.Workflow.Interface
{
    public interface IWorkflowRegistry
    {
        void RegisterWorkflow(WorkflowDefinition workFlow);
        WorkflowDefinition GetWorkflowDefinition(string workflowId, int? version = null);

    }
}