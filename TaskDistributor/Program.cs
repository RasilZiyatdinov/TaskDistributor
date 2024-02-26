using TaskDistributor;


class Program
{
    static async Task Main()
    {
        Console.Write("Введите количество исполнителей: ");
        int executorCount = Convert.ToInt32(Console.ReadLine());

        var executors = new List<Executor>();

        for (int i = 0; i < executorCount; i++)
        {
            executors.Add(new Executor(maxJobs: 2, maxLoad: 10)); //задание max нагрузки и количества задач
        }

        var loadBalancer = new LoadBalancer(executors);

        var random = new Random();
        
        for (int i = 0; i < 10; i++)
        {
            var job = new Job(load: random.Next(1, 6), runtime: random.Next(4, 11)); //задание нагрузки и времени выполнения задачи
            loadBalancer.EnqueueJob(job);
        }
        try
        {
            await loadBalancer.StartAsync();
        }
        catch(Exception ex)
        {
            Console.WriteLine(ex.ToString());
        }
    }
}
