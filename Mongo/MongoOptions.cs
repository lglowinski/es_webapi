using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExpertalSystem.Mongo
{
    public class MongoOptions
    {
        public string ConnectionString { get; set; }
        public string DbName { get; set; }
        public string Users { get; set; }
        public string Rules { get; set; }
    }
}
