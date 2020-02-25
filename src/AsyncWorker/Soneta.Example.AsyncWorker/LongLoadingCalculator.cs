using System;
using System.Threading;
using Soneta.Business;

namespace Soneta.Example.AsyncWorker
{
    internal sealed class LongLoadingCalculator
    {
        private static readonly Random Random = new Random();

        public decimal GetValue(IAsyncContext context = null)
        {
            var value = 0m;
            var timeout = Random.Next(5, 50);

            for (var i = 0; i < 50; i++)
            {
                if (IsCancelled(context)) return value;

                Thread.Sleep(timeout);
                value += (decimal) Random.Next()/10000;
            }

            return value;
        }

        private static bool IsCancelled(IAsyncContext context) =>
            context?.IsCancellationRequested ?? false;
    }
}