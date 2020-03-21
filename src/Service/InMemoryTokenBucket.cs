using System.Threading.Tasks;
using MeiYiJia.Abp.Workflow.Interface;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Nito.AsyncEx;
using Volo.Abp.DependencyInjection;

namespace MeiYiJia.Abp.Workflow.Service
{
    [ExposeServices(typeof(ITokenBucket))]
    public class InMemoryTokenBucket: ITokenBucket, ISingletonDependency
    {
        /// <summary>
        /// 令牌桶大小上限
        /// </summary>
        private int MaxSize { get; set; } = 10;

        /// <summary>
        /// 当前大小
        /// </summary>
        private int CurrentSize { get; set; } = 10;
        /// <summary>
        /// 间隔多少秒自动填充
        /// </summary>
        private int AutoFillInInterval { get; set; } = 10;
        private readonly AsyncLock _mutex = new AsyncLock();
        private readonly ILogger _logger;
        private readonly object _locker = new object();

        public InMemoryTokenBucket(IOptions<WorkflowOptions> options, ILogger<InMemoryTokenBucket> logger)
        {
            _logger = logger;
            CurrentSize = MaxSize = (options.Value ?? new WorkflowOptions()).MaxWaitingQueueCount;
        }
        public async Task<bool> TryGetToken()
        {
            using (await _mutex.LockAsync())
            {
                if (CurrentSize <= MaxSize)
                {
                    CurrentSize--;
                    return true;
                }
                else
                {
                    return false;
                }
            }
        }

        public async Task Decrease()
        {
            using (await _mutex.LockAsync())
            {
                if (CurrentSize > 0)
                {
                    CurrentSize--;
                }
            }
        }

        public async Task Increase()
        {
            using (await _mutex.LockAsync())
            {
                if (CurrentSize > 0 && CurrentSize < MaxSize)
                {
                    CurrentSize++;
                }
            }
        }
    }
}