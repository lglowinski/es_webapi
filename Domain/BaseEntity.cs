using System;
using ExpertalSystem.Mongo;

namespace ExpertalSystem.Domain
{
    public class BaseEntity : IBase
    {
        public Guid Id { get; set; }
    }
}
