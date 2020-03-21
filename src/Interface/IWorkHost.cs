using System.Threading;
using System.Threading.Tasks;

namespace MeiYiJia.Abp.Workflow.Interface
{
    public interface IWorkHost
    {
        Task StartAsync(CancellationToken stoppingToken);
        Task StopAsync(CancellationToken stoppingToken);
    }
}