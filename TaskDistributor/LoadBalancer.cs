using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskDistributor
{
    /// <summary>
    /// Балансировщик
    /// </summary>
    public class LoadBalancer
    {
        private readonly List<Executor> Executors;
        private readonly Queue<Job> JobQueue;
        private List<Task<Job>> RunnedJobs;
        public LoadBalancer(List<Executor> executors)
        {
            Executors = executors;
            JobQueue = new Queue<Job>();
            RunnedJobs = new List<Task<Job>>();
        }

        public async Task StartAsync()
        {
            while (true)
            {
                if (JobQueue.Count > 0)
                {
                    var job = JobQueue.Dequeue();
                    var availableExecutor = GetAvailableExecutor(job);

                    if (availableExecutor != null)
                    {
                         RunnedJobs.Add(ExecuteJobAsync(job, availableExecutor));
                    }           
                }

                Task.WaitAny(RunnedJobs.ToArray());
                DisplayLoadStatus();
                await Task.Delay(1000); 
            }
        }

        public void EnqueueJob(Job job)
        {
            JobQueue.Enqueue(job);
        }

        private Executor? GetAvailableExecutor(Job job)
        {
            var sortedExecutors = Executors.OrderBy(e => e.CurrentLoad);
            return sortedExecutors.FirstOrDefault(e => e.CanTake(job));
        }

        private async Task<Job> ExecuteJobAsync(Job job, Executor executor)
        {
            executor.CurrentLoad += job.Load;
            executor.CurrentJobs.Add(job);

            await Task.Delay(job.Runtime * 1000);

            executor.CurrentLoad -= job.Load;
            executor.CurrentJobs.Remove(job);

            return job;
        }

        private void DisplayLoadStatus()
        {
            Console.Clear();
            foreach (var executor in Executors)
            {
                Console.WriteLine($"Исполнитель {Executors.IndexOf(executor) + 1}:");
                Console.WriteLine($"  - Текущая нагрузка: {executor.CurrentLoad}/{executor.MaxLoad}");
                Console.WriteLine($"  - Количество исполняемых задач: {executor.CurrentJobs.Count}/{executor.MaxJobs}");
                Console.WriteLine();
            }

            Console.WriteLine($"Задач в очереди: {JobQueue.Count}");
        }
    }
}
