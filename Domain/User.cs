using MongoDB.Bson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExpertalSystem.Domain
{
    public class User : BaseEntity
    {
        public string Name { get; set; }
        public string Password { get; set; }
    }
}
