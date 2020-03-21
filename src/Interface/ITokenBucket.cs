using System.Threading.Tasks;

namespace MeiYiJia.Abp.Workflow.Interface
{
    public interface ITokenBucket
    {
        Task<bool> TryGetToken();
        Task Decrease();
        Task Increase();
    }
}