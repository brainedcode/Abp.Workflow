using System.Collections.Generic;
using System.Threading.Tasks;
using MeiYiJia.Abp.Workflow.Model;

namespace MeiYiJia.Abp.Workflow.Interface
{
    public interface IWorkflowController
    {
        Task<string> StartWorkflow(string workflowId, Dictionary<string, object> data);
        Task<string> StartWorkflow(string workflowId, int? version, Dictionary<string, object> data);
        Task<string> StartWorkflow<TData>(string workflowId, TData data = null) where TData : class;
        Task<string> StartWorkflow<TData>(string workflowId, int? version, TData data = null) where TData : class;
        Task ResumeWorkflow(WorkflowInstance wfi);
        Task RetryWorkflow(string workflowInstanceId);
        bool TryGetWorkflowInstance(out WorkflowInstance wfi);
    }
}