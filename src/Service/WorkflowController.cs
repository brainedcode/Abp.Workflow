using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using MeiYiJia.Abp.Workflow.Exception;
using MeiYiJia.Abp.Workflow.Interface;
using MeiYiJia.Abp.Workflow.Model;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Newtonsoft.Json;

namespace MeiYiJia.Abp.Workflow.Service
{
    public class WorkflowController: IWorkflowController
    {
        private readonly ILogger _logger;
        private readonly IWorkflowRegistry _registry;
        private readonly IPersistenceProvider _persistenceProvider;
        private readonly ITokenBucket _tokenBucket;
        private readonly ConcurrentQueue<WorkflowInstance> _workFlowsQueue = new ConcurrentQueue<WorkflowInstance>();

        public WorkflowController(ILogger<WorkflowController> logger,
            IPersistenceProvider persistenceProvider,
            IWorkflowRegistry registry,
            ITokenBucket tokenBucket)
        {
            _persistenceProvider = persistenceProvider;
            _logger = logger;
            _registry = registry;
            _tokenBucket = tokenBucket;
        }
        
        public Task<string> StartWorkflowAsync(string workflowId, Dictionary<string, object> data)
        {
            return StartWorkflowAsync(workflowId, null, data);
        }

        public Task<string> StartWorkflowAsync<TData>(string workflowId, TData data = default(TData)) where TData : class
        {
            return StartWorkflowAsync(workflowId, null, data);
        }

        public Task<string> StartWorkflowAsync<TData>(string workflowId, int? version, TData data = default(TData)) where TData : class
        {
            var dic = JsonConvert.DeserializeObject<Dictionary<string, object>>(JsonConvert.SerializeObject(data));
            return StartWorkflowAsync(workflowId, version, dic);
        }
        
        public async Task<string> StartWorkflowAsync(string workflowId, int? version, Dictionary<string, object> data)
        {
            var wfd = _registry.GetWorkflowDefinition(workflowId, version);
            if (wfd == null)
            {
                throw new WorkflowNotRegisteredException(workflowId, version);
            }

            var wfi = new WorkflowInstance()
            {
                Id = Guid.NewGuid().ToString(),
                WorkflowId = wfd.Id,
                Steps = wfd.Steps,
                Data = wfd.Data,
                Version = wfd.Version
            };
            if (wfi.Data == null)
            {
                wfi.Data = data;
            }
            else
            {
                foreach (var dataKey in data.Keys)
                {
                    if (wfi.Data.ContainsKey(dataKey))
                    {
                        wfi.Data[dataKey] = data[dataKey];
                    }
                    else
                    {
                        wfi.Data.Add(dataKey, data[dataKey]);
                    }
                }
            }

            if (await _tokenBucket.TryGetToken(default))
            {
                _workFlowsQueue.Enqueue(wfi);
            }
            else
            {
                await _persistenceProvider.StorageWorkflowInstanceWaitForConsumeAsync(wfi);
            }

            return wfi.Id;
        }

        public Task RetryWorkflowAsync(string workflowInstanceId)
        {
            return Task.CompletedTask;
        }

        public bool TryGetWorkflowInstance(out WorkflowInstance wfi)
        {
            return _workFlowsQueue.TryDequeue(out wfi);
        }

        public async Task<bool> TryResumeWorkflowInstanceAsync(CancellationToken stoppingToken)
        {
            if (await _tokenBucket.TryGetToken(stoppingToken))
            {
                var wfiRunnable = await _persistenceProvider.GetRunnableInstanceAsync(stoppingToken);
                if (wfiRunnable != null)
                {
                    _workFlowsQueue.Enqueue(wfiRunnable);
                    return true;
                }
            }
            return false;
        }

        public async Task CompleteWorkflowAsync(WorkflowInstance wfi)
        {
            await _tokenBucket.Increase();
        }
    }
}