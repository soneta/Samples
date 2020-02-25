using Soneta.Business;
using Soneta.Example.AsyncWorker;

[assembly: Worker(typeof(AsyncExtender))]

namespace Soneta.Example.AsyncWorker
{
    [AsyncWorker(IsConcurrent = true)]
    public sealed class AsyncExtender : AsyncExtenderBase
    {
    }
}