using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskDistributor
{
    /// <summary>
    /// Задача
    /// </summary>
    public class Job
    {
        public Job(int load, int runtime)
        {
            Load = load;
            Runtime = runtime;
        }

        private int load;
        public int Load
        {
            set
            {
                if (value < 1 || value > 5)
                    throw new ArgumentException("Нагрузка на исполнителя должна быть в диапазоне от 1 до 5");
                else
                    load = value;
            }
            get { return load; }
        }
        public int Runtime { get; set; }
    }
}
