using Soneta.Business;
using Soneta.Example.AsyncWorker;

[assembly: Worker(typeof(NonConcurrentAsyncExtender1))]
[assembly: Worker(typeof(NonConcurrentAsyncExtender2))]
[assembly: Worker(typeof(NonConcurrentAsyncExtender3))]
[assembly: Worker(typeof(NonConcurrentAsyncExtender4))]
[assembly: Worker(typeof(NonConcurrentAsyncExtender5))]

namespace Soneta.Example.AsyncWorker
{
    public sealed class NonConcurrentAsyncExtender1 : AsyncExtenderBase
    {
    }

    public sealed class NonConcurrentAsyncExtender2 : AsyncExtenderBase
    {
    }

    public sealed class NonConcurrentAsyncExtender3 : AsyncExtenderBase
    {
    }

    public sealed class NonConcurrentAsyncExtender4 : AsyncExtenderBase
    {
    }

    public sealed class NonConcurrentAsyncExtender5 : AsyncExtenderBase
    {
    }
}