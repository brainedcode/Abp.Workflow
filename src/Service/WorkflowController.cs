using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
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
        
        public Task<string> StartWorkflow(string workflowId, Dictionary<string, object> data)
        {
            return StartWorkflow(workflowId, null, data);
        }

        public Task<string> StartWorkflow<TData>(string workflowId, TData data = default(TData)) where TData : class
        {
            return StartWorkflow(workflowId, null, data);
        }

        public Task<string> StartWorkflow<TData>(string workflowId, int? version, TData data = default(TData)) where TData : class
        {
            var dic = JsonConvert.DeserializeObject<Dictionary<string, object>>(JsonConvert.SerializeObject(data));
            return StartWorkflow(workflowId, version, dic);
        }
        
        public async Task<string> StartWorkflow(string workflowId, int? version, Dictionary<string, object> data)
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
                    // wf.Data.TryAdd(dataKey, data[dataKey]);
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

            await ResumeWorkflow(wfi);

            return default(string);
        }

        public async Task ResumeWorkflow(WorkflowInstance wfi)
        {
            if (await _tokenBucket.TryGetToken())
            {
                _workFlowsQueue.Enqueue(wfi);
            }
            else
            {
                await _persistenceProvider.StorageWorkflowInstanceWaitForConsumeAsync(wfi);
            }
        }

        public Task RetryWorkflow(string workflowInstanceId)
        {
            // var wfi = await _persistenceProvider.GetWorkflowInstanceAsync(workflowInstanceId);
            // if (wfi == null)
            // {
            //     throw new System.Exception($"{workflowInstanceId} not found");
            // }
            // else if (_workFlowsQueue.Count <= _options.MaxRunningInstance)
            // {
            //     _workFlowsQueue.Enqueue(wfi);
            // }
            // else
            // {
            //     // 丢回数据库
            // }
            return Task.CompletedTask;
        }

        public bool TryGetWorkflowInstance(out WorkflowInstance wfi)
        {
            return _workFlowsQueue.TryDequeue(out wfi);
        }
    }
}