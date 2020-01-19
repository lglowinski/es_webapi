using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ExpertalSystem.Dtos
{
    public class Answere : Response
    {
        public Answere(string text) : base(text, ResponseType.Answere)
        {
        }
    }
}
