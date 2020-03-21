using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MeiYiJia.Abp.Workflow.Model;

namespace MeiYiJia.Abp.Workflow.Interface
{
    public interface IPersistenceProvider
    {
        // Task<string> CreateNewWorkflowAsync(WorkFlow workFlow, CancellationToken stoppingToken = default);
        // Task PersisWorkflowAsync(string workFlowId, WorkFlow wf, StepExecutionContext context,
        //     ExecutionResult executionResult, long swElapsedMilliseconds, CancellationToken stoppingToken = default);
        // Task PersisWorkflowStepAsync(string workFlowId, WorkFlowStep step, Dictionary<string, object> inComeData, Dictionary<string, object> outComeData, ExecutionResult executionResult, CancellationToken stoppingToken = default);
        // Task<WorkFlow> GetWorkflowInstanceAsync(string id, CancellationToken stoppingToken = default);
        // Task<IEnumerable<WorkFlow>> GetWorkflowInstancesAsync(IEnumerable<string> ids, CancellationToken stoppingToken = default);
        // Task<IEnumerable<WorkFlow>> GetWorkflowInstancesAsync(WorkFlowStatus? status, string type, DateTime? createdFrom, DateTime? createdTo, int skip, int take, CancellationToken stoppingToken = default);
        Task PersistWorkflowStepAsync(string wfiId, string workflowId, WorkFlowStep workFlowStep, Dictionary<string, object> inComeData, Dictionary<string, object> outComeData, ExecutionResult executionResult, CancellationToken stoppingToken);
        Task PersistWorkflowInstanceAsync(WorkflowInstance wfi, StepExecutionContext context, ExecutionResult executionResult, long elapsedMilliseconds, CancellationToken stoppingToken);
        Task CreateNewWorkflowInstanceAsync(WorkflowInstance wfi, CancellationToken stoppingToken = default);
        Task StorageWorkflowInstanceWaitForConsumeAsync(WorkflowInstance wfi, CancellationToken stoppingToken = default);
        Task<WorkflowInstance> GetRunnableInstanceAsync(CancellationToken stoppingToken = default);

    }
}