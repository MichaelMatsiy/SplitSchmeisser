using SplitSchmeisser.BLL.Interfaces;
using SplitSchmeisser.BLL.Models;
using SplitSchmeisser.DAL.Entities;
using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace SplitSchmeisser.BLL.ReportWorker
{
    public class ReportGenerateResolver : IReportGenerateResolver
    {
        BlockingCollection<ReportRequest> queue = new BlockingCollection<ReportRequest>();

        private readonly object _syncRoot = new object();
        private bool _consumerHasStarted = false;

        public void Enqueue(ReportRequest request)
        {
            if (!_consumerHasStarted)
            {
                StartConsumer();
            }

            queue.Add(request);
        }

        private void Consume()
        {
            //foreach (var workItem in queue.GetConsumingEnumerable())
            //{
            //    try
            //    {
            //        Generate();
            //    }
            //    catch { }
            //}
        }

        //public Task Generate(Task<List<Operation>> task)
        //{
        //    lock (_syncRoot)
        //    {
        //        return Task.Run(action);
        //    }
        //}

        private void StartConsumer()
        {
            lock (_syncRoot)
            {
                if (!_consumerHasStarted)
                {
                    Task.Run(Consume);
                    _consumerHasStarted = true;
                }
            }
        }

        private SemaphoreSlim semaphore;
        public ReportGenerateResolver()
        {
            semaphore = new SemaphoreSlim(1);
        }

        public async Task<T> Enqueue<T>(Func<Task<T>> taskGenerator)
        {
            Debug.WriteLine(semaphore.CurrentCount);
            await semaphore.WaitAsync();
            try
            {
                return await taskGenerator();
            }
            finally
            {
                semaphore.Release();
            }
        }
        public async Task Enqueue(Func<Task> taskGenerator)
        {
            await semaphore.WaitAsync();
            try
            {
                await taskGenerator();
            }
            finally
            {
                semaphore.Release();
            }
        }


    }
}
