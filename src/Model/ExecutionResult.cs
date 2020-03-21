namespace MeiYiJia.Abp.Workflow.Model
{
    public class ExecutionResult
    {
        public bool Proceed { get; set; }
        public long ConsumeElapsedMilliseconds { get; set; }
        public System.Exception InnerException { get; set; }
        public string NextStepId { get; set; }
    }
}