using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MeiYiJia.Abp.Workflow.Model;

namespace MeiYiJia.Abp.Workflow.Interface
{
    public interface IWorkflowController
    {
        Task<string> StartWorkflowAsync(string workflowId, Dictionary<string, object> data);
        Task<string> StartWorkflowAsync(string workflowId, int? version, Dictionary<string, object> data);
        Task<string> StartWorkflowAsync<TData>(string workflowId, TData data = null) where TData : class;
        Task<string> StartWorkflowAsync<TData>(string workflowId, int? version, TData data = null) where TData : class;
        Task RetryWorkflowAsync(string workflowInstanceId);
        bool TryGetWorkflowInstance(out WorkflowInstance wfi);
        Task<bool> TryResumeWorkflowInstanceAsync(CancellationToken stoppingToken);
        Task CompleteWorkflowAsync(WorkflowInstance wfi);
    }
}