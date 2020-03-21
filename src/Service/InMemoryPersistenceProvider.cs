using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
// using Dapper;
using MeiYiJia.Abp.Workflow.Interface;
using MeiYiJia.Abp.Workflow.Model;
// using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace MeiYiJia.Abp.Workflow.Service
{
    public class InMemoryPersistenceProvider: IPersistenceProvider
    {
        // private readonly IMapper _mapper;
        private readonly IWorkflowRegistry _registry;
        private readonly ILogger _logger;
        private readonly ConcurrentQueue<WorkflowInstance> _workflowInstanceWaitForRunningQueue = new ConcurrentQueue<WorkflowInstance>();

        public InMemoryPersistenceProvider(IConfiguration configuration,
            // IMapper mapper,
            IWorkflowRegistry registry,
            ILogger<InMemoryPersistenceProvider> logger)
        {
            // _mapper = mapper;
            _registry = registry;
            _logger = logger;
        }


        // public Task<string> CreateNewWorkflowAsync(WorkFlow workFlow, CancellationToken stoppingToken = default)
        // {
        //     throw new NotImplementedException();
        // }
        //
        // public Task PersisWorkflowAsync(string workFlowId, WorkFlow wf, StepExecutionContext context, ExecutionResult executionResult,
        //     long swElapsedMilliseconds, CancellationToken stoppingToken = default)
        // {
        //     throw new NotImplementedException();
        // }
        //
        // public Task PersisWorkflowStepAsync(string workFlowId, WorkFlowStep step, Dictionary<string, object> inComeData, Dictionary<string, object> outComeData,
        //     ExecutionResult executionResult, CancellationToken stoppingToken = default)
        // {
        //     throw new NotImplementedException();
        // }
        //
        // public Task<IEnumerable<string>> GetRunnableInstancesAsync(DateTime asAt, CancellationToken stoppingToken = default)
        // {
        //     throw new NotImplementedException();
        // }
        //
        // public Task<WorkFlow> GetWorkflowInstanceAsync(string id, CancellationToken stoppingToken = default)
        // {
        //     throw new NotImplementedException();
        // }
        //
        // public Task<IEnumerable<WorkFlow>> GetWorkflowInstancesAsync(IEnumerable<string> ids, CancellationToken stoppingToken = default)
        // {
        //     throw new NotImplementedException();
        // }
        //
        // public Task<IEnumerable<WorkFlow>> GetWorkflowInstancesAsync(WorkFlowStatus? status, string type, DateTime? createdFrom, DateTime? createdTo,
        //     int skip, int take, CancellationToken stoppingToken = default)
        // {
        //     throw new NotImplementedException();
        // }
        public Task PersistWorkflowStepAsync(string wfiId, string workflowId, WorkFlowStep workFlowStep, Dictionary<string, object> inComeData,
            Dictionary<string, object> outComeData, ExecutionResult executionResult, CancellationToken stoppingToken)
        {
            _logger.LogInformation($"{workFlowStep.Id} 执行结果：{executionResult.Proceed}");
            return Task.CompletedTask;
        }

        public Task PersistWorkflowInstanceAsync(WorkflowInstance wfi, StepExecutionContext context, ExecutionResult executionResult,
            long elapsedMilliseconds, CancellationToken stoppingToken)
        {
            _logger.LogInformation($"{wfi.WorkflowId} 执行结果：{executionResult.Proceed}");
            return Task.CompletedTask;
        }

        public Task CreateNewWorkflowInstanceAsync(WorkflowInstance wfi, CancellationToken stoppingToken = default)
        {
            return Task.CompletedTask;
        }

        public Task StorageWorkflowInstanceWaitForConsumeAsync(WorkflowInstance wfi, CancellationToken stoppingToken = default)
        {
            _logger.LogInformation($"{wfi.Id}-{wfi.WorkflowId} 等待执行");
            _workflowInstanceWaitForRunningQueue.Enqueue(wfi);
            return Task.CompletedTask;
        }

        public Task<WorkflowInstance> GetRunnableInstanceAsync(CancellationToken stoppingToken = default)
        {
            if (_workflowInstanceWaitForRunningQueue.TryDequeue(out var wfi))
            {
                return Task.FromResult(wfi);
            }
            else
            {
                return Task.FromResult(default(WorkflowInstance));
            }
        }
    }
}