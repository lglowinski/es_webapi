using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ExpertalSystem.Domain;
using Newtonsoft.Json;

namespace ExpertalSystem.Dtos
{
    public class ProblemNameResponse : BaseEntity
    {
        [JsonProperty(propertyName: "problemName")]
        public string ProblemName { get; set; }
    }
}
