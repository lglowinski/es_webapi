using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExpertalSystem.Domain
{
    public class Question : BaseEntity
    {
     public string QuestionName { get; set; }
     public List<string> Answers { get; set; } = new List<string>();
    }
}
