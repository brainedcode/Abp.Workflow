using System.Threading;
using System.Threading.Tasks;

namespace MeiYiJia.Abp.Workflow.Interface
{
    public interface IWorkflowHost
    {
        Task StartAsync(CancellationToken stoppingToken);
        Task StopAsync(CancellationToken stoppingToken);
    }
}