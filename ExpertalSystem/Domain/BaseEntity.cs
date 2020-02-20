using System;
using ExpertalSystem.Mongo;
using Newtonsoft.Json;

namespace ExpertalSystem.Domain
{
    public class BaseEntity : IBase
    {
        [JsonProperty(propertyName:"id")]
        public Guid Id { get; set; }
    }
}
