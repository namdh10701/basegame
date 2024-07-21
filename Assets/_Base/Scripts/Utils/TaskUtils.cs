using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace _Base.Scripts.Utils.Extensions
{
    public static class TaskUtils
    {
        public static async Task WaitAllWithConcurrencyControl(
            IEnumerable<Func<Task>> taskFactories,
            int maxDegreeOfParallelism = 0)
        {
            if (maxDegreeOfParallelism <= 0)
            {
                await Task.WhenAll(taskFactories.Select(taskFactory => taskFactory()));
            }
            else
            {
                var semaphore = new SemaphoreSlim(maxDegreeOfParallelism);
                var tasks = new List<Task>();

                foreach (var taskFactory in taskFactories)
                {
                    await semaphore.WaitAsync();
                    tasks.Add(Task.Run(async () =>
                    {
                        try
                        {
                            await taskFactory();
                        }
                        finally
                        {
                            semaphore.Release();
                        }
                    }));
                }

                await Task.WhenAll(tasks);
            }
        }
        
        // public static async Task WhenAllLimitedConcurrency(IEnumerable<Task> tasks, int maxDegreeOfParallelism = 0)
        // {
        //     if (maxDegreeOfParallelism <= 0)
        //     {
        //         // If maxDegreeOfParallelism is 0 or less, run all tasks without limiting concurrency
        //         await Task.WhenAll(tasks);
        //         return;
        //     }
        //
        //     var taskList = new List<Task>();
        //     var enumerator = tasks.GetEnumerator();
        //
        //     async Task StartNextTask()
        //     {
        //         if (enumerator.MoveNext())
        //         {
        //             var task = enumerator.Current;
        //             var tcs = new TaskCompletionSource<object>();
        //             task.ContinueWith(t =>
        //             {
        //                 if (t.IsFaulted)
        //                 {
        //                     tcs.TrySetException(t.Exception.InnerExceptions);
        //                 }
        //                 else if (t.IsCanceled)
        //                 {
        //                     tcs.TrySetCanceled();
        //                 }
        //                 else
        //                 {
        //                     tcs.TrySetResult(null);
        //                 }
        //             });
        //             await tcs.Task;
        //             await StartNextTask();
        //         }
        //     }
        //
        //     for (int i = 0; i < maxDegreeOfParallelism; i++)
        //     {
        //         taskList.Add(StartNextTask());
        //     }
        //
        //     await Task.WhenAll(taskList);
        // }
        /// <summary>
        /// Execute tasks in parallel with concurrency control
        /// </summary>
        /// <param name="tasks">task list</param>
        /// <param name="maxDegreeOfParallelism">0: unlimited</param>
        public static async Task WhenAllLimitedConcurrency2(IEnumerable<Task> tasks, int maxDegreeOfParallelism = 0)
        {
            if (maxDegreeOfParallelism <= 0)
            {
                // If maxDegreeOfParallelism is 0 or less, run all tasks without limiting concurrency
                await Task.WhenAll(tasks);
                return;
            }

            var semaphore = new SemaphoreSlim(maxDegreeOfParallelism);
            var taskList = new List<Task>();

            foreach (var task in tasks)
            {
                await semaphore.WaitAsync();

                taskList.Add(Task.Run(async () =>
                {
                    try
                    {
                        await task;
                    }
                    finally
                    {
                        semaphore.Release();
                    }
                }));
            }

            await Task.WhenAll(taskList);
        }
    }
}