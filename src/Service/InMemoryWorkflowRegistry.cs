using System;
using System.Collections.Generic;
using System.Linq;
using MeiYiJia.Abp.Workflow.Interface;
using MeiYiJia.Abp.Workflow.Model;
using Microsoft.Extensions.Logging;

namespace MeiYiJia.Abp.Workflow.Service
{
    public class InMemoryWorkflowRegistry: IWorkflowRegistry
    {
        private readonly ILogger _logger;
        private readonly List<ValueTuple<string, int, WorkflowDefinition>> _registry = new List<ValueTuple<string, int, WorkflowDefinition>>();

        public InMemoryWorkflowRegistry(ILogger<InMemoryWorkflowRegistry> logger) => _logger = logger;
        
        public void RegisterWorkflow(WorkflowDefinition workflowDefinition)
        {
            if (_registry.Any(x => x.Item1 == workflowDefinition.Id && x.Item2 == workflowDefinition.Version))
            {
                throw new InvalidOperationException($"WorkFlow {workflowDefinition.Id} version {workflowDefinition.Version} is already registered");
            }
            _registry.Add(ValueTuple.Create(workflowDefinition.Id, workflowDefinition.Version, workflowDefinition));
        }

        public WorkflowDefinition GetWorkflowDefinition(string workflowId, int? version = null)
        {
            if (version.HasValue)
            {
                var entry = _registry.FirstOrDefault(x => x.Item1 == workflowId && x.Item2 == version.Value);
                return entry.Item3;
            }
            else
            {
                var maxVersion = _registry.Where(x => x.Item1 == workflowId).Max(x => x.Item2);
                var entry = _registry.FirstOrDefault(x => x.Item1 == workflowId && x.Item2 == maxVersion);
                return entry.Item3;
            }
        }
    }
}