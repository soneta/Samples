using Soneta.Business;
using Soneta.Example.AsyncWorker;

[assembly: Worker(typeof(ConcurrentAsyncExtender1))]
[assembly: Worker(typeof(ConcurrentAsyncExtender2))]
[assembly: Worker(typeof(ConcurrentAsyncExtender3))]
[assembly: Worker(typeof(ConcurrentAsyncExtender4))]
[assembly: Worker(typeof(ConcurrentAsyncExtender5))]

namespace Soneta.Example.AsyncWorker
{
    [AsyncWorker(IsConcurrent = true)]
    public sealed class ConcurrentAsyncExtender1 : AsyncExtenderBase
    {
    }

    [AsyncWorker(IsConcurrent = true)]
    public sealed class ConcurrentAsyncExtender2 : AsyncExtenderBase
    {
    }

    [AsyncWorker(IsConcurrent = true)]
    public sealed class ConcurrentAsyncExtender3 : AsyncExtenderBase
    {
    }

    [AsyncWorker(IsConcurrent = true)]
    public sealed class ConcurrentAsyncExtender4 : AsyncExtenderBase
    {
    }

    [AsyncWorker(IsConcurrent = true)]
    public sealed class ConcurrentAsyncExtender5 : AsyncExtenderBase
    {
    }
}