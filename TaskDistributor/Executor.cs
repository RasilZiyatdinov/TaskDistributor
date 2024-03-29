﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskDistributor
{
    /// <summary>
    /// Исполнитель
    /// </summary>
    public class Executor
    {
        public Executor(int maxJobs, int maxLoad)
        {
            MaxJobs = maxJobs;
            MaxLoad = maxLoad;
            CurrentLoad = 0;
            CurrentJobs = new List<Job>();
        }

        public int MaxJobs { get; set; }
        public int MaxLoad { get; set; }
        public int CurrentLoad { get; set; }
        public readonly List<Job> CurrentJobs;

        public bool CanTake(Job job)
        {
            return CurrentJobs.Count < MaxJobs && CurrentLoad + job.Load <= MaxLoad;
        }
    }
}
