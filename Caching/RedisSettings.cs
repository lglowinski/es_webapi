using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExpertalSystem.Caching
{
    public class RedisSettings
    {
        public string ConnectionString { get; set; }
        public int Database { get; set; }
        public bool Enabled { get; set; }
    }
}
