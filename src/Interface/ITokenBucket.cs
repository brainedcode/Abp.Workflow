using System.Threading;
using System.Threading.Tasks;

namespace MeiYiJia.Abp.Workflow.Interface
{
    public interface ITokenBucket
    {
        Task<bool> TryGetToken(CancellationToken stoppingToken);
        Task Decrease();
        Task Increase();
    }
}