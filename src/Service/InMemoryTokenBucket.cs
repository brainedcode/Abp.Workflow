using System.Threading;
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
        private readonly int _maxSize;

        /// <summary>
        /// 当前大小
        /// </summary>
        private int _currentSize;
        /// <summary>
        /// 间隔多少秒自动填充
        /// </summary>
        private int AutoFillInInterval { get; set; } = 10;
        private readonly AsyncLock _mutex = new AsyncLock();
        private readonly ILogger _logger;

        public InMemoryTokenBucket(IOptions<WorkflowOptions> options, ILogger<InMemoryTokenBucket> logger)
        {
            _logger = logger;
            _currentSize = _maxSize = (options.Value ?? new WorkflowOptions()).MaxWaitingQueueCount;
        }
        public async Task<bool> TryGetToken(CancellationToken stoppingToken)
        {
            using (await _mutex.LockAsync())
            {
                if (_currentSize > 0 && _currentSize <= _maxSize)
                {
                    Interlocked.Decrement(ref _currentSize);
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
                if (_currentSize > 0)
                {
                    Interlocked.Decrement(ref _currentSize);
                }
            }
        }

        public async Task Increase()
        {
            using (await _mutex.LockAsync())
            {
                if (_currentSize < _maxSize)
                {
                    Interlocked.Increment(ref _currentSize);
                }
            }
        }
    }
}